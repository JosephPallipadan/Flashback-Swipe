using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {
    public static int mostRecentCommandType = -1;
    public static int mostRecentCommandModifier = -1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("spawnNextEnemy", Random.Range(3, 7));
        mostRecentCommandType=collision.gameObject.GetComponent<Command>().commandType;
        mostRecentCommandModifier= collision.gameObject.GetComponent<Command>().modifier;
        Destroy(collision.gameObject);
    }

    void spawnNextEnemy()
    {
        GameObject.Find("Command Spawner").GetComponent<CommandSpawner>().SpawnCommand();
    }
}
