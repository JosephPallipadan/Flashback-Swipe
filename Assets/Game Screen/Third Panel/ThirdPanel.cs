using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPanel : MonoBehaviour {
    private Text tapDisplay;
    private int tapCount;

    public int TapCount
    {
        get
        {
            return tapCount;
        }

        set
        {
            tapCount = value;
            if (tapCount == 16)
                tapCount--;
            else
            tapDisplay.text = tapCount + "";
        }
    }

    // Use this for initialization
    void Start () {
		if(gameObject.name=="Red Collider")
        {
            tapDisplay = GameObject.Find("Red Counter").GetComponent<Text>();
        }

        else if(gameObject.name == "Blue Collider")
        {
            tapDisplay = GameObject.Find("Blue Counter").GetComponent<Text>();
        }

        else
        {
            tapDisplay = GameObject.Find("Green Counter").GetComponent<Text>();
        }
	}

    private void OnMouseDown()
    {
        if (GameObject.FindObjectOfType<CommandSpawner>().roundPaused)
        {
            TapCount++;
            GetComponent<AudioSource>().Play();
        }
    }
}
