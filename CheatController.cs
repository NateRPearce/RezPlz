using UnityEngine;
using System.Collections;

public class CheatController : GameStateFunctions {

    public Transform[] enemies;
    // Use this for initialization
	void Start () {
        FindGM();
        GM.CC = this;
        StartCoroutine("CreateList");
    }
	
	public IEnumerator CreateList()
    {
        yield return new WaitForSeconds(0.1f);
        enemies = new Transform[GM.numberOfEnemies];
    }
}
