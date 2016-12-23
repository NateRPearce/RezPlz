using UnityEngine;
using System.Collections;

public class DropBox : MonoBehaviour {

    [FMODUnity.EventRef]
    string smashSound = "event:/ULMOK/LavaLevel_Ulmok_Flop";
    FMOD.Studio.EventInstance smashEV;


    RemoteTriggerScript RTS;
    Animator anim;
    Rigidbody2D rbody;
    bool smashing;
    bool groundFound;
    LayerMask whatIsGround;
    public Transform smashPoint;
    public Collider2D squishCol;
    public Transform smashSmoke;
    public SpriteRenderer shadow;
    Animator smokeAnim;
    void Start()
    {
        RTS = GetComponent<RemoteTriggerScript>();
        InvokeRepeating("DropCheck", Time.deltaTime, 1);
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        whatIsGround = LayerMask.GetMask("ground");
        smashEV = FMODUnity.RuntimeManager.CreateInstance(smashSound);
        smokeAnim = smashSmoke.GetComponent<Animator>();
    }

    void Update()
    {
        groundFound = Physics2D.OverlapBox(smashPoint.position, new Vector2(7, 0.1f), 0,whatIsGround);

        if (groundFound && !smashing)
        {
            StartCoroutine("Smash");
            shadow.color = new Color(0, 0, 0, .35f);
            smashing = true;
        }
        if (!groundFound && shadow.color.a > 0)
        {
            shadow.color = Color.clear;
        }
        FallingCheck();
    }

    void FallingCheck()
    {
        if(rbody.velocity.y!=0)
        Debug.Log(rbody.velocity.y);

        if (!groundFound && rbody.velocity.y <0 && !squishCol.enabled)
        {
            Debug.Log("Falling");
            squishCol.enabled = true;
        }
        else if (squishCol.enabled && rbody.velocity.y > 0)
        {
            Debug.Log("Not Falling");
            squishCol.enabled = false;
        }
    }

    void DropCheck()
    {
        if (RTS.Activated && rbody.isKinematic)
        {
            anim.SetTrigger("Activate");
        }
    }
         public void Drop()
    {
        rbody.isKinematic = false;
        rbody.AddForce(new Vector2(0, 20));
    }

    IEnumerator Smash()
    {
        Debug.Log("Just F#%@ing WORK!");
        smashEV.start();
        smokeAnim.SetTrigger("Puff");
        //shake screen
        yield return new WaitForSeconds(0.1f);
    }
}
