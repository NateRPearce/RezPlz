using UnityEngine;
using System.Collections;

public class SoundFunctions : GameStateFunctions {

    public bool cameraNear;
    public bool cameraFar;
    public bool ignoreRange;

    public void soundStartFunctions()
    {
        FindGM();
        InvokeRepeating("CheckForCamera", Time.deltaTime, 1);
    }

    public float checkRange()
    {
        if (GM.skullGuide == null)
        {
            return 0;
        }
        else
        {
            float distanceToPlayer = (((GM.skullGuide.position.x - transform.position.x) + (GM.skullGuide.position.y - transform.position.y)));
            float howFar = Mathf.Abs(distanceToPlayer);
            if (howFar > 60&&!ignoreRange)
            {
                howFar = 0;
            }
            else if (ignoreRange&&howFar > 60)
            {
                howFar = 0.5f;
            }
            else
            {
                howFar = (60 - howFar) / 60;
            }
        return howFar;
        }
    }

	public void CheckForCamera()
    {
        if (GM == null||GM.skullGuide==null)
        {
            return;
        }

        if (Mathf.Abs(Vector3.Distance(GM.skullGuide.position, transform.position)) > 85 || (transform.position.y - GM.skullGuide.position.y) > 30 || (transform.position.y - GM.skullGuide.position.y) < -30)
        {
            cameraNear = false;
            cameraFar = false;
        }
        else if (Vector3.Distance(GM.skullGuide.position, transform.position) < 85)
        {
            cameraFar = true;
        }
        else if(Vector3.Distance(GM.skullGuide.position, transform.position) < 50)
        {
            cameraNear = true;
        }
    }

}
