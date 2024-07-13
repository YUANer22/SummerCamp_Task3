import http.client
import json

conn = http.client.HTTPConnection("api.openai-proxy.com")
playload = json.dumps({
    "model": "gpt-3.5-turbo",
    "messages": [{"role": "user", "content": "你好呀!"}],
})
headers = {
    'Content-Type': 'application/json',
    'Authorization': 'Bearer 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', 
    'User-Agent': 'Apifox/1.0.0(Python/3.6.11)'
}


conn.request("POST", "/v1/chat/completions", playload, headers)
res = conn.getresponse()
data = res.read().decode()
print(data)