using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class KeyPad : MonoBehaviour {
    public AudioClip[] sounds;
    public int number;

    private static int livesLeft = 3;

    public static int previousPosition = 5;
    public static int currentPosition = 5;
    public static int correctCurrentPosition;

    public static int redCount;
    public static int blueCount;
    public static int greenCount;

    private void Start()
    {
        livesLeft = 3;
    }

    public static int LivesLeft
    {
        get
        {
            return livesLeft;
        }

        set
        {
            livesLeft = value;
            if (livesLeft == 2)
                Destroy(GameObject.Find("Life_3"));

            else if (livesLeft == 1)
                Destroy(GameObject.Find("Life_2"));

            else
                SceneManager.LoadScene("Game Over Screen");

        }
    }

    private void OnMouseDown()
    {
        if (GameObject.Find("Command").GetComponent<Command>().tapped==false)
        {
            GameObject.Find("KeyPadSelected").transform.position = transform.position;

            previousPosition = currentPosition;
            currentPosition = number;

            takeActionsAfterTap();

            GameObject.Find("Command").GetComponent<Command>().tapped = true;
        }
    }

    public void takeActionsAfterTap()
    {
        GameObject.FindObjectOfType<Command>().tapped = true;

        if (correctCurrentPosition == currentPosition)
        {
            GameObject.FindObjectOfType<Command>().changeColorAfterTap();
            if (TopPanel.determineTapType() == TopPanel.PERFECT_TAP)
            {
                CommandSpawner.score += 300;
                displayPerfectTapText();
                AudioSource.PlayClipAtPoint(sounds[2], transform.position);
            }
            else if (TopPanel.determineTapType() == TopPanel.FINE_TAP)
            {
                CommandSpawner.score += 100;
                displayFineTapText();
                AudioSource.PlayClipAtPoint(sounds[1], transform.position);
            }
            GameObject.Find("Score").GetComponent<Text>().text = (CommandSpawner.score + "");
            Debug.Log("Correct");
        }
        else
        {
            loseLife();
            AudioSource.PlayClipAtPoint(sounds[0], transform.position);
        }

        Invoke("setTextToBeBlank", 2);

    }

    private void displayPerfectTapText()
    {
        GameObject.Find("Tap Adjective").GetComponent<Text>().text = "Perfect";
    }

    private void displayFineTapText()
    {
        GameObject.Find("Tap Adjective").GetComponent<Text>().text = "Fine";
    }

    private void setTextToBeBlank()
    {
        GameObject.Find("Tap Adjective").GetComponent<Text>().text = "-------------------";
    }

    public static void loseLife()
    {
        GameObject.Find("Tap Adjective").GetComponent<Text>().text = "A Miss";
        LivesLeft--;
        Debug.Log("Wrong " + correctCurrentPosition);
    }
}
