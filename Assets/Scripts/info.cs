using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class info : MonoBehaviour
{
    public Image imgInfo;
    public Button btn;

    void Start()
    {
        imgInfo.gameObject.SetActive(false);
    }
        public void openInfo()
    {
        imgInfo.gameObject.SetActive(true);
        imgInfo.GetComponent<Text>().gameObject.SetActive(true);
    }
    public void closeInfo()
    {
        imgInfo.gameObject.SetActive(false);
        imgInfo.GetComponent<Text>().gameObject.SetActive(false);
    }


}