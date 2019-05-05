using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Practice_Challenge : MonoBehaviour
{
    public Button btn;
    public Sprite practice;
    public Sprite challenge;
    public void PracticeOrChallenge()
    {

        if (Globals.Instance.challenge)
        {
            Image im1 = btn.GetComponent<Image>();
            im1.overrideSprite = practice;
            Globals.Instance.challenge = false;
            //Debug.Log("Click = " + click);
        }
        else
        {
            Image im2 = btn.GetComponent<Image>();
            im2.overrideSprite = challenge;
            Globals.Instance.challenge = true;
            //Debug.Log("Click = " + click);
        }
    }
}
