using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Globals: Singleton<Globals>
{
    public static List<List<Note>> rhythms = new List<List<Note>>() {
       {new List<Note> {Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH }},
       {new List<Note> {Note.QUARTER, Note.EIGHTH, Note.EIGHTH, Note.QUARTER, Note.EIGHTH, Note.EIGHTH}},
       {new List<Note> {Note.QUARTER, Note.QUARTER, Note.PAUSE, Note.QUARTER}}
    };
    public List<Note> curRhythm;

    protected Globals() { }

    public void setRhythm(int i) {
        curRhythm = rhythms[i];
    }
    public void setStars(Button btn, int star)
    {
        Sprite stars = Resources.Load<Sprite>("Images/" + star + "star");

        //Image image = btn.GetComponentsInChildren;
        //image.GetComponent<Image>().sprite = stars;

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
        //button.transform.SetParent(shopButtonContrainer.transform, false);


    }
}