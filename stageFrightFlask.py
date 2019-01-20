from flask import Flask
from flask import render_template, request
import io
import os
import pyaudio
import wave
import sys

CHUNK = 1024

app = Flask(__name__)

import random


UPLOAD_FOLDER = os.path.basename('uploads')
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER


@app.route('/')
def hello_world():
    return render_template('index.html')


@app.route('/record')
def record():
    return render_template("record.html")


def loudness():
    pass

@app.route('/upload', methods=['POST'])
def upload_photo():
    file = request.files['audio']
    f = os.path.join(app.config['UPLOAD_FOLDER'], file.filename)
    file.save(f)


    feedback = {}
    return render_template('play.html', feedback)

