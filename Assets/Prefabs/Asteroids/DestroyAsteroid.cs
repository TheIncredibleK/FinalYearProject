using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroid : MonoBehaviour {
	public GameObject deathParticles;
	public GameObject deathDrop;
	public int x;
	public int z;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		//If the object is a bullet
		if (other.tag.Contains("Bullet")) {
			//If I have death particles create them
			Destroy (other.gameObject);
			if (deathParticles != null) {
				Instantiate (deathParticles, this.transform.position, Quaternion.identity);
			}
			//If i drop anything, drop it
			if (deathDrop != null) {
				Instantiate (deathDrop, this.transform.position, Quaternion.identity);
			}
			//Set me to destroyed
			GameObject.FindGameObjectWithTag ("AstMng").GetComponent<AsteroidGenerator> ().wasDestroyed [x, z] = true;
			Destroy (this.gameObject);
		}

	}
}
