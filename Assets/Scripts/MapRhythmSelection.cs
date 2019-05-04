using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class MapRhythmSelection : MonoBehaviour
{

     public void OnClicked(Button button)
     {
        var selected_index = 0;
        string btn_name = button.name;
        if (btn_name == "CatchMe")
        {
            selected_index = (int)Rhythm.CATCHME;

        } else if (btn_name == "RunPony")
        {
            selected_index = (int)Rhythm.RUNPONY;

        } else if (btn_name == "Alligator")
        {
            selected_index = (int)Rhythm.ALLIGATOR;

        }
        Globals.Instance.setRhythm(selected_index);
        Debug.Log("rhythm " + selected_index + " selected " + btn_name);

     }
}
