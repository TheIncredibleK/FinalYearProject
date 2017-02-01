using UnityEngine;
using System.Collections;

public class OnTrigDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	

    void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(other.gameObject);
    }
}
