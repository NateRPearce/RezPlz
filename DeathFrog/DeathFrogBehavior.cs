using UnityEngine;
using System.Collections;

public class DeathFrogBehavior : MinionScript {
		
		int currentProjectile=0;
		public Transform[] ProjectileList = new Transform[5];
		public Transform Fireball;
		public Transform FirePoint;
		public SpriteRenderer sr;
		public float projectileSpeed;

		void Awake(){
			sr = GetComponent<SpriteRenderer> ();
			anim = GetComponent<Animator> ();
		}
		void Start(){
        FindGM();
        StartCoroutine("RegiserEnemy");
        currentSpeed = defaultSpeed;
			anim.SetBool ("Alive", true);
			setDead(false);
			dying = false;
			for(int i=0;i<ProjectileList.Length;i++){
				Transform P;
				P=Instantiate(Fireball,transform.position,transform.rotation)as Transform;
				ProjectileList[i]=P;
			}
		}
		
		
		void Update(){
			if (anim.speed == 0) {
				return;		
			}
			if (getDead()) {
				sr.sortingOrder=1;
				return;		
			}
			DeadEndCheck ();
			if (wallFound) {
				Flip();
				direction*=-1;
			}
			//GroundedCheck ();
			//Walk ();
			//EdgeCheck ();
			//Walk_Run_Check ();
			//searchForPlayers ();
		}
		
		void FixedUpdate(){
			if(atkCoolingDown){
				AttackCooldown();
			}
		}
		public void destroy(){
			Destroy (gameObject);
		}
		//stop attack animation
		public void endOfAttack(){
			anim.SetBool ("isAttacking", false);
			isAttacking = false;
		}
		public void stopAnimating(){
			anim.speed = 0;
		}
		
	public void FireBreathe(){
			{
				//Instantiate (launchSmoke, transform.position, transform.rotation);
				ProjectileList[currentProjectile].position = FirePoint.position;
				ProjectileList [currentProjectile].GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
				if (ProjectileList [currentProjectile].GetComponent<Animator> () != null) {
					Animator ProjectileAnim = ProjectileList [currentProjectile].GetComponent<Animator> ();
					ProjectileAnim.SetBool ("Activated",true);
				}
				if(facingRight){
					ProjectileList[currentProjectile].GetComponent<Rigidbody2D>().AddForce (new Vector2(projectileSpeed, 0));
				}else{
					ProjectileList[currentProjectile].GetComponent<Rigidbody2D>().AddForce (new Vector2(projectileSpeed*-1, 0));
				}
				currentProjectile += 1;
				if (currentProjectile == ProjectileList.Length) {
					currentProjectile=0;
				}
			}
		}
		public void Killed(){
			if(!getDead ()){
				
			}
		}
	}

