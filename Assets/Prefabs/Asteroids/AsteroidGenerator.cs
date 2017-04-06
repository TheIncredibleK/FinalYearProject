using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour {
	//Prefab list that can be used to generate resources
	public GameObject[] prefabs;
	public GameObject smallAsteroid;
	GameObject[,] Resources;
	Vector3[,] resourcePositions;
	public bool[,] wasDestroyed;
	GameObject player;
	//Type of generation used
	public enum GenerationType {
		PerlinCantor, GeneralRandom, JustPerlin
	};
	public GenerationType HowGenerate;
	//Numbers controlling size of levels
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

	//This was a combination of the Cantor function
	// A function that returns a unique value for any two numbers passed in,
	//And Perlin Noise, I used this as an experiment
	//To see what sort of interesting generation patterns
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

	//Purely random asteroid field start
	void RandomStart() {
		for (int i = 0; i < numPerLevel; i++) {
			float x = (Random.Range (.50f, levelSize)) * Mathf.Sign(Random.Range(-1,1));
			float y = (Random.Range (.50f, levelSize)) * Mathf.Sign(Random.Range(-1,1));
			float z = (Random.Range (.50f, levelSize)) * Mathf.Sign(Random.Range(-1,1));

			Instantiate (smallAsteroid, new Vector3 (x, y, z), Quaternion.identity);

		}
	}

	//The actual Cantor function
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


	//Perlin Noise Resource Generation
	void ThreeDeePerlin(){

		//Create the x and z sices
		size_x = (int)numPerLevel / 4;
		size_z = (int)numPerLevel / 4;
		//Initialise the x value passed into the perlin noise function
		float x_off = 0.0f;
		//Value used to help control hieght of objects
		float other_dist = numPerLevel / 2;
		//Create lists used to generate terrain

		//Actual resource objects
		Resources = new GameObject[size_x, size_z];
		//Positions of objects
		resourcePositions = new Vector3[size_x, size_z];
		//If this object was destroyed
		wasDestroyed = new bool[size_x, size_z];
		for (int x = 0; x < size_x; x++) {
			float z_off = 0.0f;
			for (int z = 0; z < size_z; z++) {
				
				float height = Mathf.PerlinNoise (x_off, z_off);
				if (height < threshold) {
					//If heigh is within wanted values, create vector using Perlin
					Resources[x,z] = null;
					resourcePositions [x, z] = new Vector3 ((x + (std_dist * height) * x * 2.0f) - (other_dist), 
						(levelSize * height) - levelSize / 2, (z + (std_dist * height) * z * 2.0f) - (other_dist));
				} else {
					Resources [x, z] = null;
					resourcePositions [x, z] = new Vector3 (-9999,-9999,-9999);
				}
				//Ensure all value begin no tbeing destroyed
				wasDestroyed [x, z] = false;
				z_off += 0.1f;
			}
			x_off += 0.1f;
		}
	}


	void Update() {
			ManageResources ();
	}

	//If resources are outside of minimum allowed distance, destroy
	//Else, if they have not been destroyed/respawned, create
	void ManageResources() {
		for (int x = 0; x < size_x; x++) {
			for (int z = 0; z < size_z; z++) {
				if (Vector3.Distance (resourcePositions [x, z], player.transform.position) > minDistance && wasDestroyed[x,z] == false) {
					Destroy (Resources [x, z]);
					Resources [x, z] = null;
				} else {
					if(Resources[x,z] == null) {
						if (wasDestroyed [x, z]) {
						} else {
							Resources [x, z] = Instantiate (prefabs [WhatAmI (x, z)], resourcePositions [x, z], Quaternion.identity);
							Resources [x, z].GetComponent<DestroyAsteroid> ().x = x;
							Resources [x, z].GetComponent<DestroyAsteroid> ().z = z;
						}
					}
				}
			}
		}
	}

	//Set destroyed to be true
	public void DestroyedAsteroid(int x, int z) {
		wasDestroyed [x, z] = true;
	}

	//Set destroyed to be false
	void ReestablishAsteroid(int x, int z) {
		wasDestroyed [x, z] = false;
		Debug.Log ("Respawned");
	}

	//Decide what resource to be created
	int WhatAmI(int x, int z) {
		//Ensure 30% are asteroids
		float justAsteroid = Random.Range (0, 100);
		if (justAsteroid < 30.0f) {
			return Random.Range(0,2);
		}
		//Get a random out of the list of prefabs
		//This damper has less of an effect the further away it is, meaning the more rare items will be selected more
		float thisRand = Random.Range (0, prefabs.Length);
		float damper = Mathf.Clamp01(Vector3.Distance (new Vector3 (0, 0, 0), resourcePositions [x, z]));

		return (int)(thisRand * damper);

	}

	//Return random resource
	//This is used for seek behaviours
	public GameObject requestRandomAsteroid() {
		bool found = false;
		GameObject randomAsteroid = null;
		while (!found) {
			int x = Random.Range (2, size_x);
			int z = Random.Range (2, size_z);
			if (Resources[x, z] != null) {
				randomAsteroid = Resources [x, z];
				found = true;
			}
		}
		return randomAsteroid;
	}

	//Print all asteroid positions
	public void prettyPrintAsteroids() {
		for (int i = 0; i < size_x; i++) {
			for (int j = 0; j < size_z; j++) {
				Vector3 cur_ast = resourcePositions [i, j];
				Debug.Log("x : " + cur_ast.x + "y: " + cur_ast.y + "z: " + cur_ast.z);
			}
		}

	}

	//Respawn an asteroid
	public void Respawn(int x, int z) {
		Debug.Log ("Making it to over arcing respawn func");
		StartCoroutine (Respawn_ (x, z));
		Debug.Log ("leaving overarcing respawn func");
	}

	//Co routine to respawn an asteroid after a certaina mount of time
	IEnumerator Respawn_(int x, int z) {
		DestroyedAsteroid (x, z);
		yield return new WaitForSeconds (60.0f);
		ReestablishAsteroid (x, z);
	}
}