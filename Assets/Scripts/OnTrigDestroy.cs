using UnityEngine;
using System.Collections;

public class OnTrigDestroy : MonoBehaviour {

	public GameObject deathParticles;
	public GameObject deathDrop;
	public bool Explode;
	public bool hasHealth;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bullet") {
			Destroy (other.gameObject);
			if (!hasHealth) {
				
				if (deathParticles != null) {
					Instantiate (deathParticles, this.transform.position, Quaternion.identity);
				}
				if (deathDrop != null) {
					Instantiate (deathDrop, this.transform.position, Quaternion.identity);
				}
				if (!Explode) {
					Destroy (this.gameObject);
				} else {
					for (int i = 0; i < this.transform.childCount; i++) {
						Transform cur_child = this.transform.GetChild (i);
						cur_child.gameObject.AddComponent<Rigidbody> ();
						cur_child.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5), Random.Range (-5, 5));
						cur_child.gameObject.GetComponent<Rigidbody> ().useGravity = false;
					}
					this.transform.DetachChildren ();
				}

			} else {
				this.GetComponent<OtherInventory> ().DamageHealth ();
				if (this.GetComponent<OtherInventory> ().health <= 0) {
					hasHealth = false;
				}
			}
		}
	}
}
