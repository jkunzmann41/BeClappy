using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notes_circles : MonoBehaviour
{
    public Button btn;
    public Sprite circles;
    public Sprite notes;
    private bool click = true;
    public void NotesOrCircles()
    {

        if (click)
        {
            Image im1 = btn.GetComponent<Image>();
            im1.overrideSprite = circles;
            click = false;
        }
        else
        {
            Image im2 = btn.GetComponent<Image>();
            im2.overrideSprite = notes;
            click = true;
        }
    }
}
