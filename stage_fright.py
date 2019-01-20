#!/usr/bin/env python3

import sys
import json
from eval_speech import Speech
from watson_developer_cloud import SpeechToTextV1

'''
Usage: stage_fright.py out_file api_key_file
'''

API_KEY = json.loads(open(sys.argv[2], 'r').read())['apiKey']
URL = 'https://stream.watsonplatform.net/speech-to-text/api'

speechToText = SpeechToTextV1(
    iam_apikey=API_KEY,
    url=URL
)

def main():
    if len(sys.argv) != 3:
        exit("Usage: stage_fright.py out_file api_key_file")

    audioFile = open('shortTalk.wav', 'rb')
    speech = Speech(speechToText, audioFile, 'out.txt')

    with open(sys.argv[1], 'w') as outfile:
        data = {"hesitations": speech.getHesitations(),
                "clarity": speech.getSpeechClarity(),
                "transcript": speech.getSpeechTranscript()}
        json.dump(data, outfile, indent=2)


if __name__ == '__main__':
    main()
