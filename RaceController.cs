using UnityEngine;
using System.Collections;

public class RaceController : GameStateFunctions {

    public Transform[] topRoute = new Transform[1];
    public Transform[] bottomRoute = new Transform[1];

    public Transform p1CheckPoint;
    public Transform p2CheckPoint;

    void Start()
    {
        FindGM();
        GM.raceMode = true;
        StartCoroutine("SetRC");
    }

    public void CheckPointCheck(int playerNum)
    {
        if (playerNum == 1)
        {
            for(int i = 0; i < topRoute.Length; i++)
            {
                if (topRoute[i] == p2CheckPoint)
                {
                    if (i == topRoute.Length - 1)
                    {
                        p1CheckPoint = bottomRoute[i];
                    }
                    else
                    {
                        p1CheckPoint = bottomRoute[i + 1];
                    }
                    break;
                }
            }
            for (int i = 0; i < bottomRoute.Length; i++)
            {
                if (bottomRoute[i] == p2CheckPoint)
                {
                    if (i == topRoute.Length - 1)
                    {
                        p1CheckPoint = topRoute[i];
                    }else
                    {
                        p1CheckPoint = topRoute[i + 1];
                    }
                    break;
                }
            }
        }
        else
        {
                for (int i = 0; i < topRoute.Length; i++)
                {
                    if (topRoute[i] == p1CheckPoint)
                    {
                    if (i == topRoute.Length - 1)
                    {
                        p2CheckPoint = bottomRoute[i];
                    }else
                    {
                        p2CheckPoint = bottomRoute[i + 1];
                    }
                    break;
                    }
                }
                for (int i = 0; i < bottomRoute.Length; i++)
                {
                    if (bottomRoute[i] == p1CheckPoint)
                    {
                    if (i == topRoute.Length - 1)
                    {
                        p2CheckPoint = topRoute[i];
                    }else
                    {
                        p2CheckPoint = topRoute[i + 1];
                    }
                    break;
                    }
                }
            }
    }
    IEnumerator SetRC()
    {
        yield return new WaitForEndOfFrame();
        GM.PM.RC = this;
    }
}
