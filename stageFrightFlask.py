from flask import Flask
from flask import render_template, request
import io
import os

import sys

# for plotting graphs
import matplotlib.pyplot as plt
from scipy.io import wavfile as wav
import base64

# for speech to text
import json
from watson_developer_cloud import SpeechToTextV1
from eval_speech import Speech

app = Flask(__name__)


with open("../apiKey.json") as keyFile:
    API_KEY = json.loads(keyFile.read())['apiKey']
    URL = 'https://stream.watsonplatform.net/speech-to-text/api'

UPLOAD_FOLDER = os.path.join('uploads')
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER



# Credit here: https://cloud.ibm.com/apidocs/speech-to-text?language=python
speechToText = SpeechToTextV1(
    iam_apikey=API_KEY,
    url=URL
)


@app.route('/')
@app.route('/index')
def hello_world():
    return render_template('index.html')

@app.route('/page2')
def page2():
    return render_template("page2.html")


@app.route('/try-it')
def try_it():
    return render_template("try-it.html")

@app.route('/upload', methods=['POST'])
def upload_photo():
    audioFileUser = request.files['audio']
    f = os.path.join(app.config['UPLOAD_FOLDER'], audioFileUser.filename)
    audioFileUser.save(f)

    # https://technovechno.com/creating-graphs-in-python-using-matplotlib-flask-framework-pythonanywhere/
    # show dynamic images
    img = io.BytesIO()
    rate, data = wav.read(f)
    plt.plot(data)
    plt.title("Sound Waves over Time")
    plt.savefig(img, format='png')
    img.seek(0)
    graph_url = base64.b64encode(img.getvalue()).decode()
    plt.close()
    print(audioFileUser)
    print(audioFileUser.read())
    # watson
    audioFile = open(f, 'rb').read()
    speech = Speech(speechToText, audioFile, 'out.txt')

    hesitations = speech.getHesitations()
    clarity = speech.getSpeechClarity()
    transcript = speech.getSpeechTranscript()

    return render_template('stat.html', graph_url='data:image/png;base64,{}'.format(graph_url), hesitations=hesitations, clarity=clarity*100, transcript=transcript)


