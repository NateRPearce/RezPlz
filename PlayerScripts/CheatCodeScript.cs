using UnityEngine;
using System.Collections;

public class CheatCodeScript : GameStateFunctions {
    string jumpBtn;
    string grabBtn;
    string rezBtn;
    string attackBtn;
    bool buttonsAsinged;

    [FMODUnity.EventRef]
    string codeSound = "event:/Level Complete";
    public FMOD.Studio.EventInstance codeEV;

    PlayerControls PC;
    public int[] CheatCodes=new int[5];
    int currentCodePoint=0;
    public bool[] CheatCodeActivated= new bool[3];

    //cheat codes
    int[] smallEnemies=new int[] { 4, 3, 2, 1, 5 };
    int[] largeEnemies = new int[] { 1, 2, 3, 3, 5 };
    int[] lowGravity = new int[] { 1, 2, 3, 4, 5 };

    void Start () {
        FindGM();
        codeEV = FMODUnity.RuntimeManager.CreateInstance(codeSound);
        PC = GetComponent<PlayerControls>();
        StartCoroutine("findButtons");
	}

    public IEnumerator findButtons()
    {
        yield return new WaitForSeconds(1);
        jumpBtn = PC.jumpbtn;
        grabBtn = PC.grabbtn;
        rezBtn = PC.switchPlayersbtn;
        attackBtn = PC.attackbtn;
        buttonsAsinged = true;
    }

    void Update()
    {
        if (!buttonsAsinged)
        {
            return;
        }
      // CheatUpdate();
    }

    void CheckCheatPoint()
    {
        if (CheatCodes[4] != 0 && currentCodePoint == 4)
        {
            for (int i = 0; i < CheatCodes.Length - 1; i++)
            {
                CheatCodes[i] = CheatCodes[i + 1];
            }
        }
    }

    void UpdateCurrentCodePoint()
    {
        if (currentCodePoint < 4)
        {
            currentCodePoint++;
        }
    }


    public void CheatUpdate()
    {
        if (Input.GetButtonDown(jumpBtn))
        {
            CheckCheatPoint();
            CheatCodes[currentCodePoint] = 1;
            UpdateCurrentCodePoint();
            EnterCheatCode(CheatCodes);
        }
        else if (Input.GetButtonDown(attackBtn))
        {
            CheckCheatPoint();
            CheatCodes[currentCodePoint] = 2;
            UpdateCurrentCodePoint();
            EnterCheatCode(CheatCodes);
        }
        else if (Input.GetButtonDown(rezBtn))
        {
            CheckCheatPoint();
            CheatCodes[currentCodePoint] = 3;
            UpdateCurrentCodePoint();
            EnterCheatCode(CheatCodes);
        }
        else if (Input.GetButtonDown(grabBtn))
        {
            CheckCheatPoint();
            CheatCodes[currentCodePoint] = 4;
            UpdateCurrentCodePoint();
            EnterCheatCode(CheatCodes);
        }
        else if (Input.GetButtonDown("Enter_Code"))
        {
            CheckCheatPoint();
            CheatCodes[currentCodePoint] = 5;
            UpdateCurrentCodePoint();
            EnterCheatCode(CheatCodes);
        }
    }

    void EnterCheatCode(int[] a)
    {
        if (ShrinkCheck(CheatCodes))
        {
            Try2ShrinkEnemies();
        }
        if(GrowCheck(CheatCodes))
        {
            Try2GrowEnemies();
        }
        if(GravityCheck(CheatCodes))
        {
            ChangeGravity();
        }
    }
    public bool ShrinkCheck(int[] currentCode)
    {
        bool codeEntered = true;
        for(int i = 0; i < CheatCodes.Length; i++)
        {
            if (smallEnemies[i] != currentCode[i])
            {
                codeEntered = false;
                return codeEntered;
            }
        }
        return codeEntered;
    }

    public bool GravityCheck(int[] currentCode)
    {
        bool codeEntered = true;
        for (int i = 0; i < CheatCodes.Length; i++)
        {
            if (lowGravity[i] != currentCode[i])
            {
                codeEntered = false;
                return codeEntered;
            }
        }
        return codeEntered;
    }

    public bool GrowCheck(int[] currentCode)
    {
        bool codeEntered = true;
        for (int i = 0; i < CheatCodes.Length; i++)
        {
            if (largeEnemies[i] != currentCode[i])
            {
                codeEntered = false;
                return codeEntered;
            }
        }
        return codeEntered;
    }

    public void Try2ShrinkEnemies()
    {
        Debug.Log("attempt shrink");
        CheatCodeActivated[0] = !CheatCodeActivated[0];
        if (!CheatCodeActivated[0])
        {
            RestoreEnemies(0);
            codeEV.start();
        }else
        {
            shrinkEnemies();
            codeEV.start();
            CheatCodeActivated[1] = false;
        }
    }

    public void shrinkEnemies()
    {
        int flipMod = 1;
        foreach (Transform enemy in GM.CC.enemies)
        {
            if (enemy.GetComponent<MinionScript>().facingRight)
            {
                flipMod = -1;
            }
            else
            {
                flipMod = 1;
            }
            enemy.localScale = new Vector3(5*flipMod, 5, 5);
            for (int i = 0; i < CheatCodes.Length; i++)
            {
                CheatCodes[i] = 0;
            }

        }
    }

    void OnDestroy()
    {
        GM.numberOfEnemies = 0;
    }

    public void enlargeEnemies()
    {
        int flipMod=1;
            foreach(Transform enemy in GM.CC.enemies)
        {
            if (enemy.GetComponent<MinionScript>().facingRight)
            {
                flipMod = -1;
            }else
            {
                flipMod = 1;
            }
            enemy.localScale = new Vector3(15*flipMod, 15, 15);
            for (int i = 0; i < CheatCodes.Length; i++)
            {
                CheatCodes[i] = 0;
            }

        }
    }

    public void Try2GrowEnemies()
    {
        Debug.Log("attempt Grow");
        CheatCodeActivated[1] = !CheatCodeActivated[1];
        if (!CheatCodeActivated[1])
        {
            RestoreEnemies(1);
            codeEV.start();
        }
        else
        {
           enlargeEnemies();
            codeEV.start();

            CheatCodeActivated[0] = false;
        }
    }

    public void ChangeGravity()
    {
        Debug.Log("attempt Gravity");
        CheatCodeActivated[2] = !CheatCodeActivated[2];
        if (!CheatCodeActivated[2])
        {
            Debug.Log("Gravity restored");
            Physics2D.gravity=new Vector2(0,-100);
            codeEV.start();
            for (int i = 0; i < CheatCodes.Length; i++)
            {
                CheatCodes[i] = 0;
            }
        }
        else
        {
            Debug.Log("Low Gravity");
            Physics2D.gravity = new Vector2(0, -50);
            codeEV.start();
            for (int i = 0; i < CheatCodes.Length; i++)
            {
                CheatCodes[i] = 0;
            }
        }
    }

    public void RestoreEnemies(int codeNum)
    {
        int flipMod = 1;
        if (codeNum==0)
        {
            foreach (Transform enemy in GM.CC.enemies)
            {
                if (enemy.GetComponent<MinionScript>().facingRight)
                {
                    flipMod = -1;
                }
                else
                {
                    flipMod = 1;
                }
                enemy.localScale = new Vector3(10*flipMod, 10, 10);
                for (int i = 0; i < CheatCodes.Length; i++)
                {
                    CheatCodes[i] = 0;
                }

            }
        }
        if (codeNum==1)
        {
            foreach (Transform enemy in GM.CC.enemies)
            {
                if (enemy.GetComponent<MinionScript>().facingRight)
                {
                    flipMod = -1;
                }
                else
                {
                    flipMod = 1;
                }
                enemy.localScale = new Vector3(10*flipMod, 10, 10);
                for (int i = 0; i < CheatCodes.Length; i++)
                {
                    CheatCodes[i] = 0;
                }

            }
        }
    }
}
