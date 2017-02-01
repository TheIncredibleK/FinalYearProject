using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;

public class GestureRecogniser : MonoBehaviour
{

    Controller gestureController;
    public GameObject camera;

    // Use this for initialization
    void Start()
    {
        //Initalise flight controller
        

    }

    public System.Collections.Generic.List<Leap.Hand> getFrameHands()
    {

        gestureController = new Controller();
        //Gets an instance of the current frame of leap motion objects (I believe).
        Frame curframe = gestureController.Frame();
        System.Collections.Generic.List<Leap.Hand> hands = new System.Collections.Generic.List<Leap.Hand>();
        //In this case we care about both hands. This will call a function that recognises the current gesture and returns a string code based on it.
        if (curframe.Hands.Count == 2)
        {

            //Ensures correct hand labeling
            Leap.Hand r_hand = curframe.Hands[0];
            Leap.Hand l_hand = curframe.Hands[1];

            if (curframe.Hands[0].IsLeft)
            {
                r_hand = curframe.Hands[1];
                l_hand = curframe.Hands[0];
            }
            hands.Add(l_hand);
            hands.Add(r_hand);
        }
        return hands;
        
    }

    public string Recognise(Leap.Hand cur_hand)
    {
        //Initially assuming no gesture
        string gesture = "NONE";
        //If palm faces camera
        bool result = checkPalmFacingCamera(cur_hand);
        if (result)
        {
            gesture = "UI";
        }
        //If making fist
        bool result_fist = checkFist(cur_hand);
        if (result_fist) {
            gesture = "FIST";
        }
        
   
        //Return gesture here
        return gesture;

        //There is a gesture hierarchy of sorts,
        //If the user is making a fist, we can ignore UI for example
    }


    bool checkFist(Leap.Hand cur_hand)
    {
        //Checking if the two middle fingers average distace from palm is a set distance
        Vector3 palm = cur_hand.PalmPosition.ToVector3();
        Vector3 middle_finger = cur_hand.Fingers[2].StabilizedTipPosition.ToVector3();
        Vector3 ring_finger = cur_hand.Fingers[3].StabilizedTipPosition.ToVector3();

        float fing_to_palm_distance = ((Vector3.Distance(ring_finger, palm) + Vector3.Distance(middle_finger, palm)) / 2.0f) / 100.0f;

        if (fing_to_palm_distance < 0.7f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool checkPalmFacingCamera(Leap.Hand cur_hand)
    {
        //Checking if the dot prodct of the Palms forward, here called 'PalmNormal',
        //And the relPos between camera and palm is < 0
        Vector3 palm_forward = (cur_hand.PalmNormal.ToVector3());
        Vector3 palm_pos = cur_hand.PalmPosition.ToVector3();
        float dot = Vector3.Dot(palm_forward, (palm_pos - camera.transform.position).normalized);
        if(dot < 0)
        {
            return true;
        }
        return false;
    }
}
