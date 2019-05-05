using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class MapRhythmSelection : MonoBehaviour
{
    goToMap SceneChanger;

     public void OnClicked(Button button)
     {
        var selected_index = 0;
        string btn_name = button.name;
        Debug.Log("BUTTON " + btn_name);
        if (btn_name == "CatchMe")
        {
            selected_index = (int)Rhythm.CATCHME;
            Debug.Log("CATCH ME: " + selected_index);

        } else if (btn_name == "RunPony")
        {
            selected_index = (int)Rhythm.RUNPONY;

        } else if (btn_name == "Alligator")
        {
            selected_index = (int)Rhythm.ALLIGATOR;

        }
        Globals.Instance.setRhythm(selected_index);
        Debug.Log("rhythm " + selected_index + " selected " + btn_name);
        SceneChanger.changeSceneToMap();

     }
}
