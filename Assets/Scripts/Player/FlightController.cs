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
    float topSpeed = 0.06f;


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
        System.Collections.Generic.List<Leap.Hand> hands = gestureRecogniser.getFrameHands();
        string current_gesture = gestureRecogniser.Recognise(hands[1]);
        string for_ui = gestureRecogniser.Recognise(hands[0]);
        Leap.Hand r_hand = hands[1];
		Debug.Log ("Pitch: " + r_hand.Direction.Pitch);
        if (current_gesture != "FIST")
        {
            if (for_ui != "UI")
            {
                //We only care about the right hand in this instance.
                Vector3 relPosOfFingersTilt = (r_hand.Fingers[0].StabilizedTipPosition - r_hand.Fingers[4].StabilizedTipPosition).ToVector3();

                Vector3 avgVec = r_hand.Fingers[0].StabilizedTipPosition.ToVector3();
                avgVec += r_hand.Fingers[1].StabilizedTipPosition.ToVector3();
                avgVec += r_hand.Fingers[2].StabilizedTipPosition.ToVector3();
                avgVec += r_hand.Fingers[3].StabilizedTipPosition.ToVector3();
                avgVec += r_hand.Fingers[4].StabilizedTipPosition.ToVector3();
                avgVec = avgVec / 5;
                Vector3 relPosFingersRise = (r_hand.PalmPosition.ToVector3() - avgVec);


                Vector3 tiltVector = Vector3.Cross(vehicle.transform.forward.normalized, vehicle.transform.right);
                Vector3 riseVector = Vector3.Cross(vehicle.transform.forward.normalized, vehicle.transform.up);

				float RollAngle = r_hand.PalmNormal.Roll;
				float PitchAngle = r_hand.Direction.Pitch;
				Tilt(RollAngle, tiltVector);
				Rise (PitchAngle, riseVector);
            }

            vehicle.transform.position += vehicle.transform.forward * topSpeed;
        }


    }

	bool Tilt(float Roll, Vector3 tiltVector)
    {
        //Debug.Log((Mathf.Abs(relPosOfFingers.z)));

		if (Roll > 0.0f) {
			if (Roll > 1.0f && Roll < 2.0f) {
				//float angle = topRot * (Mathf.Sign(relPosOfFingers.z));
				//Quaternion targetRotation = vehicle.transform.rotation * Quaternion.AngleAxis(-angle, tiltVector);
				//vehicle.transform.rotation = targetRotation;
				Quaternion targetRotation = Quaternion.AngleAxis((1.0f * Mathf.Sign (Roll)), Vector3.up);
				vehicle.transform.rotation *= targetRotation;
				return true;
			}
		} else {
			if(Roll > -2.4f && Roll < -1.5f){
				vehicle.transform.Rotate (vehicle.transform.up * (1.0f * Mathf.Sign (Roll)));
				Quaternion targetRotation = Quaternion.AngleAxis((1.0f * Mathf.Sign (Roll)), Vector3.up);
				vehicle.transform.rotation *= targetRotation;
				return true;
			}
				
		}

		return false;
	}
	void Rise(float Pitch, Vector3 riseVector)
    {
		float direction = 1.0f;
		if (Pitch > 1.6f) {
			direction *= -1;
			Quaternion targetQuat = Quaternion.AngleAxis (direction, Vector3.left);
			vehicle.transform.rotation *= targetQuat;
		} else if (Pitch < 0.5f) {
			Quaternion targetQuat = Quaternion.AngleAxis (direction, Vector3.left);
			vehicle.transform.rotation *= targetQuat;
		}

	}

	
}
