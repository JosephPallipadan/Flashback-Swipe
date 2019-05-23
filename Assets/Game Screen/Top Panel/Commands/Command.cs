using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour {

    public const int LEFT = 0;
    public const int RIGHT = 1;
    public const int UP = 2;
    public const int DOWN = 3;
    public const int UP_LEFT = 4;
    public const int DOWN_LEFT = 5;
    public const int UP_RIGHT = 6;
    public const int DOWN_RIGHT = 7;

    public const int NONE = 0;
    public const int DOUBLE = 1;
    public const int REVERSAL = 2;
    public const int RECALL = 3;
    public const int NULLIFY = 4;

    private Rigidbody2D body;
    private bool colorChanged;

    public bool tapped;
    public int modifier;
    public int commandType;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();

        colorChanged = false;
        body.velocity = Vector2.left;
	}
	
	// Update is called once per frame
	void Update () {
        if (!colorChanged && (TopPanel.determineTapType() == -1 || tapped) )
            changeColorAfterTap();
	}

    public void setModifier(int modifier)
    {
        Color color;
        switch (modifier)
        {
            case NONE://No modifiers
                color = Color.black;
                break;

            case DOUBLE://Double
                color = Color.red;
                break;

            case REVERSAL://Reversal
                color = Color.blue;
                break;

            case RECALL://Recall
                color = Color.green;
                break;

            default://Nullify
                color = Color.grey;
                break;
        }
        this.modifier = modifier;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void changeColorAfterTap()
    {
        if (!colorChanged)
        {
            if(TopPanel.determineTapType()==-1 && !tapped)
                KeyPad.loseLife();

            int color = Random.Range(0, 3);

            if (color == 0)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                KeyPad.redCount++;

                if(TopPanel.determineTapType()==1)
                    GameObject.Find("Red Collider").GetComponent<ThirdPanel>().TapCount++;
            }
            else if (color == 1)
            {
                GetComponent<SpriteRenderer>().color = Color.green;
                KeyPad.greenCount++;

                if (TopPanel.determineTapType() == 1)
                    GameObject.Find("Green Collider").GetComponent<ThirdPanel>().TapCount++;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
                KeyPad.blueCount++;

                if (TopPanel.determineTapType() == 1)
                    GameObject.Find("Blue Collider").GetComponent<ThirdPanel>().TapCount++;
            }

            colorChanged = true;
        }
    }
}
