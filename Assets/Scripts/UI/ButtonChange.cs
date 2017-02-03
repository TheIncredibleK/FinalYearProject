using UnityEngine;
using System.Collections;
using Leap;
public class ButtonChange : MonoBehaviour {

    public Color change_color;
    public Color ui_colour;
    public Color my_colour;
    public GameObject change_to;

	// Use this for initialization
	void Start () {
        transform.GetComponent<Renderer>().material.color = my_colour;
    }
	
	// Update is called once per frame
	void Update () {
        

    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag != "UI")
        {
            this.transform.GetComponent<Renderer>().material.color = Color.black;
            ui_colour.a = 0.0f;
            this.transform.parent.GetComponent<SpriteRenderer>().material.color = ui_colour;
        }
    }

    void OnTriggerExit()
    {
        this.transform.GetComponent<Renderer>().material.color = my_colour;
    }



    private Leap.Hand GetHand(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.parent.parent != null &&
            other.transform.parent.parent.GetComponent<Leap.Hand>() != null)
            return other.transform.parent.parent.GetComponent<Leap.Hand>();
        else
            return null;
    }
}
