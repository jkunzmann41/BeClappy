﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class goToMap : MonoBehaviour
{
    public void changeSceneToMap()
    {
        Scene scene;
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 1)
        {
            SceneManager.LoadScene(0);

        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
