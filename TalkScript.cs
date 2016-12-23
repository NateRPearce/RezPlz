using UnityEngine;
using System.Collections;

public class TalkScript : GameStateFunctions {

    void Start()
    {
        FindGM();
    }

void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("ShowDialogue");
        }
    }

    IEnumerator ShowDialogue()
    {
        yield return new WaitForSeconds(0.5f);       
        GM.HD[0].Talk("That was a close one", 125, 40, -115, 19, 1.6f);
        yield return new WaitForSeconds(1.7f);
        GM.HD[1].Talk("I can't believe we survi...", 135, 45, 122, 19, 1);
        yield return new WaitForSeconds(.75f);
        GM.PM.PC1.transform.localScale = new Vector3(-10, 10, 10);
        GM.PM.PC1.facingRight = !GM.PM.PC1.facingRight;
        yield return new WaitForSeconds(.05f);
        GM.HD[0].Talk("Oh $#!%", 60, 32, -91, 19, 2);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
