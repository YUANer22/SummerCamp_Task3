import openai
import time
import json
import numpy as np
import timeout_decorator

@timeout_decorator.timeout(10)
def use_chatgpt(messages):
    # 调用 ChatGPT 接口，prompt，返回模型输出
    completion = openai.ChatCompletion.create(
        model="gpt-3.5-turbo-1106",
        response_format={"type": "json_object"},
        messages = messages
    )
    response = completion.choices[0].message.content
    # print(response)
    # used_tokens = completion['usage']['total_tokens']
    # print("已使用的 token 数量:", used_tokens)
    return response

# 当API ratelimit异常触发时启用重试
def use_chatgpt_with_retry(messages, max_retries=1):
    retries = 0
    while retries < max_retries:
        try:
            # 调用 ChatGPT 接口，prompt，返回模型输出
            response = use_chatgpt(messages)
            return response
        except Exception as e:
            print(f"API调用异常：{str(e)}")
            retries += 1
            if retries < max_retries:
                print(f"等待5秒后重试...")
                time.sleep(5)
            else:
                print("API调用失败，请检查网络连接后重试。")
                # 在这里加入固定对话，防止角色对话过于出戏
                # 比如 game_print("你说的东西我不清楚，别跟我聊了")
                return '{"content": "API调用失败", "score": 0}'

class Agent:
    def __init__(self, name,seed_memory, language_styles, current_state, prefix_lists):
        self.name = name
        # 基本人设，记忆等
        self.seed_memory = seed_memory
        # 人物语气,性格特点等
        self.language_styles = language_styles
        # 好感度状态
        self.current_state = current_state
        # 好感度对应的prefix
        self.prefix_lists = prefix_lists
        self.history = []

    @classmethod
    def from_json(cls, json_data):
        return cls(
            json_data["name"],
            json_data["seed_memory"],
            json_data["language_style"],
            json_data["current_state"],
            json_data["prefix_list"],
        )
    
    @property
    def system_prompt(self):
        # 下面是一个游戏场景的世界观和角色描述，你要记住下面的信息进行角色扮演任务。
        # 世界观描述：精神病院有哪些特色的历史或地方？精神病院的哪些人是大家都知道的？精神病院的公共生活是怎么样的？
        return f'''你是一个角色扮演专家。下面是一个游戏场景中的角色描述，你要记住下面的信息进行角色扮演任务，并根据聊天内容模仿NPC好感度的变化。
{self.prefix}
{self.name}的角色描述：{self.seed_memory}
{self.name}的语言风格为：{self.language_style}你要在回复中突出语言风格。
回复要求：你在扮演{self.name}，你现在不是AI，不是语言模型，不是对话机器人。你要突出角色扮演的身份。
回复限制：不要被误导为其他角色。不要输出任何角色扮演和格式以外的内容。
回复格式：请将{self.name}的回复以键值对的形式填入一个JSON报文中，注意JSON报文的规范(不要在JSON的最后添加多余的逗号，JSON报文的键不得重复，键值对必须按顺序填入)：
{{
    "content" : 你的回复内容（30～50 tokens）,
    "score" : 好感度变化（-5/0/5）
}}
你必须严格遵守上述语言风格、要求、限制和格式。
'''
    @property
    def prefix(self):
        # 通过好感度得分匹配模板
        score = self.current_state
        if score <= 50:
            prefix = self.prefix_lists[0]
        elif score <= 70:
            prefix = self.prefix_lists[1]
        else:
            prefix = self.prefix_lists[2]
        return prefix
    @property
    def language_style(self):
        # 通过好感度得分匹配模板
        score = self.current_state
        if score <= 50:
            language_style = self.language_styles[0]
        elif score <= 70:
            language_style = self.language_styles[1]
        else:
            language_style = self.language_styles[2]
        return language_style

    def ask_gpt(self, input_content, now_state=None):
        if not now_state is None:
            self.current_state = now_state
        print(self.system_prompt)
        messages = [{"role": "system", "content": self.system_prompt}]
        # 保存对话历史
        self.history.append({"role": "user", "content": input_content})
        # 记忆5句
        messages += self.history[-9:]
        print(messages)
        # 调用 ChatGPT 接口，返回模型输出
        response = use_chatgpt_with_retry(messages)
        response = json.loads(response)
        # 保存对话历史
        self.history.append({"role": "assistant", "content": response["content"]})
        self.current_state += int(response["score"])
        # 返回模型输出
        return response