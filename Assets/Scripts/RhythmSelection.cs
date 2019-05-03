using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class RhythmSelection : MonoBehaviour
{
    public Button b1, b2;

    void Awake()
    {

    }
    void Start()
    {
        b1.onClick.AddListener(delegate {HandleRhythmSelection(0); });
        b2.onClick.AddListener(delegate {HandleRhythmSelection(1); });
    }

    void HandleRhythmSelection(int i)
    {
        Globals.Instance.setRhythm(i);
        Debug.Log("rhythm " + i + " selected");
    }
   
}