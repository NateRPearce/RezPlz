using UnityEngine;
using System.Collections;

public class InfiniteEnemySpawn : GameStateFunctions {

    public Transform enemy;
    public Transform patrolPoint1;
    public Transform patrolPoint2;
    public Transform spawnPoint;
    public float spawnDelay;
    Transform newEnemy;
    Patrol pScript;
    EnemyDeathScript EDS;
    BatlingBehavior BB;
    public float deadTimer;

    void Start()
    {
        FindGM();
        SpawnEnemy();
    }

    void FixedUpdate()
    {
        if (EDS.dead)
        {
            deadTimer += Time.deltaTime;
        }

        if (EDS.dead&&deadTimer>spawnDelay && !GM.PM.PDS1.playerDead && !GM.PM.PDS2.playerDead)
        {
            SpawnEnemy();
            deadTimer = 0;
            EDS.dead = false;
        }
    }


    void SpawnEnemy()
    {
        newEnemy = Instantiate(enemy, spawnPoint.position, enemy.rotation) as Transform;
        EDS = newEnemy.GetComponentInChildren<EnemyDeathScript>();
        if (newEnemy.GetComponentInChildren<BatlingBehavior>() != null)
        {
            BB = newEnemy.GetComponentInChildren<BatlingBehavior>();
            BB.enterancePoint = patrolPoint1;
            BB.StartCoroutine("EnterCastle");
        }
        if (newEnemy.GetComponentInChildren<Patrol>() == null)
        {
            return;
        }
        pScript = newEnemy.GetComponentInChildren<Patrol>();
        pScript.patrolPoint1.position = patrolPoint1.position;
        pScript.patrolPoint2.position = patrolPoint2.position;
    }
}


