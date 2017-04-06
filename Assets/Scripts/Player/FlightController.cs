using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;

public class FlightController : MonoBehaviour
{
	Controller flyController;
	GestureRecogniser gestureRecogniser;
	public GameObject vehicle;
	float rotateAngleX;
	float rotateAngleZ;
	float rateOfChange = 0.00058f;
	float topRot = 1.0f;
	public float topSpeed = 10.0f;
	public float speed = 0.0f;
	public float topDamped = 5.0f;
	float acceleration = 0.07f;
	float handling = 30.0f;
	Vector3 velocity = new Vector3(0.0f,0.0f,0.0f);

	// Use this for initialization
	void Start()
	{
		//Initalise flight controller
		flyController = new Controller();
		gestureRecogniser = GetComponent<GestureRecogniser>(); 
		rotateAngleX = 0.0f;
		rotateAngleZ = 0.0f;


	}

	void Update()
	{

		ShipControls();

	}

	void ShipControls()
	{
		//Gets The Current Hands In Frame
		System.Collections.Generic.List<Leap.Hand> hands = gestureRecogniser.getFrameHands();
		if (hands.Count == 2) {
			//Gets current gestures of each hand
			string left_hand_gesture = gestureRecogniser.Recognise (hands [0]);
			Leap.Hand r_hand = hands [1];
			Leap.Hand l_hand = hands [0];
			//Shoot if right hand is in a fist
			if (left_hand_gesture == "FIST") {

					vehicle.transform.FindChild ("MyGuns").gameObject.GetComponent<Firing> ().fire = true;
			}

			//If there is a right hand
			if (r_hand != null) {
					//Get angles required and pass them into their respective functions
					float RollAngle = r_hand.PalmNormal.Roll;
					float PitchAngle = r_hand.Direction.Pitch;
					Tilt (RollAngle);
					Rise (PitchAngle);
			}

			//Cap the speed at the top speed
			if (speed <= topSpeed) {
				speed += acceleration;
				acceleration += .001f;
			}


			//Added slipperiness to flight, to make it more natural
			velocity = (velocity.normalized + (vehicle.transform.forward) / (handling * 1.5f)) * speed * Time.deltaTime;
		} else {
			//Dampen speed and acceleration
			speed *= .99f;
			acceleration *= .99f;
		}

		//Move vehicle by velocity
		vehicle.transform.position += velocity;




	}

	void Tilt(float Roll)
	{

		//Check if rolling left or right
		if (Roll > 0.0f) {
			//Constaining the rolling between these two values means players can fly straight, instead of constantly turning
			if (Roll > 1.0f && Roll < 2.0f) {
				//Find out new target rotation
				Quaternion targetRotation = Quaternion.AngleAxis((1.0f * Mathf.Sign (Roll)), Vector3.up);
				//Slerp to new target, using the handling as the time constraint
				vehicle.transform.rotation = Quaternion.Slerp(vehicle.transform.rotation , vehicle.transform.rotation *= targetRotation, handling * Time.deltaTime);
			}
		} else {
			//Constaining the rolling between these two values means players can fly straight, instead of constantly turning
			if(Roll > -2.4f && Roll < -1.5f){
				//Find out new target rotation
				Quaternion targetRotation = Quaternion.AngleAxis((1.0f * Mathf.Sign (Roll)), Vector3.up);
				//Slerp to new target, using the handling as the time constraint
				vehicle.transform.rotation = Quaternion.Slerp(vehicle.transform.rotation , vehicle.transform.rotation *= targetRotation, handling * Time.deltaTime);
			}

		}
	}
	void Rise(float Pitch)
	{
		float direction = 1.0f;
		//Depending on pitch value, change direction
		//And rotate
		if (Pitch > 1.6f) {
			direction *= -1;
			Quaternion targetQuat = Quaternion.AngleAxis (direction, Vector3.left);
			vehicle.transform.rotation = Quaternion.Slerp(vehicle.transform.rotation, vehicle.transform.rotation *= targetQuat, handling * Time.deltaTime);
		} else if (Pitch < 0.5f) {
			Quaternion targetQuat = Quaternion.AngleAxis (direction, Vector3.left);
			vehicle.transform.rotation = Quaternion.Slerp(vehicle.transform.rotation, vehicle.transform.rotation *= targetQuat, handling * Time.deltaTime);
		}

	}

	//If hit asteroid, speed goes down
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Asteroid") {
			Debug.Log ("Colliding");
			speed = speed / 2;
		}
	}

}
