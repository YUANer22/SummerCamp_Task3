# 赛博精神病Cyber Psychosis

## 项目背景

### 使用场景

在近年的游戏行业发展中，AI大模型已经被尝试性地运用到了部分游戏中，例如网易自研的“逆水寒”游戏中，玩家便可以和NPC进行对话，并获得不同的反馈以提升游戏的趣味性。除此之外，以游戏为媒介，AI agent技术也被运用于游戏开发中，并用于进行各样的前沿社会测试，例如[斯坦福小镇](https://github.com/joonspk-research/generative_agents/tree/main)。

![image-20240712212640518](C:/Users/yuaner/AppData/Roaming/Typora/typora-user-images/image-20240712212640518.png)

基于此，我构想将大模型融合于一款以未来为背景的解密游戏中，玩家需要与不同的NPC（搭载不同的AI agent）进行互动，达到一定的条件才能解锁对应道具并取得最终通关。该游戏不仅能提供一定的商业价值，也是对人与AI交互的一次以游戏为媒介的测试。

## 项目基本说明

### 故事背景：

在未来的一个城市中，居民们是各式各样的”动物“，大部分”人“有属于自己的专属 AI 仿生机器人，平时用作日常服务，在主人肉体死亡后机构便会上传其主人的数据意识至机器人使其“重生”，在某一天，由于“机构”的“数据传输失误”，部分机器人失控获得了自我意识，并将自己和主人的角色混淆，这些发生异变的机器人大部分已被清除，而我们的主角由于“错误的检测”被带到我们的赛博精神病医院里，在这里，你见证了各式各样由于科技发展而产生的“精神病人们”，你需要证明你是一名正常“人类”,然而，事情真的这麽简单吗...（赛博精神病：本游戏中指由于科技发展而引发的各种混合新型精神病，由于出现时间短，医学对此类病症缺乏明确认识，便统称赛博精神病）

### 游戏流程：

游戏细节流程较为复杂，故作以下流程图便于理解：

![681380f4c764ac1f177fdec7a8c0a8f](D:/%E6%96%B0%E5%BB%BA%E6%96%87%E4%BB%B6%E5%A4%B9/OneDrive/%E6%96%87%E6%A1%A3/WeChat%20Files/wxid_lek3r6n3aum122/FileStorage/Temp/681380f4c764ac1f177fdec7a8c0a8f.jpg)

其中游戏有逐渐推进的时间设定，每天都需要与NPC交互以提高NPC好感度进而推进游戏，流程如下：

![image](../../../image.png)

## 技术栈

### Unity

引擎：[Unity 2021.3.30.f1](https://unity.com/releases/editor/whats-new/2021.3.30) (2021 最新lts版本，推荐使用Unity Hub安装)   
AI接入：尝试ChatGPT （Inworld AI无中文）  
项目结构：  

- data  存放配置的data
- asset 美术资源 
- src   项目工程存放（代码等）
- tools 测试功能时写的一些工具等

### Stable Diffusion

[Stable Diffusion](noveai/models/Stabl
e-diffusion)用于生成游戏中的美术资产，节约了UI相关的人力投入和时间成本,具体工作流如下：

![image-20240712212545506](C:/Users/yuaner/AppData/Roaming/Typora/typora-user-images/image-20240712212545506.png)

### 好感度分析模块（利用Open AI  API实现）

#### 想法

当玩家与NPC交互时，他们的输入被转化为嵌入向量，这些向量则被用于NPC的记忆和知识检索过程中，以生成响应和提供信息。为了进一步提升效率和可调试性，可以使用这些已生成的嵌入向量来进行情感分析。通过计算输入文本的嵌入向量与预定义情感标签的相似度差，我们可以更精确地量化文本的情感倾向，并据此调整代理角色的好感度状态

#### 实现

使用嵌入向量模型来进行情感分析。通过计算给定对话文本与预先定义的“友好”和“矛盾”情感的相似度来实现。下面是代码的详细解释：

1.首先，使用 Open AI 的 API 来创建两个固定的嵌入向量：

- `friendly_embedding`：代表“友好,让人开心,充满信任的对话”这一概念的嵌入向量。
- `adversarial_embedding`：代表“矛盾,让人难受,充满怀疑的对话”这一概念的嵌入向量。

这些嵌入向量被用作参照点，用于后续与输入对话的嵌入向量进行比较。

2.`sentiment_analysis` 函数接受一个字符串参数 `dialog`，这是需要进行情感分析的对话文本。

3.在这个函数内部，首先计算 `dialog` 文本的嵌入向量 `dialog_embedding`。接着，使用余弦相似度公式来计算 `dialog_embedding` 与两个固定嵌入向量（`friendly_embedding` 和 `adversarial_embedding`）之间的相似度。余弦相似度是通过点积除以向量长度（范数）的乘积来计算的。余弦相似度范围从 -1（完全不同）到 1（完全相同）。因此，计算得到的两个相似度值分别反映了输入对话与“友好”和“矛盾”情感的匹配程度。

4.最后，计算这两个相似度的差值作为最终的情感分析得分 `score`。如果 `score` 是正值，这意味着对话更倾向于“友好”情感；如果是负值，则表示对话更倾向于“矛盾”情感。

这种方法可以通过调准基准概念和其他工程手段，来实现更好的效果。（详见代码）

## 部署

1.点击打包文件的CyberPsychosis.exe即可直接启动（只发给了对应老师邮件，没有放在Github）

![image-20240713174851497](C:/Users/yuaner/AppData/Roaming/Typora/typora-user-images/image-20240713174851497.png)

2.下载本仓库后导入Unity 2021.3.30.f1对src部分进行调试即可

## API文档

Open AI接口详情见[API文档](https://platform.openai.com/docs/guides/text-generation/chat-completions-api)