﻿using UnityEngine;
using System.Collections;

public class OnTrigDestroy : MonoBehaviour {
	// HERE FOR HISTORIC REASONS //

	// Inital attempts at creating an overall
	//Object destroyer
	//Handled health, particles and drops

	//Became too much hassle, and seperating destruction into individual object types became more useful

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

//		Debug.Log ("Collision Occured : tag :" + other.tag);
		if (other.tag.Contains("Bullet")) {
			Debug.Log ("Hit");
			Destroy (other.gameObject);
			if (!hasHealth) {
				
				if (deathParticles != null) {
					Instantiate (deathParticles, this.transform.position, Quaternion.identity);
				}
				if (deathDrop != null) {
					Instantiate (deathDrop, this.transform.position, Quaternion.identity);
				}
				if (!Explode) {
					Debug.Log ("Making it to if");
					if (this.transform.childCount > 0) {
						Debug.Log (this.name + " Looping through kids");
						for (int i = 0; i < this.transform.childCount; i++) {
							Destroy (this.transform.GetChild (i));
						} 
					}
						Debug.Log ("Getting here");
						this.GetComponent<Asteroid> ().RespawnSelf ();
						Debug.Log ("Getting here 2");
						Destroy (this.gameObject);
				
					}
				} else {
					for (int i = 0; i < this.transform.childCount; i++) {
						Transform cur_child = this.transform.GetChild (i);
						cur_child.gameObject.AddComponent<Rigidbody> ();
						cur_child.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5), Random.Range (-5, 5));
						cur_child.gameObject.GetComponent<Rigidbody> ().useGravity = false;
					}
					this.transform.DetachChildren ();
				Destroy (this.gameObject);
				}

			} else {
				this.GetComponent<OtherInventory> ().DamageHealth ();
				if (this.GetComponent<OtherInventory> ().health <= 0) {
					hasHealth = false;

			}
		}
	}
}
