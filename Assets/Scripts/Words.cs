using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Words : MonoBehaviour
{
    public Text text;
    public Button words;
    public TextMeshProUGUI words_text;
    Color32 blue_color = new Color32(148, 189, 156, 255);
    Color32 orange_color = new Color32(215, 109, 67, 255);

    public void ChangeColor(){
        //words_text.color = blue_color;

        if (text.gameObject.activeSelf == true)
        {
            //GetComponent(text)
            //words.GetComponentsInChildren().color = blue_color;
            text.gameObject.SetActive(false);
            words_text.text = "Words";
            //this.GetComponentInChildren(TextMesh)
        }
        else
        {
            text.gameObject.SetActive(true);
            text.text = Globals.Instance.curMnemonic;
            Debug.Log("SET " + Globals.Instance.curMnemonic);
            words_text.text = "No words";

        }


    }

}
