using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DoubleClick : MonoBehaviour, IPointerDownHandler
{
    public AudioSource m_MyAudioSource;
    float m_MySliderValue = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int clicked = 0;
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
            //Create a horizontal Slider that controls volume levels. Its highest value is 1 and lowest is 0
            m_MySliderValue = GUI.HorizontalSlider(new Rect(25, 25, 200, 60), m_MySliderValue, 0.0F, 1.0F);
            //Makes the volume of the Audio match the Slider value
            m_MyAudioSource.volume = m_MySliderValue;
            //Debug.Log("Double CLick: " + this.GetComponent<RectTransform>().name);
            ////var set = this.gameObject.AddComponent(System.Type.GetType("Settings.cs"));
            ////set.OpenSettings();
            //Debug.Log("Opening settings...");

        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        //Settings sn = new Settings();
        //sn.OpenSettings();

    }
}
