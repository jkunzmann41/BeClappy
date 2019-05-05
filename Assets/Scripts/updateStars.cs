using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateStars : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button[] btns = FindObjectsOfType<Button>();
        foreach (Button b in btns)
        {
            if (b.name == "Alligator") 
            {
                Globals.Instance.setStars(b, Globals.Instance.bestScore[(int)Rhythm.ALLIGATOR]);
            } else if (b.name == "RunPony") 
            {
                Globals.Instance.setStars(b, Globals.Instance.bestScore[(int)Rhythm.RUNPONY]);
            }
            else if (b.name == "CatchMe")
            {
                Globals.Instance.setStars(b, Globals.Instance.bestScore[(int)Rhythm.CATCHME]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
