using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using System;
using System.Linq;

public class DetectTaps : MonoBehaviour
{
    // These are global and should be extracted out and referenced by other scripts
    public double bpm;
    public enum Rating {AWESOME, GREAT, GOOD, MISSED};
    // value is the max offset from the actual time the tap was supposed to happen
    public Dictionary<Rating, double> ratingMap = new Dictionary<Rating, double> {
        {Rating.AWESOME, .1},
        {Rating.GREAT, .15},
        {Rating.GOOD, .2},
        {Rating.MISSED, 1}
    };

    public Dictionary<Rating, String> rateDisplayMap = new Dictionary<Rating, String> {
        {Rating.AWESOME, "---Awesome---"},
        {Rating.GREAT, "---Great---"},
        {Rating.GOOD, "---Good---"},
        {Rating.MISSED, "---Missed---"}
    };


    public enum Note {WHOLE, HALF, QUARTER, EIGHTH, SIXTEENTH};
    // value is duration of the note (number of beats)
    public Dictionary<Note, double> noteMap = new Dictionary<Note, double> {
        {Note.WHOLE, 4}, 
        {Note.HALF, 2},
        {Note.QUARTER, 1},
        {Note.EIGHTH, .5},
        {Note.SIXTEENTH, .25}
    };
    // end of globals

    private List<Note> rhythm = new List<Note> {Note.HALF, Note.QUARTER, Note.QUARTER};
    private List<double> expNoteTimes;
    private List<Rating> noteRatings;
    private AudioSource _audio;
    public int numTaps = 0;
    private List<double> timestamps; // timestamps of taps, starting at 0 for tap
    private double rhythmStartTime; // time of first detected tap
    private int noteIndex; // next note to be played
    private double noteStartTime; // time that most recent tap occurred
    private double expectedClapTime; // time to expect the next clap

    void Awake()
    {

    }
    void Start()
    {
        noteRatings = Enumerable.Repeat(Rating.MISSED, rhythm.Count).ToList();
        bpm = 60.0F;
        expNoteTimes = computeExpectedNoteTimes(rhythm);
        rhythmStartTime = -1;
        expectedClapTime = 0;
        timestamps = new List<double>(); 
        _audio = GetComponent<AudioSource>();
    }
    void Update()
    {      
        if (Input.GetMouseButtonDown(0))
        {
            Rating rate = Rating.MISSED;
            double time;
            _audio.Play();
            if(rhythmStartTime < 0) {
                // first note tapped
                rhythmStartTime = GetCurrentTime();
                time = 0;
                timestamps.Add(time);
                rate = Rating.AWESOME; // automatically get first one right
                noteRatings[noteIndex] = rate;
                Debug.Log("Expected time: " + time);
                Debug.Log("Actual time: " + time);
            } else {
                time = GetCurrentTime() - rhythmStartTime;
                timestamps.Add(time);
                int noteIndex = NearestNote(time);
                if(noteIndex >= 0) {
                    rate = ComputeRating(expNoteTimes[noteIndex], time);
                    noteRatings[noteIndex] = rate;
                }   
            }
            DisplayRating(rate);
        }

        if (Input.GetMouseButtonDown(1))
        {
            // right click to be able to see what timestamps are stored
            foreach (double time in timestamps) {
                Debug.Log(time);
            }

        }
        
    }

    double GetCurrentTime() {
        return (DateTime.Now.ToUniversalTime() - new DateTime (1970, 1, 1)).TotalSeconds;
    }

    // computes Awesome, Great, Good, or Missed rating for the note
    Rating ComputeRating(double expected, double actual) {
        double absDiff = Math.Abs(expected - actual);
        Debug.Log("Expected time: " + expected);
        Debug.Log("Actual time: " + actual);
        if(absDiff <= ratingMap[Rating.AWESOME]) 
            return Rating.AWESOME;
        if(absDiff <= ratingMap[Rating.GREAT])
            return Rating.GREAT;
        if(absDiff <= ratingMap[Rating.GOOD])
            return Rating.GOOD;
        return Rating.MISSED;
    }

    List<double> computeExpectedNoteTimes(List<Note> rhythm) {
        double offset = 0;
        List<double> noteTimes = new List<double>();
        foreach (Note note in rhythm) {
            noteTimes.Add(offset);
            offset += noteMap[note]/(bpm/60); // add note's beat duration divided by beats per second to get time offset
        }
        return noteTimes;
    }

    // Binary search for the nearest unrated note based on time of clap/tap. If both surrounding notes already rated, returns -1
    int NearestNote(double time) {
        int nearestLarger = ~expNoteTimes.BinarySearch(time);
        int nearestSmaller = nearestLarger - 1;
        if(nearestLarger == expNoteTimes.Count) {
            if(noteRatings[nearestSmaller] == Rating.MISSED)
                return nearestSmaller;
            return -1;
        }
        if (noteRatings[nearestSmaller] != Rating.MISSED)
            return nearestLarger;
        return expNoteTimes[nearestLarger] - time < time - expNoteTimes[nearestSmaller] ? nearestLarger : nearestSmaller;
          
    }

    // This should render the corresponding image to the GUI
    void DisplayRating(Rating r) {
        Debug.Log(rateDisplayMap[r]);
    }
}