using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum Rhythm { ALLIGATOR, RUNPONY, CATCHME };

public class Globals : Singleton<Globals>
{
    public List<int> bestScore = new List<int>() {0, 0, 0};

    public static List<List<Note>> rhythms = new List<List<Note>>() {
       {new List<Note> {Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH }},
       {new List<Note> {Note.QUARTER, Note.EIGHTH, Note.EIGHTH, Note.QUARTER, Note.EIGHTH, Note.EIGHTH}},
       {new List<Note> {Note.EIGHTH, Note.QUARTER, Note.EIGHTH, Note.EIGHTH, Note.QUARTER, Note.EIGHTH}}
    };
    public static List<String> mnemonics = new List<String>() {
        "See you later alligator", 
        "Run pony, run pony", 
        "Catch me go catch me go"
    };

    public int rhythmIndex = (int)Rhythm.RUNPONY;
    public List<Note> curRhythm = rhythms[(int)Rhythm.RUNPONY];
    public string curMnemonic = mnemonics[(int)Rhythm.RUNPONY];
    public bool notes = true;
    public bool challenge = false;

    protected Globals() { }

    public void setRhythm(int i) {
        curRhythm = rhythms[i];
        curMnemonic = mnemonics[i];
        rhythmIndex = i;
    }

    public void setBestScore(int score) {
        if (score > bestScore[rhythmIndex])
            bestScore[rhythmIndex] = score;
        Debug.Log("New best score: " + score);
    }

    // set "star" number of stars for the visual
    public void setStars(Button btn, int star)
    {
        Sprite stars = Resources.Load<Sprite>("Images/" + star + "star");
        Image buttonImage = btn.GetComponent<Image>();
        Image[] images = btn.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image != buttonImage)
            {
                image.sprite = stars;
                if(challenge)
                    image.enabled = true;
                else
                    image.enabled = false;
                break;
            }
        }
    }

    // set image for selected rhythm
    public void setImageRhythm(Image img)
    {
        Sprite playZone;
        int i = findIndex();
        if (notes)
        {
            playZone = Resources.Load<Sprite>("Images/" + i + "notes");
        }
        else
        {
            playZone = Resources.Load<Sprite>("Images/" + i + "circles");
        }
        img.sprite = playZone;

    }

    private int findIndex()
    {
        int index = 0;
        for (int i = 0; i < mnemonics.Count; i++)
        {
            if (mnemonics[i] == curMnemonic)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    // Number of repititions to perform the rhythm, depending on the mode
    public int getRepititions() {
        if(challenge) {
            return 3;
        } else return 1;
    }

    public List<Note> getRhythm() {
        return curRhythm;
    }

    // Return number of stars for the rhythm at index
    public int getStars(int index) {
        return bestScore[index];
    }

    public bool isChallengeMode() {
        return challenge;
    }

    public void SetChallenge(bool val) {
        challenge = val;
        Debug.Log("CHALLENGE STATE: " + challenge);
    }


    //public void setNotes(bool b)
    //{
    //    notes = b;
    //}

    //public void setMode(bool b)
    //{
    //    challenge = b;
    //}
}