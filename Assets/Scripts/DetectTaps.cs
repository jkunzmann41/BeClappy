// This class is used for detecting claps (taps), scoring, and providing feedback to the user
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class DetectTaps : MonoBehaviour
{
    // These are global and should be extracted out and referenced by other scripts
    public enum Rating {AWESOME, GREAT, GOOD, MISSED, EARLY, LATE};
    // value is the max offset from the actual time the tap was supposed to happen
    public Dictionary<Rating, double> ratingMap = new Dictionary<Rating, double> {
        {Rating.AWESOME, .1},
        {Rating.GREAT, .2},
        {Rating.GOOD, .3},
        {Rating.MISSED, 1},
        {Rating.LATE, 1},
        {Rating.EARLY, 1}
    };

    public Dictionary<Rating, String> rateDisplayMap = new Dictionary<Rating, String> {
        {Rating.AWESOME, "Awesome!"},
        {Rating.GREAT, "Great!"},
        {Rating.GOOD, "Good!"},
        {Rating.MISSED, "Missed"},
        {Rating.LATE, "Late"},
        {Rating.EARLY, "Early"}
    };

    public Dictionary<Rating, int> rateStarsMap = new Dictionary<Rating, int> {
        {Rating.AWESOME, 3},
        {Rating.GREAT, 2},
        {Rating.GOOD, 1},
        {Rating.MISSED, 0},
        {Rating.LATE, 0},
        {Rating.EARLY, 0}
    };

    public Dictionary<int, String> starsTextMap = new Dictionary<int, String> {
        {3, "Awesome!"},
        {2, "Great!"},
        {1, "Good!"},
        {0, "Try Again!"}
    };


    // end of globals

    public RhythmGenerate rg;
    private List<Note> rhythm;
    private List<double> expNoteTimes;
    private List<Rating> noteRatings;
    private AudioSource _audio;
    public int numTaps = 0;
    private List<double> timestamps; // timestamps of taps, starting at 0 for tap
    private double rhythmStartTime; // time of first detected tap
    private int noteIndex; // next note to be played
    private double noteStartTime; // time that most recent tap occurred
    private double expectedClapTime; // time to expect the next clap
    private bool inprogress;
    private double window;
    public Button starsBtn;
    public Button feedbackBtn;
    public Button playBtn;
    public GameObject Metronome;

    void Awake()
    {
    }
    void OnEnable()
    {
        if(inprogress) {
            return;  // already in progress
        }
        feedbackBtn.gameObject.SetActive(false);
        starsBtn.gameObject.SetActive(false);
        rg = new RhythmGenerate();
        rhythm = rg.getRepeatedRhythm(Globals.Instance.curRhythm, Globals.Instance.getRepititions());
        noteIndex = 0;
        noteRatings = Enumerable.Repeat(Rating.MISSED, rhythm.Count).ToList();
        expNoteTimes = rg.computeExpectedNoteTimes(rhythm);
        rhythmStartTime = -1;
        timestamps = new List<double>(); 
        _audio = GetComponent<AudioSource>();
        inprogress = true;
        window = 1; // stop accepting taps x seconds after last tap was supposed to occur
    }

    void Update()
    {   
        if(!inprogress) {
            return;  // end script!
        }

        if(rhythmStartTime > 0 && (GetCurrentTime() - rhythmStartTime > expNoteTimes[expNoteTimes.Count - 1] + window))
            HandleEndRhythm(); 
      
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
    }

    double GetCurrentTime() {
        return (DateTime.Now.ToUniversalTime() - new DateTime (1970, 1, 1)).TotalSeconds;
    }

    // Returns index of nearest unattempted note by comparing time of clap/tap to the expected clap/tap times. If both surrounding notes already attempted, returns -1
    int NearestNote(double time) {
        int nearestLarger = ~expNoteTimes.BinarySearch(time);
        int nearestSmaller = nearestLarger - 1;
        if(nearestLarger == expNoteTimes.Count) {
            // then nearestSmaller is index of last element in expNoteTimes
            if(noteRatings[nearestSmaller] == Rating.MISSED)
                return nearestSmaller;
            return -1;
        }
        // if earlier note has already been rated
        if (noteRatings[nearestSmaller] != Rating.MISSED) {
            if(noteRatings[nearestLarger] != Rating.MISSED)
                return -1;
            return nearestLarger; 
        }
        // neither neighboring notes have been detected. Return index of closest note in time
        return expNoteTimes[nearestLarger] - time < time - expNoteTimes[nearestSmaller] ? nearestLarger : nearestSmaller;
          
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
        if(actual < expected)
            return Rating.EARLY;
        else
            return Rating.LATE;
    }

    // Render the corresponding text to the GUI
    void DisplayRating(Rating r) {
        Debug.Log(rateDisplayMap[r]);
        Text ButtonText = feedbackBtn.GetComponentInChildren<Text>();
        ButtonText.text = rateDisplayMap[r];
        feedbackBtn.gameObject.SetActive(true);
    }

    void HandleEndRhythm() {
        inprogress = false;
        Metronome.SetActive(false);
        feedbackBtn.gameObject.SetActive(false);
        int numStars = computeNumStars();
        Globals.Instance.setStars(starsBtn, numStars);
        if(Globals.Instance.challenge) {
            Globals.Instance.setBestScore(numStars);
            Debug.Log("NUM STARS AWARDED: " + numStars);
        }
        Text ButtonText = starsBtn.GetComponentInChildren<Text>();
        ButtonText.text = starsTextMap[numStars];
        Debug.Log("challenge mode? " + Globals.Instance.challenge);
        starsBtn.gameObject.SetActive(true);
    
        this.gameObject.SetActive(false);
        this.enabled = false;
    }

    // Computing number of stars to display after a rhythm in challenge mode
    int computeNumStars() {
        int sum = 0;
        foreach (Rating r in noteRatings) {
            sum += rateStarsMap[r];
        }
        double starsAvg = 1.0*sum/noteRatings.Count;
        return (int)Math.Round(starsAvg);
    }

}