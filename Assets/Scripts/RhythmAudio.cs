using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class RhythmAudio : MonoBehaviour
{
    private RhythmGenerate rg;
    private List<float> noteDurations;
    private AudioSource _audio;
    private double rhythmLengthTime = 5; // change this later
    public int numTaps = 0;
    private List<double> timestamps; // timestamps of taps, starting at 0 for tap
    private bool keepPlaying;
    private int clapIndex;
    private int totalClaps;
    private bool first;
    public Button playButton;
    public Button starsBtn;
    public Button feedbackBtn;

    void Awake()
    {

    }
    void Start()
    {
        starsBtn.gameObject.SetActive(false);
        feedbackBtn.gameObject.SetActive(false);
        rg = new RhythmGenerate();
        List<Note> rep = rg.getRepeatedRhythm(Globals.Instance.getRhythm(), Globals.Instance.getRepititions());
        noteDurations = rg.computeNoteDurations(rep);
        first = true;
        clapIndex = 0;
        totalClaps = noteDurations.Count;
        timestamps = new List<double>(); 
        _audio = GetComponent<AudioSource>();
        StartCoroutine(SoundOut());
    }
    
    IEnumerator SoundOut()
     {
         playButton.gameObject.SetActive(false);
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
         playButton.gameObject.SetActive(true);
     }
    double GetCurrentTime() {
        return (DateTime.Now.ToUniversalTime() - new DateTime (1970, 1, 1)).TotalSeconds;
    }
}