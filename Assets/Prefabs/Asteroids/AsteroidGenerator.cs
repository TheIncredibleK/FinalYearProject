using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour {

	public GameObject smallAsteroid;
	GameObject[,] Asteroids;
	Vector3[,] asteroidPositions;
	GameObject player;
	public enum GenerationType {
		PerlinCantor, GeneralRandom, JustPerlin
	};
	public GenerationType HowGenerate;
	public float levelSize;
	public float numPerLevel;
	public float numLevels;


	//Variables for asteroid spawn control
	int size_x;
	int size_z;
	public float minDistance;


	//Perlin Values
	public float threshold;
	public float scale;
	public float std_dist;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		if (HowGenerate == GenerationType.GeneralRandom) {
			RandomStart ();
		}
		if(HowGenerate == GenerationType.PerlinCantor){
			PerlinCantorStart ();
		}
		if (HowGenerate == GenerationType.JustPerlin) {
			ThreeDeePerlin ();
		}

	}

	void PerlinCantorStart() {
		for (int i = 0; i < numLevels; i++) {
			for (int j = 0; j < numPerLevel; j++) {

				float perlinVal = 0.0f;
				float x;
				float y;
				float z;
				do {
					x = (Random.Range (.50f * (i + 1), levelSize*(i + 1))) * Mathf.Sign (Random.Range (-1, 1));
					y = (Random.Range (.50f*(i + 1), levelSize*(i + 1))) * Mathf.Sign (Random.Range (-1, 1));
					z = (Random.Range (.50f*(i + 1), levelSize*(i + 1))) * Mathf.Sign (Random.Range (-1, 1));

					float perlinX = CantorMapping (levelSize*(i + 1)/x, levelSize*(i + 1)/y);
					float perlinY = CantorMapping (levelSize*(i + 1)/y, levelSize*(i + 1)/z);
					perlinVal = Mathf.PerlinNoise (perlinX, perlinY);

				} while(perlinVal < 0.8f);



				Instantiate (smallAsteroid, new Vector3 (x, y, z), Quaternion.identity);

			}
		}
	}

	void RandomStart() {
		for (int i = 0; i < numPerLevel; i++) {
			float x = (Random.Range (.50f, levelSize)) * Mathf.Sign(Random.Range(-1,1));
			float y = (Random.Range (.50f, levelSize)) * Mathf.Sign(Random.Range(-1,1));
			float z = (Random.Range (.50f, levelSize)) * Mathf.Sign(Random.Range(-1,1));

			Instantiate (smallAsteroid, new Vector3 (x, y, z), Quaternion.identity);

		}
	}

	float CantorMapping(float x, float y) {
		if (x < 0) {
			x = (-x * 2) - 1;
		} else {
			x = x * 2;
		}

		if (y < 0) {
			y = -(y * 2) - 1;
		} else {
			y = y * 2;
		}

		float cantorVal = (x + y) * (x + y + 1) / 2 + x;
		return cantorVal;

	}

	void ThreeDeePerlin(){
		size_x = (int)numPerLevel / 4;
		size_z = (int)numPerLevel / 4;
		float x_off = 0.0f;
		float other_dist = numPerLevel / 2;
		Asteroids = new GameObject[size_x, size_z];
		asteroidPositions = new Vector3[size_x, size_z];

		for (int x = 0; x < size_x; x++) {
			float z_off = 0.0f;
			for (int z = 0; z < size_z; z++) {

				float height = Mathf.PerlinNoise (x_off, z_off);
				if (height < threshold) {
					Asteroids [x, z] = (GameObject)Instantiate (smallAsteroid, new Vector3 ((x + (std_dist * height) * x * 2.0f) - (other_dist), (levelSize * height) - levelSize / 2, (z + (std_dist * height) * z * 2.0f) - (other_dist)), Quaternion.identity);
					asteroidPositions [x, z] = Asteroids[x,z].transform.position;
				} else {
					Asteroids [x, z] = null;
					asteroidPositions [x, z] = new Vector3 (-9999, -9999, -9999);
				}

				z_off += 0.1f;
			}
			x_off += 0.1f;
		}
	}


	void Update() {
		ManageAsteroids ();
	}


	void ManageAsteroids() {
		for (int x = 0; x < size_x; x++) {
			for (int z = 0; z < size_z; z++) {
				if (Vector3.Distance (asteroidPositions [x, z], player.transform.position) > minDistance) {
					Destroy (Asteroids [x, z]);
					Asteroids [x, z] = null;
				} else {
					if(Asteroids[x,z] == null) {
						Asteroids [x, z] = Instantiate (smallAsteroid, asteroidPositions [x, z], Quaternion.identity);
					}
				}
			}
		}
	}

	public GameObject requestRandomAsteroid() {
		bool found = false;
		GameObject randomAsteroid = null;
		while (!found) {
			int x = Random.Range (2, size_x);
			int z = Random.Range (2, size_z);
			if (Asteroids[x, z] != null) {
				randomAsteroid = Asteroids [x, z];
				found = true;
				Debug.Log ("Returning");
			}
		}
		return randomAsteroid;
	}

	public void prettyPrintAsteroids() {
		for (int i = 0; i < size_x; i++) {
			for (int j = 0; j < size_z; j++) {
				Vector3 cur_ast = asteroidPositions [i, j];
				Debug.Log("x : " + cur_ast.x + "y: " + cur_ast.y + "z: " + cur_ast.z);
			}
		}

	}
}