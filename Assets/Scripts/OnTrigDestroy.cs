using UnityEngine;
using System.Collections;

public class OnTrigDestroy : MonoBehaviour {

	public GameObject death;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	

    void OnTriggerEnter(Collider other)
    {
		Destroy (this.gameObject);
		Destroy (other.gameObject);
		if (death != null) {
			Instantiate (death, this.transform.position, Quaternion.identity);
		}
    }
}
