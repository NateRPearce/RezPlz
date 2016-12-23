using UnityEngine;
using System.Collections;

public class FreezeFrame : MonoBehaviour {

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void StopAnimating()
    {
        anim.speed = 0;
    }
}
