using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class FinalScreen : GameStateFunctions {


    public bool restartEnabled;
    public Transform mainCredits;
    public Transform stats;
    public float creditsTarget;
    public bool statsDoneScrolling;
    public float statsTarget;
    public float time;

	void Start () {
        StartCoroutine("EnableRestart");
        FindGM();
	}
	
	void Update () {
        if (!restartEnabled)
        {
            return;
        }
        if(Input.GetButtonDown(GM.jumpbtn[0])|| Input.GetButtonDown(GM.jumpbtn[1]))
        {
            EidolonTrigger.BossEV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            SceneManager.LoadScene(0);
        }
	}

    void FixedUpdate()
    {
        time += Time.deltaTime;
        mainCredits.localPosition = Vector3.MoveTowards(mainCredits.localPosition, new Vector3(mainCredits.localPosition.x, creditsTarget, 0), Time.deltaTime*100);
        if (statsDoneScrolling)
        {
            return;
        }
        stats.localPosition = Vector3.MoveTowards(stats.localPosition, new Vector3(stats.localPosition.x, statsTarget, 0), Time.deltaTime * 100);
    }

    IEnumerator EnableRestart()
    {
        yield return new WaitForSeconds(10);
        restartEnabled = true;
    }

}
