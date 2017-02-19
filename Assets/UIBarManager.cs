using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarManager : MonoBehaviour {


	public float amount_of_increase;
    float local_y;
	float local_x;
	float max_x;
	float min_x;
	float local_z;
	// Use this for initialization
	void Start () {
		max_x = this.transform.localScale.x;
		min_x = max_x * 0.05f;
		local_x = min_x;
		local_y = this.transform.localScale.y;
		local_z = this.transform.localScale.z;
		this.transform.localScale = new Vector3(local_x, local_y, local_z);
	}

	void IncreaseSize() {
		local_x += amount_of_increase;
		if (local_x > max_x) {
			local_x = max_x;
		}
		this.transform.localScale = new Vector3 (local_x, local_y, local_z);

	}

	void DecreaseSize() {
		local_x -= amount_of_increase;
		if (local_x < min_x) {
			local_x = min_x;
		}
		this.transform.localScale = new Vector3 (local_x, local_y, local_z);
	}
}
