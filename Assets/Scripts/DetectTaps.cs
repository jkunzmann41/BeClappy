using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using System;

public class DetectTaps : MonoBehaviour
{
    private List<double> rhythm = new List<double> {0, 1, 1.5, 2, 3, 3.5};
    private AudioSource _audio;
    private double rhythmLengthTime = 5; // change this later
    public int numTaps = 0;
    private List<double> timestamps; // timestamps of taps, starting at 0 for tap
    private double startTime; // time of first detected tap


    void Awake()
    {

    }
    void Start()
    {
        startTime = -1;
        timestamps = new List<double>(); 
        _audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _audio.Play();
            if(startTime < 0) {
                startTime = GetCurrentTime();
                timestamps.Add(startTime);
            } else {
                timestamps.Add(GetCurrentTime() - startTime);
            }
            Debug.Log(GetCurrentTime());
        }

        if (Input.GetMouseButtonDown(1))
        {
            // right click to be able to see what timestamps are stored
            foreach (double time in timestamps) {
                Debug.Log(time);
            }

        }
        // if(startTime > 0 && (GetCurrentTime() - startTime) > rhythmLengthTime) {

        // } else {
           
        // }
        
    }

    double GetCurrentTime() {
        return (DateTime.Now.ToUniversalTime() - new DateTime (1970, 1, 1)).TotalSeconds;
    }
}