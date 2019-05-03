using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadSprite : MonoBehaviour
{
    Sprite sprite;
    // Use this for initialization
    void Start()
    {

        StartCoroutine(PlaceImage());

    }

    public IEnumerator PlaceImage() {

        yield return new WaitForSeconds(5);

        sprite = Resources.Load<Sprite>("Images/quarter_note");

        GameObject image = GameObject.Find("Image");

        image.GetComponent<Image>().sprite = sprite;

    }

}
