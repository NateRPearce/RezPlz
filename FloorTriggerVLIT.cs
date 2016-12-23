using UnityEngine;
using System.Collections;

public class FloorTriggerVLIT : MonoBehaviour {

    bool playerFound;
    public Transform[] target = new Transform[1];
    RemoteTriggerScript[] RTS;
    public Transform triggerPoint;
    public LayerMask whatIsPlayer;
    public AnimatedTextureExtendedUV AT;

    void Start()
    {
        RTS = new RemoteTriggerScript[target.Length];
        for (int i = 0; i < target.Length; i++)
        {
            RTS[i] = target[i].GetComponent<RemoteTriggerScript>();
        }
            AT = GetComponent<AnimatedTextureExtendedUV>();
        InvokeRepeating("CheckForPlayer", Time.deltaTime, 0.05f);
    }

    void CheckForPlayer()
    {
        playerFound = Physics2D.OverlapCircle(triggerPoint.position, 2, whatIsPlayer);
        if (playerFound)
        {
            if (AT.colNumber < AT.totalCells - 1)
            { 
                AT.colNumber += 1;
            }
            for (int i = 0; i < target.Length; i++)
            {
                RTS[i].Activated = true;
            }
        }
        else
        {
            if (AT.colNumber > 0)
            {
                AT.colNumber -= 1;
            }
            for (int i = 0; i < target.Length; i++)
            {
                RTS[i].Activated = false;
            }
        }
    }
}
