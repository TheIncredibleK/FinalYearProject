using UnityEngine;
using System.Collections;

public class Flight : MonoBehaviour {
	//causes whatever object this is attached to, to simply fly straight


    public float speed = 50.0f;
	// Use this for initialization
	void Start () {
        this.transform.forward = this.transform.parent.transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;
	}
}
