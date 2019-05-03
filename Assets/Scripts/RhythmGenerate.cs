using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum Note {WHOLE, HALF, QUARTER, EIGHTH, SIXTEENTH};

public class RhythmGenerate {
    public int bpm = 60;

    // value is duration of the note (number of beats)
    public Dictionary<Note, double> noteMap = new Dictionary<Note, double> {
        {Note.WHOLE, 4}, 
        {Note.HALF, 2},
        {Note.QUARTER, 1},
        {Note.EIGHTH, .5},
        {Note.SIXTEENTH, .25}
    };

    public RhythmGenerate() {

    }

    public List<Note> getRepeatedRhythm(List<Note> baseRhythm, int repititions) {
        List<Note> repeated = new List<Note>();
        for (int i= 0; i < repititions; i++) {
            repeated.AddRange(baseRhythm);
        }
        return repeated;
    }

    public List<double> computeExpectedNoteTimes(List<Note> rhythm) {
        double offset = 0;
        List<double> noteTimes = new List<double>();
        foreach (Note note in rhythm) {
            noteTimes.Add(offset);
            offset += noteMap[note]/(bpm/60); // add note's beat duration divided by beats per second to get time offset
            Debug.Log(offset);
        }
        return noteTimes;
    }

    public List<float> computeNoteDurations(List<Note> rhythm) {
        List<float> noteDurs= new List<float>();
        foreach (Note note in rhythm) {
            noteDurs.Add( (float) noteMap[note]/(bpm/60));
        }
        return noteDurs;
    }

}