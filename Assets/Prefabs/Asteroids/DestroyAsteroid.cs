using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroid : MonoBehaviour {
	public GameObject deathParticles;
	public GameObject deathDrop;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Bullet") {
			Destroy (other.gameObject);
			if (deathParticles != null) {
				Instantiate (deathParticles, this.transform.position, Quaternion.identity);
			}
			if (deathDrop != null) {
				Instantiate (deathDrop, this.transform.position, Quaternion.identity);
			}
			Debug.Log ("Destroyed");
			Destroy (this.gameObject);
		}

	}
}
