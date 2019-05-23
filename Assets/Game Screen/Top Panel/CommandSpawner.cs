using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandSpawner : MonoBehaviour {
    public static int score;

    public GameObject[] commands;
    public GameObject roundTimer;
    public bool roundPaused;
    

    private int commandsForRound;
    private int commandsSpawned;

	void Start () {
        score = 0;
        commandsForRound = 1;
        roundPaused = false;

        SpawnCommand();
	}

    public void SpawnCommand()
    {
        if(commandsSpawned<commandsForRound)
        {
            int modifier = Random.Range(0,5);//Command.NONE;
            while (Shredder.mostRecentCommandModifier==-1 && modifier==Command.RECALL)
                modifier = Random.Range(0, 5);

            int type = Random.Range(0, 8); //Command.UP_LEFT;

            GameObject command = Instantiate(commands[type]);

            command.GetComponent<Command>().setModifier(modifier);
            command.GetComponent<Command>().commandType = type;
            command.GetComponent<Rigidbody2D>().velocity = Vector2.left;
            command.transform.parent = transform;
            command.name = "Command";

            KeyPad.correctCurrentPosition = calculateNextPosition(type, modifier);

            commandsSpawned++;
        }

        else
        {
            roundTimer.GetComponent<Animator>().SetTrigger("Count Down");
            roundPaused = true;
            Invoke("startNextRound", 15);
        }
    }

    private int calculateNextPosition(int type, int modifier)
    {
        int correctPosition=KeyPad.currentPosition;
        //Debug.Log(correctPosition);

        Vector2 correctPositionAsVector = new Vector2(  (KeyPad.currentPosition) % 3 , Mathf.Ceil(KeyPad.currentPosition/3.0f) );
        if (correctPositionAsVector.x == 0)
            correctPositionAsVector.x = 3;
        //Debug.Log(correctPositionAsVector);

        Vector2 movement = Vector2.zero;

        if (modifier == Command.NULLIFY)
            return correctPosition;

        if (modifier==Command.RECALL)
        {
            modifier = Shredder.mostRecentCommandModifier;
            type = Shredder.mostRecentCommandType;
        }

        movement = getHorizontalAndVerticalMovement(type);
        //Debug.Log(movement);

        if (modifier == Command.DOUBLE)
            movement *= 2;
        else if (modifier == Command.REVERSAL)
            movement *= -1;

        //Debug.Log(movement);

        //Debug.Log(correctPositionAsVector);
        correctPositionAsVector += movement;
        //Debug.Log(correctPositionAsVector);

        if (correctPositionAsVector.x > 3)
            correctPositionAsVector.x -= 3;

        else if (correctPositionAsVector.x < 1)
            correctPositionAsVector.x += 3;

        if (correctPositionAsVector.y > 3)
            correctPositionAsVector.y -= 3;

        else if (correctPositionAsVector.y < 1)
            correctPositionAsVector.y += 3;

        //Debug.Log(correctPositionAsVector);

        correctPosition = (int) (correctPositionAsVector.x + (correctPositionAsVector.y-1) * 3);
        return correctPosition;
    }

    private Vector2 getHorizontalAndVerticalMovement(int type)
    {
        Vector2 movement = Vector2.zero;

        if(type==Command.UP)
        {
            movement = Vector2.up*-1;
        }
        else if (type == Command.DOWN)
        {
            movement = Vector2.down*-1;
        }
        else if (type == Command.LEFT)
        {
            movement = Vector2.left;
        }
        else if (type == Command.RIGHT)
        {
            movement = Vector2.right;
        }
        else if (type == Command.UP_LEFT)
        {
            movement = Vector2.up*-1 + Vector2.left;
        }
        else if (type == Command.UP_RIGHT)
        {
            movement = Vector2.up * -1 + Vector2.right;
        }
        else if (type == Command.DOWN_LEFT)
        {
            movement = Vector2.down * -1 + Vector2.left;
        }
        else if (type == Command.DOWN_RIGHT)
        {
            movement = Vector2.down * -1 + Vector2.right;
        }

        return movement;
    }

    private void startNextRound()
    {
        int tappedRedCount = GameObject.Find("Red Collider").GetComponent<ThirdPanel>().TapCount;
        int tappedBlueCount = GameObject.Find("Blue Collider").GetComponent<ThirdPanel>().TapCount;
        int tappedGreenCount = GameObject.Find("Green Collider").GetComponent<ThirdPanel>().TapCount;

        if(tappedRedCount==KeyPad.redCount && tappedBlueCount==KeyPad.blueCount && tappedGreenCount==KeyPad.greenCount)
        {
            score *= 2;
        }
        else
        {
            score /= 2;
        }

        KeyPad.greenCount = 0;
        KeyPad.blueCount = 0;
        KeyPad.redCount = 0;
        GameObject.Find("Score").GetComponent<Text>().text = (CommandSpawner.score + "");
        GameObject.Find("Blue Collider").GetComponent<ThirdPanel>().TapCount=0;
        GameObject.Find("Green Collider").GetComponent<ThirdPanel>().TapCount=0;
        GameObject.Find("Red Collider").GetComponent<ThirdPanel>().TapCount=0;

        commandsForRound += 1;
        commandsSpawned = 0;
        SpawnCommand();
        roundPaused = false;
    }


}
