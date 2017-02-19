using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSlots : MonoBehaviour {

	public GameObject slot;
	public float initial_x;
	public float initial_y;
	float local_scale_offset = 90.0f;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
			GameObject cur_slot = (GameObject)Instantiate (slot);
			cur_slot.GetComponent<RectTransform> ().localPosition = new Vector3 (initial_x, initial_y, 0.5f);
			cur_slot.transform.SetParent (this.transform, false);
			initial_y -= local_scale_offset;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
