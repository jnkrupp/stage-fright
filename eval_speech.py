#!/usr/bin/env python3

class Speech:

    def __init__(self, speechToText, audioFile, outFilePath):
        # Credit here: https://gist.githubusercontent.com/santiagobasulto/bc0e4c2c29ae8086e775a4d666fdbd60/raw/d279159859ca715b9d5b44c3b47e2283b8d69a31/watson_speech_to_text.py
        self.response = speechToText.recognize(audioFile, content_type="audio/wav",
                                   continuous=True, timestamps=False,
                                   max_alternatives=1)

    def getSpeechTranscript(self):
        try:
            return self.response.get_result()['results'][0]['alternatives'][0]['transcript'].replace('%HESITATION', '...')
        except:
            return "(A transcription couldn't be deciphered.)"

    def getHesitations(self):
        try:
            return self.response.get_result()['results'][0]['alternatives'][0]['transcript'].count('%HESITATION')
        except:
            return 0

    def getSpeechClarity(self):
        try:
            return self.response.get_result()['results'][0]['alternatives'][0]['confidence']
        except:
            return 0
