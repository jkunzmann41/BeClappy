using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;

public class AudioDetection : MonoBehaviour
{
    private double rhythmLengthTime = 10; // change this later
    public float sensitivity = 100;
    public float loudness = 0;
    public int numClaps = 0;
    private AudioSource _audio;
    private List<double> timestamps; // timestamps of claps, starting at 0 for first clap
    private double startTime; // time of first detected clap

    private bool recording;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        recording = true;
        _audio.loop = true;
        _audio.mute = false;
        startTime = -1;
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
            loudness = GetAveragedVolume() * sensitivity;
            if(loudness > 7.2){
                numClaps++;
                Debug.Log("SOUND DETECTED");
                Debug.Log(loudness);
                if(startTime < 0) {
                    startTime = GetCurrentTime();
                    timestamps.Add(0);
                } else {
                    timestamps.Add(GetCurrentTime() - startTime);
                }
                Debug.Log("TIME: " + timestamps[timestamps.Count - 1]);       
            }
        }
        
    }
    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    double GetCurrentTime() {
        return (DateTime.Now.ToUniversalTime() - new DateTime (1970, 1, 1)).TotalSeconds;
    }
}