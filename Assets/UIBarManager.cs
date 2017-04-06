using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarManager : MonoBehaviour {


	public float amount_of_increase;
	public int costOfIncrease;
	public bool healthBar;
    float local_y;
	float local_x;
	public float max_x;
	float min_x;
	float local_z;
	float size;
	float full_size = 3.5f;
	// Use this for initialization
	void Start () {
		//Maximum size
		max_x = this.transform.localScale.x;
		//Minimum size
		min_x = max_x * 0.05f;
		//Set current size to be the minimum
		local_x = min_x;
		//SEt y an z to be regular
		local_y = this.transform.localScale.y;
		local_z = this.transform.localScale.z;
		this.transform.localScale = new Vector3(local_x, local_y, local_z);
		//Set size to be the local x
		size = local_x;
		//If it's a health bar, make some alterations
		if (healthBar) {
			HealthBarSetup ();
		}
	}
//	void Update() {
//		this.transform.localScale = new Vector3 (local_x, local_y, local_z);
//	}

	//Sets up a UI bar as a health bar
	void HealthBarSetup() {
		local_x = max_x;
		min_x = 0;
		this.transform.localScale = new Vector3(local_x, local_y, local_z);
		size = local_x;
	}

	//Increases size of bar by the amount of increase available to each bar
	public void IncreaseSize() {
		local_x += amount_of_increase;
		if (local_x > max_x) {
			local_x = max_x;
		}
		size = (local_x/max_x) * full_size;
		this.transform.localScale = new Vector3 (local_x, local_y, local_z);

	}

	//Decreases the size of the bar displaying current status.
	public void DecreaseSize(float decrease, float barSize) {
		Debug.Log ("Decrease : " + decrease + " bar Size : " + barSize);
		//How much the barsize must change
		float ratioOfChange = barSize / decrease;
		//Translate this into a number that can be used by the bar sizes
		//An example would be barSize = 100
		//decrease = 10
		//100/10 = 10
		//local_x -= max_x(2)/10 = .2
		local_x -= max_x/ratioOfChange;
		if (local_x < min_x) {
			local_x = min_x;
		}
		size = local_x;
		this.transform.localScale = new Vector3 (local_x, local_y, local_z);
	}
}
