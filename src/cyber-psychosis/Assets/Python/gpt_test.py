'''
curl https://api.openai-proxy.com/v1/chat/completions \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer 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" \
  -d '{
    "model": "gpt-3.5-turbo",
    "messages": [{"role": "user", "content": "Hello!"}]
  }'

'''


import socket
import sys
import os
import http.client
import json

HOST = '127.0.0.1'
PORT = 31415

conn = http.client.HTTPConnection("api.openai-proxy.com")
playload = json.dumps({
    "model": "gpt-3.5-turbo",
    "messages": [{"role": "user", "content": "你好呀!"}]
})
headers = {
    'Content-Type': 'application/json',
    'Authorization': 'Bearer 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'
}

with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as s:

    s.bind((HOST, PORT))

    print('Listening on', (HOST, PORT))
    while True:
        sys.stdout.flush()
        data, addr = s.recvfrom(1024)
        if(data.decode('utf-8')):
            print(data.decode('utf-8'))
            # chat.messages.append({"role": "user", "content": data.decode()})
            # answer = chat.ask_gpt()
            conn.request("POST", "/v1/chat/completions", playload, headers)
            res = conn.getresponse()
            data = res.read().decode()
            print(data)
            print(f"[ChatGPT]{data}")
