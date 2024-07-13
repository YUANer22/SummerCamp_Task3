import requests

WORKSPACE_ID = 'cyber-psychosis'
YOUR_KEY_HERE = 'MEdMVm9lb2xYc3N1R0lUU1VUQUN3T214UWtEQXV1dWQ6bmxtdmlBTGEyQmJhNXpOMDVyY2NRNDd6OVgxUjZyWjhHTENIa1Uzc09uS2JxdEFXNk9IbU5FUVl6TUtITDNYcA=='

url = 'https://studio.inworld.ai/v1/workspaces/{WORKSPACE_ID}/characters/test:simpleSendText'
headers = {"Content-Type": "application/json", "authorization": "Basic {YOUR_KEY_HERE}"}
myobj = {"character":"workspaces/{WORKSPACE_ID}/characters/test", "text":"hello there!", "endUserFullname":"Tom", "endUserId":"12345"}

x = requests.post(url, json = myobj, headers=headers)

print(x)


'''
curl -X POST https://studio.inworld.ai/v1/workspaces/cyber-psychosis/characters/test:simpleSendText \
-H 'Content-Type: application/json' \
-H 'authorization: Basic MEdMVm9lb2xYc3N1R0lUU1VUQUN3T214UWtEQXV1dWQ6bmxtdmlBTGEyQmJhNXpOMDVyY2NRNDd6OVgxUjZyWjhHTENIa1Uzc09uS2JxdEFXNk9IbU5FUVl6TUtITDNYcA==' \
-d '{"character":"workspaces/cyber-psychosis/characters/test", "text":"hello there!", "endUserFullname":"Player", "endUserId":"1"}'
'''
