using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPlay : MonoBehaviour
{
    public GameObject TapScript;
    public GameObject Metronome;
    public Button btnPlay;
    DetectTaps tapScript;

    public void startGame()
    {
        btnPlay.gameObject.SetActive(false);
        Debug.Log("Starting the game");
        tapScript = TapScript.GetComponent<DetectTaps>();
        TapScript.SetActive(true);
        tapScript.enabled = true;
    }
}
