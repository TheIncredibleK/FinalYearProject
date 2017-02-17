using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour {

	public GameObject smallAsteroid;
	public enum GenerationType {
		PerlinCantor, GeneralRandom, JustPerlin
	};
	public GenerationType HowGenerate;
	public float levelSize;
	public float numPerLevel;
	public float numLevels;



	//Perlin Values
	public float threshold;
	public float scale;
	public float std_dist;


	// Use this for initialization
	void Start () {
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
		float x_off = 0.0f;
		float other_dist = numPerLevel / 2;
		for (int x = 0; x < numPerLevel/4; x++) {
			float z_off = 0.0f;
			for (int z = 0; z < numPerLevel/4; z++) {

				float height = Mathf.PerlinNoise (x_off, z_off);
				if (height < threshold) {
					GameObject cur_ast = (GameObject)Instantiate (smallAsteroid, new Vector3 ((x + (std_dist * height) * x * 2.0f) - (other_dist ), levelSize * 2 * height, (z + (std_dist * height) * z * 2.0f) - (other_dist)), Quaternion.identity);
				}

				z_off += 0.1f;
			}
			x_off += 0.1f;
		}
	}
}