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
        if (btn_name == "IceCream")
        {
            selected_index = (int)Rhythm.ICECREAM;

        } else if (btn_name == "RunPony")
        {
            selected_index = (int)Rhythm.RUNPONY;

        } else if (btn_name == "Alligator")
        {
            selected_index = (int)Rhythm.ALLIGATOR;

        }

        Globals.Instance.setRhythm(selected_index);
        //Globals.Instance.setStars(button, 2);
        Debug.Log("rhythm " + selected_index + " selected " + btn_name);

     }
}
