using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Globals : Singleton<Globals>
{
    public static List<List<Note>> rhythms = new List<List<Note>>() {
       {new List<Note> {Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH }},
       {new List<Note> {Note.QUARTER, Note.EIGHTH, Note.EIGHTH, Note.QUARTER, Note.EIGHTH, Note.EIGHTH}},
       {new List<Note> {Note.QUARTER, Note.QUARTER, Note.PAUSE, Note.QUARTER}}
    };
    public static List<String> mnemonics = new List<String>() {
        "See you later alligator", 
        "Run pony, run pony", 
        "Ice cream shhh cone"
        };

    public List<Note> curRhythm = rhythms[1];
    public string curMnemonic = mnemonics[1];
    public bool notes = true;
    public bool challenge = true;

    protected Globals() { }

    public void setRhythm(int i) {
        curRhythm = rhythms[i];
        curMnemonic = mnemonics[i];
    }

    // set "star" number of stars on a map of progress
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
                break;
            }
        }

    }

    // set image for selected rhythm
    public void setImageRhythm(Image img)
    {
        Sprite playZone;
        int i = findIndex();
        Debug.Log("FOUND INDEX ++++++ " + i);
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

    //public void setNotes(bool b)
    //{
    //    notes = b;
    //}

    //public void setMode(bool b)
    //{
    //    challenge = b;
    //}
}