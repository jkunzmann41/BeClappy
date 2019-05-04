using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToMap : MonoBehaviour
{
    public void changeSceneToMap()
    {
<<<<<<< HEAD
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
=======
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
>>>>>>> UI_experiments
    }
}
