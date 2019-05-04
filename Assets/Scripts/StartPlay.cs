using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPlay : MonoBehaviour
{
    public Button btnPlay;

    public void startGame()
    {
        btnPlay.gameObject.SetActive(false);
        Debug.Log("Starting the game");
    }
}
