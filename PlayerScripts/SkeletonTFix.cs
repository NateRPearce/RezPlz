using UnityEngine;
using System.Collections;

public class SkeletonTFix : MonoBehaviour {

    public Rigidbody2D[] rbodies;
    Vector3[] startingPos;
    Collider2D[] col;
    void Awake()
    {
        rbodies = GetComponentsInChildren<Rigidbody2D>();
        startingPos = new Vector3[rbodies.Length];
        col = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < rbodies.Length; i++)
        {
            startingPos[i] = rbodies[i].transform.localPosition;
        }
        }

    public void ResetRbodies()
    {
        foreach(Collider2D c in col)
        {
            c.enabled = true;
        }
        for(int i = 0; i < rbodies.Length; i++)
        {
            rbodies[i].velocity = new Vector2(0, 0);
            rbodies[i].transform.localPosition = startingPos[i];
        }
    }

}
