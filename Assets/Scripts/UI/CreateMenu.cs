using UnityEngine;
using System.Collections;

public class CreateMenu : MonoBehaviour {
	// HERE FOR HISTORIC REASONS //
	// was used to create the rudimentary menu from scratch, with other layers going to be built on top of it//
	// However, users complained of  this menu being created and fading breaking the action
	// SO the UI became a permanent fixture within the ship.

    public GameObject button;
    GameObject clickable;
    GameObject camera_;
	// Use this for initialization
	void Start () {
        clickable = CreateIntialInterface();
        camera_ = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        clickable.transform.LookAt(camera_.transform);
	}

    GameObject CreateIntialInterface()
    {

        GameObject b = GameObject.CreatePrimitive(PrimitiveType.Cube);
        BoxCollider bc = (BoxCollider)b.gameObject.AddComponent(typeof(BoxCollider));
        bc.isTrigger = true;
        b.transform.localScale = new Vector3(transform.localScale.x / 8, transform.localScale.y / 8, transform.localScale.y / 8);

        
        b.transform.position = transform.position + (transform.up * transform.localScale.x/4) + (transform.forward * b.transform.localScale.z);
        b.GetComponent<Renderer>().material.color = Color.cyan;
        b.transform.LookAt(this.transform);
        return b;
    }
}
