using UnityEngine;
using System.Collections;

public class WaterScript : SoundFunctions {


    [FMODUnity.EventRef]
    string splashSound = "event:/TreeLevel/TreeLevel_Player_Water_Splash_3";
    FMOD.Studio.EventInstance splashEV;

    public Transform splash;
    public Transform SplashingOBJ;
    public float sameSplashCooldown;
    bool coolingDown;
    void Start()
    {
        splashEV = FMODUnity.RuntimeManager.CreateInstance(splashSound);
    }

    void OnTriggerStay2D(Collider2D other){
		if (other.GetComponentInChildren<StungCorpsePlayerHolder>()!=null && other.GetComponent<Rigidbody2D> () != null) {
			StungCorpsePlayerHolder SCPH=other.GetComponentInChildren<StungCorpsePlayerHolder>();
			Rigidbody2D rbody=other.GetComponent<Rigidbody2D> ();
			if(SCPH.HoldingPlayer){
				if(rbody.velocity.y<-7){
					other.GetComponent<Rigidbody2D> ().AddForce(new Vector2(0,400));
				}else if(rbody.velocity.y<20){
					other.GetComponent<Rigidbody2D> ().AddForce(new Vector2(0,300));
				}
			}else{
				if(rbody.velocity.y<0){
					other.GetComponent<Rigidbody2D> ().AddForce(new Vector2(0,300));
				}else if(rbody.velocity.y>0){
					other.GetComponent<Rigidbody2D> ().AddForce(new Vector2(0,100));
				}
			}
		}
		if (other.tag == "Enemy"&& other.GetComponentInParent<Rigidbody2D> () != null) {
			Rigidbody2D rbody=other.GetComponentInParent<Rigidbody2D> ();
			if(rbody.velocity.y<0){
				rbody.AddForce(new Vector2(rbody.velocity.x*-20,rbody.velocity.y*-10));
			}
		}

		if (other.tag == "Player"&& other.GetComponent<Rigidbody2D> () != null) {
			Rigidbody2D rbody=other.GetComponent<Rigidbody2D> ();
			if(rbody.velocity.y<0){
				rbody.AddForce(new Vector2(rbody.velocity.x*-20,rbody.velocity.y*-10));
			}
		}
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Rigidbody2D>()!=null)
        {
            if (SplashingOBJ != other.transform)
            {
                float vol = other.GetComponent<Rigidbody2D>().velocity.y * -1;
                Debug.Log(vol);
                if (vol > 5f && vol < 20)
                {
                    vol = vol / 20;
                }
                else if (vol > 20)
                {
                    vol = 1;
                }else
                {
                    vol = 0;
                }
                splashEV.setVolume(vol);
                if (vol > 0)
                {
                    Instantiate(splash, new Vector3(other.transform.position.x, transform.position.y+18.5f, 0), Quaternion.identity);
                }
                PlaySplash();
            }else
            {
                if (!coolingDown)
                {
                    StartCoroutine("SplashCD");
                }
            }
            SplashingOBJ = other.transform;
        }
        if (other.GetComponentInChildren<StungCorpsePlayerHolder>() != null && other.GetComponent<Rigidbody2D>() != null)
        {
            StungCorpsePlayerHolder SCPH = other.GetComponentInChildren<StungCorpsePlayerHolder>();
            Rigidbody2D rbody = other.GetComponent<Rigidbody2D>();
            if (SCPH.HoldingPlayer)
            {
                if (rbody.velocity.y < 0)
                {
                    other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, rbody.velocity.y*-200));
                }
            }
            else
            {
                if (rbody.velocity.y < 0)
                {
                    other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
                }               
            }
        }
        if (other.tag == "Enemy" && other.GetComponentInParent<Rigidbody2D>() != null)
        {
            Rigidbody2D rbody = other.GetComponentInParent<Rigidbody2D>();
            if (rbody.velocity.y < 0)
            {
                rbody.AddForce(new Vector2(rbody.velocity.x * -20, rbody.velocity.y * -10));
            }
        }

        if (other.tag == "Player" && other.GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rbody = other.GetComponent<Rigidbody2D>();
            if (rbody.velocity.y < 0)
            {
                rbody.AddForce(new Vector2(rbody.velocity.x * -20, rbody.velocity.y * -10));
            }
        }
    }

    public void PlaySplash()
    {
        splashEV.start();
    }
    IEnumerator SplashCD()
    {
        coolingDown = true;
        yield return new WaitForSeconds(0.1f);
        SplashingOBJ = null;
        coolingDown = false;
    }
}
