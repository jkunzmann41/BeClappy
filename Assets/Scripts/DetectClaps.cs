using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Runtime.InteropServices;


[RequireComponent (typeof (AudioSource))]  
[RequireComponent(typeof(AudioListener))]
public class DetectClaps : MonoBehaviour
{
    //  [DllImport("AudioPluginDemo")]
    // private static extern float PitchDetectorGetFreq(int index);

    //A boolean that flags whether there's a connected microphone  
    private bool micConnected = false;  
  
    //The maximum and minimum available recording frequencies  
    private int minFreq;  
    private int maxFreq;  
  
    //A handle to the attached AudioSource  
    private AudioSource goAudioSource;  

    public string frequency;

    // Start is called before the first frame update
    void Start()
    {
        //Check if there is at least one microphone connected  
        if(Microphone.devices.Length <= 0)  
        {  
            //Throw a warning message at the console if there isn't  
            Debug.LogWarning("Microphone not connected!");  
        }  
        else //At least one microphone is present  
        {  
            //Set 'micConnected' to true  
            micConnected = true;  
  
            //Get the default microphone recording capabilities  
            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);  
  
            //According to the documentation, if minFreq and maxFreq are zero, the microphone supports any frequency...  
            if(minFreq == 0 && maxFreq == 0)  
            {  
                //...meaning 44100 Hz can be used as the recording sampling rate  
                maxFreq = 44100;  
            }  
  
            //Get the attached AudioSource component  
            goAudioSource = this.GetComponent<AudioSource>();  
            float[] curOutput = new float[1024];
	        goAudioSource.GetOutputData(curOutput, 0);
            Debug.Log(curOutput);
            Debug.Log("curOutput:");
            foreach (float i in curOutput)
            {
                Debug.Log(i);
            }
        }  
    }

    void OnGUI()   
    {  
        //If there is a microphone  
        if(micConnected)  
        {  
            //If the audio from any microphone isn't being captured  
            if(!Microphone.IsRecording(null))  
            {  
                //Case the 'Record' button gets pressed  
                if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-25, 200, 50), "Record"))  
                {  
                    //Start recording and store the audio captured from the microphone at the AudioClip in the AudioSource  
                    goAudioSource.clip = Microphone.Start(null, true, 20, maxFreq);  
                }  
            }  
            else //Recording is in progress  
            {  
                //Case the 'Stop and Play' button gets pressed  
                if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-25, 200, 50), "Stop and Play!"))  
                {  
                    Microphone.End(null); //Stop the audio recording  
                    goAudioSource.Play(); //Playback the recorded audio  
                }  
  
                GUI.Label(new Rect(Screen.width/2-100, Screen.height/2+25, 200, 50), "Recording in progress...");  
            }  
        }  
        else // No microphone  
        {  
            //Print a red "Microphone not connected!" message at the center of the screen  
            GUI.contentColor = Color.red;  
            GUI.Label(new Rect(Screen.width/2-100, Screen.height/2-25, 200, 50), "Microphone not connected!");  
        }  
  
    }  

    // Update is called once per frame
    void Update()
    {
         float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        Debug.Log("SPECTRUM RESULTS");
        foreach (float i in spectrum)
        {
            Debug.Log(i);
        }

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }
        // float freq = PitchDetectorGetFreq(0), deviation = 0.0f;
        // frequency = freq.ToString() + " Hz";

        // if (freq > 0.0f)
        // {
        //     Debug.Log("sound detected at " + System.DateTime.Now);

        //     // float noteval = 57.0f + 12.0f * Mathf.Log10(freq / 440.0f) / Mathf.Log10(2.0f);
        //     // float f = Mathf.Floor(noteval + 0.5f);
        //     // deviation = Mathf.Floor((noteval - f) * 100.0f);
        //     // int noteIndex = (int)f % 12;
        //     // int octave = (int)Mathf.Floor((noteval + 0.5f) / 12.0f);
        //     // note = noteNames[noteIndex] + " " + octave;
        // }
    }
}