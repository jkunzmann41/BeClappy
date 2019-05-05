using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextRhythm : MonoBehaviour
{
    RhythmAudio aud;
    public GameObject AudioScript;
    DetectTaps tapScript;
    public GameObject TapScript;
    public Image playZone;
    Button playButton;


    public void nextRhythmLeft()
    {
        int next = Globals.Instance.rhythmIndex - 1;
        if (next == -1)
        {
            next = 2;
        }
        Globals.Instance.setRhythm(next);
        Globals.Instance.setImageRhythm(playZone);
        StartNext();
    }
    public void nextRhythmRight()
    {
        int next = Globals.Instance.rhythmIndex + 1;
        if (next == Globals.Instance.numRhythms())
        {
            next = 0;
        }
        Globals.Instance.setRhythm(next);
        Globals.Instance.setImageRhythm(playZone);
        Debug.Log("index is now : " + Globals.Instance.rhythmIndex);
        StartNext();
    }
    public void nextRepeat()
    {
        // playButton.gameObject.SetActive(true);
        tapScript = TapScript.GetComponent<DetectTaps>();
        TapScript.SetActive(true);
        tapScript.enabled = true;
    }

    public void StartNext() {
        aud = AudioScript.GetComponent<RhythmAudio>();
        aud.enabled = false;
        AudioScript.SetActive(true);
        aud.enabled = true;
        
    }
}
