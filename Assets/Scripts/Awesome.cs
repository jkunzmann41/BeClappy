using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


[RequireComponent(typeof(AudioSource))]
public class Awesome : MonoBehaviour
{
    public AudioClip awesome; //make sure you assign an actual clip here in the inspector
    public AudioClip great;
    public AudioClip good; 
    public AudioClip tryagain;
    public Text feedback;

    public Dictionary<String, int> starsFeedbackInt = new Dictionary<String, int> {
        {"Awesome!", 3},
        {"Great!", 2},
        {"Good!", 1},
        {"Try Again!", 0}
    };

    //void Start()
    //{
    //    AudioClip clip = tryagain;
    //    switch (starsFeedbackInt[feedback.text])
    //    {
    //        case 0:
    //            clip = tryagain;
    //            break;
    //        case 1:
    //            clip = good;
    //            break;
    //        case 2:
    //            clip = great;
    //            break;
    //        case 3:
    //            clip = awesome;
    //            break;
    //    }
    //    AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2));
    //}
    public void sayIt()
    {
        AudioClip clip = tryagain;
        switch (starsFeedbackInt[feedback.text])
        {
            case 0:
                clip = tryagain;
                break;
            case 1:
                clip = good;
                break;
            case 2:
                clip = great;
                break;
            case 3:
                clip = awesome;
                break;
        }
        AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2));
    }
}
