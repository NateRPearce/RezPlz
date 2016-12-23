using UnityEngine;
using System.Collections;

public class ExplosionScript : SoundFunctions {


    [FMODUnity.EventRef]
    string explosionSound = "event:/Puffer/PUFFER_Explosion";
    public FMOD.Studio.EventInstance explosionEv;

    //public Transform wallBlood;
	public Transform bloodSpurt;
	public float power;
	public bool noBlood;
	public bool disableExplosion;
    public bool infiniteExplode;
	public bool exploded;
	public bool randomize;

	void Start(){
        soundStartFunctions();
        if (!disableExplosion&&explosionSound!=" ")
        {
            explosionEv = FMODUnity.RuntimeManager.CreateInstance(explosionSound);
            explosionEv.setVolume(checkRange());
            explosionEv.start();
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if (exploded&&!infiniteExplode) {
			return;
		}
		if ((other.name == "Player1"|| other.name == "Player2")&&!noBlood) {
			Instantiate(bloodSpurt,other.transform.position,bloodSpurt.rotation);
			exploded=true;
		}

	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Explodable") {
			Vector2 playerPos=other.transform.position;
			Vector2 explosionPos=transform.position;
			if(randomize){
				other.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-power/2,power/2)*5,(Random.Range(-power/2,power/2)*5)),ForceMode2D.Impulse);
			}else{
				Vector3 explosionForce=(playerPos-explosionPos)*power;
				other.GetComponent<Rigidbody2D>().AddForce(explosionForce,ForceMode2D.Impulse);
			}
		}
		if (other.tag == "Player") {
			//Transform newBlood;
		//create position to place blood
			//Vector3 bloodPlacement=new Vector3(other.transform.position.x+Random.Range(-5,5),other.transform.position.y+Random.Range(-5,5),wallBlood.transform.position.z);
			//newBlood = Instantiate(wallBlood,bloodPlacement,transform.rotation)as Transform;
			//MaxBloodController.wallBloodCollection.Add(newBlood.gameObject);
		}
	}
    public void PlayExplosion()
    {
        if (disableExplosion || explosionSound == " ")
        {
            return;
        }
        explosionEv.setVolume(checkRange());
        explosionEv.start();
    }
}
