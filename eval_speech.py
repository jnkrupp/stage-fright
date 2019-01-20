#!/usr/bin/env python3

import json
from watson_developer_cloud import SpeechToTextV1

class Speech:

    def __init__(self, speechToText, audioFile, outFilePath):
        with open(outFilePath, 'w') as fp:
            self.result = speechToText.recognize(audioFile, content_type="audio/wav",
                                   continuous=True, timestamps=False,
                                   max_alternatives=1)
            json.dump(self.result, fp, indent=2)

        # speechToText.set_detailed_response(True)
        # response = speechToText.methodName(parameters)
        # self.result = json.dumps(response.get_result(), indent=2)
        # self.headers = response.get_headers()
        # self. statusCode = response.get_status_code()

    def measureVolume(self):
        pass

    def checkBadSpeech(self):
        pass

    def checkPauses(self):
        pass