using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;

 
 public class AudioDetection2 : MonoBehaviour
 {
     public float RmsValue;
     public float DbValue;
     public float PitchValue;
     private bool micConnected = false;  
     private bool recording;
 
     private const int QSamples = 1024;
     private const float RefValue = 0.1f;
     private const float Threshold = 0.02f;
     private int minFreq = 10;
     private int maxFreq = 44100;
 
     float[] _samples;
     private float[] _spectrum;
     private float _fSample;

     public int numClaps = 0;
     private AudioSource _audio;
     private List<double> timestamps; // timestamps of claps, starting at 0 for first clap
     private double startTime = -1; // time of first detected clap
     private double rhythmLengthTime = 8; // change this later

     void Awake()
     {
         _audio = GetComponent<AudioSource>();
     }
 
     void Start()
     {
         _audio.clip = Microphone.Start(null, true, 10, 44100);
         recording = true;
         _samples = new float[QSamples];
         _spectrum = new float[QSamples];
         _fSample = AudioSettings.outputSampleRate;
         startTime = GetCurrentTime();
        _audio.loop = true;
        _audio.mute = false;
        numClaps = 0;
        timestamps = new List<double>();
        while (!(Microphone.GetPosition(null) > 0)) {
             _audio.Play();
         }
     }
 
     void Update()
     {
        if(!recording) {
            return;
        }
        if(startTime > 0 && (GetCurrentTime() - startTime) > rhythmLengthTime) {
             Microphone.End(null); //Stop the audio recording
             Debug.Log("END RECORDING");
             Debug.Log(numClaps + " CLAPS DETECTED");
             recording = false;
        } else {
            AnalyzeSound();
        }
     }
 
     void AnalyzeSound()
     {
         _audio.GetOutputData(_samples, 0); // fill array with samples
         int i;
         float sum = 0;
         for (i = 0; i < QSamples; i++)
         {
             sum += _samples[i] * _samples[i]; // sum squared samples
         }
         RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average
         DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB
         if (DbValue < -160) DbValue = -160; // clamp it to -160dB min
                                             // get sound spectrum
         _audio.GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
         float maxV = 0;
         var maxN = 0;
         for (i = 0; i < QSamples; i++)
         { // find max 
             if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                 continue;
 
             maxV = _spectrum[i];
             maxN = i; // maxN is the index of max
         }
         float freqN = maxN; // pass the index to a float variable
         if (maxN > 0 && maxN < QSamples - 1)
         { // interpolate index using neighbours
             var dL = _spectrum[maxN - 1] / _spectrum[maxN];
             var dR = _spectrum[maxN + 1] / _spectrum[maxN];
             freqN += 0.5f * (dR * dR - dL * dL);
         }
         PitchValue = freqN * (_fSample / 2) / QSamples; // convert index to frequency
         if(PitchValue > 500) {
             Debug.Log(PitchValue);
             numClaps++;
         }
     }

     double GetCurrentTime() {
        return (DateTime.Now.ToUniversalTime() - new DateTime (1970, 1, 1)).TotalSeconds;
    }
 }