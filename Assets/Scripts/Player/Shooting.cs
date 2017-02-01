using UnityEngine;
using Leap;
using System.Collections;

public class Shooting : MonoBehaviour {

    GestureRecogniser gestureRecogniser;
    public GameObject gun;
    // Use this for initialization
    void Start () {
        gestureRecogniser = GetComponent<GestureRecogniser>();
    }
	
	// Update is called once per frame
	void Update () {
        System.Collections.Generic.List<Leap.Hand> hands = gestureRecogniser.getFrameHands();
        string current_gesture = gestureRecogniser.Recognise(hands[0]);
        if (current_gesture == "FIST")
        {
            Firing fire = gun.GetComponent<Firing>();
            fire.Fire();
        }
       
    }
}
