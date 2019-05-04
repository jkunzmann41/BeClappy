using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notes_circles : MonoBehaviour
{
    public Button btn;
    public Sprite circles;
    public Sprite notes;
    public Image play;

    private bool click = true;

    void Start() {
        Image im2 = btn.GetComponent<Image>();
        im2.overrideSprite = notes;
        click = true;
        Globals.Instance.notes = true;
        Globals.Instance.setImageRhythm(play);
    } 
    public void NotesOrCircles()
    {

        if (click)
        {
            Image im1 = btn.GetComponent<Image>();
            im1.overrideSprite = circles;
            click = false;
            Globals.Instance.notes = false;
            Globals.Instance.setImageRhythm(play);
        }
        else
        {
            Image im2 = btn.GetComponent<Image>();
            im2.overrideSprite = notes;
            click = true;
            Globals.Instance.notes = true;
            Globals.Instance.setImageRhythm(play);
        }
    }
}
