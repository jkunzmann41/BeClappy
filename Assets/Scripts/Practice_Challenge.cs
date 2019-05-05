using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Practice_Challenge : MonoBehaviour
{
    public Button btn;
    public Sprite practice;
    public Sprite challenge;
    private bool click = true;
    public void PracticeOrChallenge()
    {

        if (click)
        {
            Image im1 = btn.GetComponent<Image>();
            im1.overrideSprite = practice;
            click = false;
            Globals.Instance.SetChallenge(false);
            //Debug.Log("Click = " + click);
        }
        else
        {
            Image im2 = btn.GetComponent<Image>();
            im2.overrideSprite = challenge;
            click = true;
            Globals.Instance.SetChallenge(true);
            //Debug.Log("Click = " + click);
        }
    }
}
