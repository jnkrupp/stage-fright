#!/usr/bin/env python3

class Speech:

    def __init__(self, speechToText, audioFile, outFilePath):
        self.response = speechToText.recognize(audioFile, content_type="audio/wav",
                                   continuous=True, timestamps=False,
                                   max_alternatives=1)

    def getSpeechTranscript(self):
        return self.response.get_result()['results'][0]['alternatives'][0]['transcript'].replace('%HESITATION', '...')

    def getHesitations(self):
        return self.response.get_result()['results'][0]['alternatives'][0]['transcript'].count('%HESITATION')

    def getSpeechClarity(self):
        return self.response.get_result()['results'][0]['alternatives'][0]['confidence']
