using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour {

	public GameObject smallAsteroid;
	public enum GenerationType {
		PerlinCantor, GeneralRandom
	};
	public GenerationType HowGenerate;
	public float levelSize;
	public float numPerLevel;
	public float numLevels;


	// Use this for initialization
	void Start () {
		if (HowGenerate == GenerationType.GeneralRandom) {
			RandomStart ();
		}
		if(HowGenerate == GenerationType.PerlinCantor){
			PerlinCantorStart ();
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
}
