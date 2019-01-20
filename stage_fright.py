#!/usr/bin/env python3

import sys
import json
from eval_speech import Speech
from watson_developer_cloud import SpeechToTextV1

print(sys.argv[1])
API_KEY = json.loads(open(sys.argv[1], 'r').read())['apiKey']
URL = 'https://stream.watsonplatform.net/speech-to-text/api'

# API_KEY = open(sys.argv[1], 'r').read()

speechToText = SpeechToTextV1(
    iam_apikey=API_KEY,
    url=URL
)

# speech_to_text = SpeechToTextV1(
#     username='',
#     password='',
#     url=URL
# )

def main():
    audioFile = open("shortTalk.wav", "rb")
    speech = Speech(speechToText, audioFile, 'out.txt')

if __name__ == '__main__':
    main()