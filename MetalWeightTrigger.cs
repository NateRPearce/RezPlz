using UnityEngine;
using System.Collections;

public class MetalWeightTrigger : GameStateFunctions {

    public bool player1_On;
    public bool player2_On;
    public bool heavyPlayer_On;
    public bool heavyObject_on;
    public bool Object_on;
    public float max_Pressure;
    public float current_Pressure;
    public Transform target;
    RemoteTriggerScript RTS;
    Animator[] anims;
    Rigidbody2D p1_rbody;
    Rigidbody2D p2_rbody;

	void Start()
    {
        RTS = target.GetComponent<RemoteTriggerScript>();
        FindGM();
        anims = GetComponentsInChildren<Animator>();
        InvokeRepeating("CheckWeight", Time.deltaTime, 0.1f);
    }



    public void CheckWeight()
    {
        if (player1_On||player2_On)
        {
            if ((player1_On&&GM.PM.PC1.rbody.mass > 10)||(player2_On && GM.PM.PC2.rbody.mass > 10))
            {
                heavyPlayer_On = true;
            }else
            {
                heavyPlayer_On = false;
            }
        }else
        {
            heavyPlayer_On = false;
        }


        if ((player1_On && player2_On) || heavyPlayer_On)
        {
            max_Pressure = 13;
        }else if (player1_On || player2_On)
        {
            max_Pressure = 5;
        }else
        {
            max_Pressure = 0;
        }
        if (current_Pressure != max_Pressure)
        {
            if ((player1_On || player2_On) && current_Pressure < max_Pressure)
            {
                current_Pressure += 1;
            }
            else
            {
                current_Pressure -= 1;
            }
        }
        if (current_Pressure < 0)
        {
            current_Pressure = 0;
        }
        if (current_Pressure > 13)
        {
            current_Pressure = 13;
        }
        for(int i = 0; i < anims.Length; i++)
        {
            anims[i].SetFloat("Pressure", current_Pressure);
        }
        if (max_Pressure == 13)
        {
            RTS.Activated = true;
        }
        else if (max_Pressure==5)
        {
            RTS.halfActivated = true;
            RTS.Activated = false;
        }
        else
        {
            RTS.halfActivated = false;
            RTS.Activated = false;
        }
    }

void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player1")
        {
            player1_On=true;
        }
        if (other.name == "Player2")
        {
            player2_On = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player1")
        {
            player1_On = false;
        }
        if (other.name == "Player2")
        {
            player2_On = false;
        }
    }
}
