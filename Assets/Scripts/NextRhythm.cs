using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRhythm : MonoBehaviour
{
    public int curIndex = Globals.Instance.rhythmIndex;
    int totalNumberRhythms = 3;
    public void nextRhythmLeft()
    {
        int next = curIndex - 1;
        if (next == -1)
        {
            next = 2;
        }
        Globals.Instance.setRhythm(next);
    }
    public void nextRhythmRight()
    {
        int next = curIndex + 1;
        if (next == totalNumberRhythms)
        {
            next = 0;
        }
        Globals.Instance.setRhythm(next);
    }
    public void nextRepeat()
    {

    }
}
