using UnityEngine;
using System.Collections;

public class CamPosAdjust : GameStateFunctions {

    public float minY;
    public float maxY;

    void Start()
    {
        FindCBS();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CameraCollider")
        {
            if (maxY != 0)
            {
                CBS.yMax = maxY;
            }
            if (minY != 0)
            {
                CBS.yMin = minY;
            }
        }
    }
}
