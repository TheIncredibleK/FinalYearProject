using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour {

	public GameObject smallAsteroid;
	public float levelSize;
	public float numPerLevel;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numPerLevel; i++) {
			float x = (Random.Range (.50f, levelSize)) * Mathf.Sign(Random.Range(-1,1));
			float y = (Random.Range (.50f, levelSize)) * Mathf.Sign(Random.Range(-1,1));
			float z = (Random.Range (.50f, levelSize)) * Mathf.Sign(Random.Range(-1,1));

			Instantiate (smallAsteroid, new Vector3 (x, y, z), Quaternion.identity);

		}
	}
}
