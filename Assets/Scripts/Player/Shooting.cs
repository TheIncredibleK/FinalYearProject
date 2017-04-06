using UnityEngine;
using Leap;
using System.Collections;

public class Shooting : MonoBehaviour {
	//Uses gesture recogniser to
	//Check if player is creating a fist
	//If so, activates a boolean, allowing player to shoot

	// THIS IS NOW TAKEN OVER BY THE FLIGHT CONTROLLER //

    GestureRecogniser gestureRecogniser;
    public GameObject gun;
    // Use this for initialization
    void Start () {
        gestureRecogniser = GetComponent<GestureRecogniser>();
    }
	
	// Update is called once per frame
	void Update () {
        System.Collections.Generic.List<Leap.Hand> hands = gestureRecogniser.getFrameHands();
		if (hands.Count >= 1) {
			string current_gesture = gestureRecogniser.Recognise (hands [0]);
			if (current_gesture == "FIST") {
				Firing fire = gun.GetComponent<Firing> ();
				fire.Fire ();
			}
		}
    }
}
