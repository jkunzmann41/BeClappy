using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TapDetection : MonoBehaviour
{
    private List<double> rhythm = new List<double> {0, 1, 1.5, 2, 3, 3.5};

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
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(startTime < 0) {
                startTime = GetCurrentTime();
                timestamps.Add(startTime);
            } else {
                timestamps.Add(GetCurrentTime() - startTime);
            }
            Debug.Log("Pressed left click.");
        }

        if (Input.GetMouseButtonDown(1))
        {
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