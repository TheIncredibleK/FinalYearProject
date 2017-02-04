using UnityEngine;
using System.Collections;

public class OnTrigDestroy : MonoBehaviour {

	public GameObject deathParticles;
	public GameObject deathDrop;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bullet") {
			Destroy (other.gameObject);
			if (deathParticles != null) {
				Instantiate (deathParticles, this.transform.position, Quaternion.identity);
			}
			if (deathDrop != null) {
				Instantiate (deathDrop, this.transform.position, Quaternion.identity);
			}
			Destroy (this.gameObject);
		}
	}
}
