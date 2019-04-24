using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    public void OnPointerDown(PointerEventData data)
    {
        clicked++;
        if (clicked == 1) clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            Debug.Log("Double CLick: " + this.GetComponent<RectTransform>().name);
            //var set = this.gameObject.AddComponent(System.Type.GetType("Settings.cs"));
            //set.OpenSettings();
            Debug.Log("Opening settings...");

        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        //Settings sn = new Settings();
        //sn.OpenSettings();

    }
}
