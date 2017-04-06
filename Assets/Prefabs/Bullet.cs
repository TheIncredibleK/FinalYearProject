using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	//Holds speed, damage, and the owner of a bullet

	public float speed = 10.0f;
	public float damage;
	public int myOwner;
	public GameObject shipShotFrom;
	// Use this for initialization
	void Start () {
		this.transform.forward = this.transform.parent.transform.forward;
	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	public void SetDamage(float newDamage) {
		damage = newDamage;
		StartCoroutine (WaitAndDie());

	}

	IEnumerator WaitAndDie() {
		yield return new WaitForSeconds (1.0f);
		Destroy (this.gameObject);
	}
}
