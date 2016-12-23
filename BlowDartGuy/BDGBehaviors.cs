using UnityEngine;
using System.Collections;

public class BDGBehaviors : MinionScript {

    public Transform blowDart;
    Rigidbody2D bdRbody;
    Animator bodyAnim;
    Animator headAnim;
    public Transform head;
    public Transform firePoint;
    public Transform smoke;
    public float angle;
    public float angleOffset;
    public Quaternion targetRotation;
    Quaternion startingRot;
    void Start()
    {
        FindGM();
        StartCoroutine("RegiserEnemy");
        currentSpeed = 0;
        anim = GetComponent<Animator>();
       // anim.SetBool("Alive", true);
        setDead(false);
        dying = false;
        bodyAnim = GetComponent<Animator>();
        headAnim = head.GetComponent<Animator>();
        startingRot = head.rotation;
    }

    void Update () {
        DeadEndCheck();
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            float x = targetPC.transform.position.x - transform.position.x;
            float y = targetPC.transform.position.y - transform.position.y;
            float xOffset = targetPC.rbody.velocity.x / 4;
            float yOffset = targetPC.rbody.velocity.y / 4;
            angle = Mathf.Atan2(y+yOffset, x+xOffset) * Mathf.Rad2Deg;
            targetRotation = Quaternion.AngleAxis(angle+angleOffset, Vector3.forward);
            head.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 20);
            Debug.Log("Aiming");
        }else if(head.rotation!=startingRot)
        {
            head.rotation = Quaternion.RotateTowards(transform.rotation,startingRot, 20);
        }
    }
IEnumerator AttemptAttack()
    {
        if (!isAttacking)
        {
            //shoot dart
            isAttacking = true;
            bodyAnim.SetTrigger("Attack");
            headAnim.SetTrigger("Attack");
            yield return new WaitForSeconds(atkCooldown);
            isAttacking = false;
        }
    }
    public void Shoot()
    {
        Transform currentDart;
        Transform currentSmoke;
        currentDart = Instantiate(blowDart, firePoint.position, transform.rotation) as Transform;
        currentSmoke = Instantiate(smoke, firePoint.position, transform.rotation)as Transform;
        bdRbody = currentDart.GetComponent<Rigidbody2D>();
        float x = targetPC.transform.position.x - transform.position.x;
        float y = targetPC.transform.position.y - transform.position.y;
        float xOffset = targetPC.rbody.velocity.x / 4;
        float yOffset = targetPC.rbody.velocity.y / 4;
        currentSmoke.rotation = targetRotation;
        head.rotation = targetRotation;
        Vector2 trajectory = new Vector2(x + xOffset, y + yOffset).normalized * 100;
        bdRbody.velocity = trajectory;
    }
}
