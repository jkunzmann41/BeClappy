using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using System;

public class RhythmAudio : MonoBehaviour
{
    private List<float> noteDurations = new List<float> { 1f, .5f, .5f, 1f, .5f, .5f};
    private AudioSource _audio;
    private double rhythmLengthTime = 5; // change this later
    public int numTaps = 0;
    private List<double> timestamps; // timestamps of taps, starting at 0 for tap
    private double startTime; // time of first detected tap
    private bool keepPlaying;
    private int clapIndex;
    private int totalClaps;

    void Awake()
    {

    }
    void Start()
    {
        keepPlaying = true;
        clapIndex = 0;
        startTime = -1;
        totalClaps = noteDurations.Count;
        timestamps = new List<double>(); 
        _audio = GetComponent<AudioSource>();
        StartCoroutine(SoundOut());
    }
    
    IEnumerator SoundOut()
     {
         while (clapIndex < totalClaps){
            Debug.Log("clap at " + clapIndex);
            Debug.Log(GetCurrentTime());
            _audio.Play();
            yield return new WaitForSeconds(noteDurations[clapIndex++]); 

         }
     }
    double GetCurrentTime() {
        return (DateTime.Now.ToUniversalTime() - new DateTime (1970, 1, 1)).TotalSeconds;
    }
}