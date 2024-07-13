import json

# 你的 OpenAI API 密钥
api_key = "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"

# 创建一个字典来存储 API 密钥
data = {'api': api_key}

# 将字典写入 JSON 文件
with open('openai_key.json', 'w') as f:
    json.dump(data, f)