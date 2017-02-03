using UnityEngine;
using System.Collections;

public class Flight : MonoBehaviour {

    public float speed = 10.0f;
	// Use this for initialization
	void Start () {
        this.transform.forward = this.transform.parent.transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;
	}
}
