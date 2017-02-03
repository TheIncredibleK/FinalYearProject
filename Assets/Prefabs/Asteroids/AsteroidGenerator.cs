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
			float x = Random.Range (-levelSize, levelSize);
			float y = Random.Range (-levelSize, levelSize);
			float z = Random.Range (-levelSize, levelSize);

			Instantiate (smallAsteroid, new Vector3 (x, y, z), Quaternion.identity);
		}
	}
}
