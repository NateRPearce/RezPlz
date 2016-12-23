using UnityEngine;
using System.Collections;

public class DiasBase : MonoBehaviour {

    [FMODUnity.EventRef]
    string dhitSound = "event:/NATECHECKTHISOUT/Swing_Hit_Lever_01";
    FMOD.Studio.EventInstance dhitEV;

    DiasHeart DS;


    void Start()
    {
        dhitEV = FMODUnity.RuntimeManager.CreateInstance(dhitSound);
        DS = GetComponentInChildren<DiasHeart>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<PlayerControls>() != null && other.tag == Tags.attackCollider)
        {
            DS.PC = other.GetComponentInParent<PlayerControls>();
            DS.col.isTrigger = false;
            DS.col.gameObject.layer = 11;
            DS.rbody.isKinematic = false;
            DS.anim.SetTrigger("Hit");
            DS.diasAnim.SetTrigger("Hit");
            dhitEV.start();
            DS.target.GetComponent<RemoteTriggerScript>().Activated = !DS.invert;
            if (DS.PC.facingRight)
            {
                DS.PC = other.GetComponentInParent<PlayerControls>();
                DS.rbody.AddForce(new Vector2(500, 0));
                DS.rbody.AddTorque(-100);
            }
            else
            {
                Vector3 xScale = transform.localScale;
                xScale.x *= -1;
                transform.localScale = xScale;
                DS.rbody.AddTorque(100);
                DS.rbody.AddForce(new Vector2(-500, 0));
            }
        }
    }
}
