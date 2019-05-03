using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using System;

public class RhythmAudio : MonoBehaviour
{
    // public enum Note {WHOLE, HALF, QUARTER, EIGHTH, SIXTEENTH};
    // // value is duration of the note (number of beats)
    // public Dictionary<Note, double> noteMap = new Dictionary<Note, double> {
    //     {Note.WHOLE, 4}, 
    //     {Note.HALF, 2},
    //     {Note.QUARTER, 1},
    //     {Note.EIGHTH, .5},
    //     {Note.SIXTEENTH, .25}
    // };

    private RhythmGenerate rg;
    private List<Note> rhythm = new List<Note> {Note.QUARTER, Note.EIGHTH, Note.EIGHTH};
    //private List<float> noteDurations = new List<float> { .8f, .4f, .4f, .8f, .4f, .4f};
    private List<float> noteDurations;
    private AudioSource _audio;
    private double rhythmLengthTime = 5; // change this later
    public int numTaps = 0;
    private List<double> timestamps; // timestamps of taps, starting at 0 for tap
    private double startTime; // time of first detected tap
    private bool keepPlaying;
    private int clapIndex;
    private int totalClaps;
    private bool first;

    void Awake()
    {

    }
    void Start()
    {
        rg = new RhythmGenerate();
        List<Note> rep = rg.getRepeatedRhythm(rhythm, 4);
        noteDurations = rg.computeNoteDurations(rep);
        first = true;
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
             if(first) {
                  yield return new WaitForSeconds(.5f);
                  first = false;
             }
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