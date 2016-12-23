using UnityEngine;
using System.Collections;

public class SquishCollider : GameStateFunctions {

    public Transform stompCloud;
    Animator cloudAnim;
    AudioSource source;
    public AudioClip hitGround;
    CaveBossBehavior CBB;
    public Vector3 bossFightCamPosition;
    void Awake () {
        cloudAnim = stompCloud.GetComponent<Animator>();
        source = GetComponent<AudioSource>();
	}

    void Start()
    {
        FindCBS();
        CBB = GetComponentInParent<CaveBossBehavior>();
        bossFightCamPosition = CBB.bossFightCamPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Ground")
        {
            source.PlayOneShot(hitGround, 1);
            bossFightCamPosition = CBB.bossFightCamPosition;
            StartCoroutine("ShakeCamera");
            stompCloud.position = new Vector3(transform.position.x, transform.position.y+1, stompCloud.position.z);
            cloudAnim.SetTrigger("Activated");
        }
    }


    IEnumerator ShakeCamera()
    {
        CBS.EventLocation = new Vector3(bossFightCamPosition.x + 2, bossFightCamPosition.y, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.1f);
        CBS.EventLocation = new Vector3(bossFightCamPosition.x - 2, bossFightCamPosition.y, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.1f);
        CBS.EventLocation = new Vector3(bossFightCamPosition.x + 2, bossFightCamPosition.y, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.1f);
        CBS.EventLocation = new Vector3(bossFightCamPosition.x - 2, bossFightCamPosition.y, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.1f);
        CBS.EventLocation = bossFightCamPosition;
    }
}
