using UnityEngine;
using System.Collections;

public class ProjectileSpawner : RemoteControllable {

	public Transform Projectile;
	public Transform[] ProjectileList = new Transform[5];
	public bool hasNoRigidBody;
	//public Transform launchSmoke;
	public float delayedStart;
	public float xForce;
	public float yForce;
	public float cooldown=2f;
	public bool StartOn;
	public bool singleFire;
	int currentProjectile=0;
    LavaGeiser LG;
    GeyserDetector GD;
	void Awake () {
		if(RemoteControl){
			RTS = GetComponent<RemoteTriggerScript> ();
		}
        if (GetComponent<LavaGeiser>() != null)
        {
            LG = GetComponent<LavaGeiser>();
        }
        if (GetComponent<GeyserDetector>() != null)
        {
            GD = GetComponent<GeyserDetector>();
        }
        CreateProjectiles();
	}
	void Start(){
		CheckDifficulty ();
	}
	void Update(){
		if (disabled) {
			return;
		}
        if (GD != null)
        {
            if (GD.ulmockNear)
            {
                if (RTS.Activated)
                {
                    RTS.Activated = false;
                }
                return;
            }
        }
		if(RemoteControl){
			CheckActiveStatus ();
		}
	}

	void FixedUpdate(){
		CheckDifficulty();
		if (disabled) {
			return;
		}
        if (GD != null)
        {
            if (GD.ulmockNear)
            {
                if (RTS.Activated)
                {
                    RTS.Activated = false;
                }
                return;
            }
        }
        delayedStart += Time.deltaTime;
		if (RemoteControl) {
			activated=RTS.Activated;
			if(activated){
                for(int i = 0; i < ProjectileList.Length; i++)
                {
                    if (ProjectileList[i].GetComponent<Animator>() == null)
                    {
                        ProjectileList[i].GetComponent<Collider2D>().enabled = true;
                    }
                    }		
				if (delayedStart > cooldown) {
					Fire ();
					delayedStart=0;
				}
				if(singleFire){
                    RTS.Activated=false;
				}
            }
        }
        else if(activated){
			if (delayedStart > cooldown) {
				Fire ();
				delayedStart=0;
			}
		}
	}

    void Fire()
    {
        //Instantiate (launchSmoke, transform.position, transform.rotation);
        ProjectileList[currentProjectile].position = transform.position;
        Rigidbody2D PRbody = ProjectileList[currentProjectile].GetComponent<Rigidbody2D>();
        PRbody.isKinematic = false;
        PRbody.velocity = new Vector2(0, 0);
        PRbody.constraints = RigidbodyConstraints2D.None;
        if (ProjectileList[currentProjectile].GetComponent<Animator>() != null)
        {
            Animator ProjectileAnim = ProjectileList[currentProjectile].GetComponent<Animator>();
            ProjectileAnim.SetBool("Activated", true);
            ProjectileAnim.SetBool("Absorb", false);
            ProjectileAnim.SetBool("Destroy", false);

        }
        if (!hasNoRigidBody)
        {
            ProjectileList[currentProjectile].transform.position = transform.position;
            ProjectileList[currentProjectile].GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce));
        }
        currentProjectile += 1;
        if (currentProjectile == ProjectileList.Length)
        {
            currentProjectile = 0;
        }
    }
    void CreateProjectiles()
    {
        if (ProjectileList[0] != null)
        {
            return;
        }

        for (int i = 0; i < ProjectileList.Length; i++)
        {
            Transform P;
            P = Instantiate(Projectile, transform.position, transform.rotation) as Transform;
            P.GetComponent<Rigidbody2D>().isKinematic = true;
            ProjectileList[i] = P;
        }
    }
}
