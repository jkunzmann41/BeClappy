using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToMap : MonoBehaviour
{
    void OnMouseUp()
    {
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }
}
