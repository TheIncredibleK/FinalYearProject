using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWithinDistance : MonoBehaviour {

	GameObject player;
	public float threshold;
	public AudioClip clip;
	bool isPlaying = false;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Vector3.Distance (player.transform.position, this.transform.position));
		if (Vector3.Distance (player.transform.position, this.transform.position) < threshold) {
			if (isPlaying == false) {
				isPlaying = true;
				PlaySound ();
			}
		} else {
			if (isPlaying) {
				isPlaying = false;
				StopSound ();
			}

		}
		
	}


	void PlaySound(){
		this.GetComponent<AudioSource> ().clip = clip;
		this.GetComponent<AudioSource> ().Play ();
		this.GetComponent<AudioSource> ().loop = true;

	}

	void StopSound() {
		this.GetComponent<AudioSource> ().Stop ();
	}
}
