using UnityEngine;

public class TopPanel : MonoBehaviour {
    public const int PERFECT_TAP= 1;
    public const int FINE_TAP = 0;
    public const int MISSED_TAP = -1;

    public static int determineTapType()
        //1 Means Perfect, 0 Means Fine, -1 Means Missed
    {
        Command command = GameObject.FindObjectOfType<Command>();
        float pos = command.gameObject.transform.position.x;

        if (pos >= 1.2)
            return PERFECT_TAP;
        else if (pos >= -1)
            return FINE_TAP;
        else
            return MISSED_TAP;
    }
}
