using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;

public class UIController : MonoBehaviour {
    Controller flyController;
    GestureRecogniser gestureRecogniser;
    public GameObject UI;
    public GameObject created_ui;
    Transform camera_;
    public GameObject camera2;
    float height_offset = 1.0f;
    float initialAlpha = 0.0f;
    float increaseAlpha = 0.08f;
	// Use this for initialization
	void Start () {
        //Initalise flight controller
        gestureRecogniser = GetComponent<GestureRecogniser>();
        camera_ = camera2.transform;
        created_ui = null;
	    
	}


    void Update () {
        //Gets the current gesture
        System.Collections.Generic.List<Leap.Hand> hands = gestureRecogniser.getFrameHands();
        string current_gesture = gestureRecogniser.Recognise(hands[0]);
        if (current_gesture == "UI" && created_ui == null)
        {
            createUI(hands[0], (hands[0].PalmPosition.ToVector3() - hands[1].PalmPosition.ToVector3()).normalized);
        } else if (current_gesture == "UI") {
            created_ui.transform.rotation = camera_.transform.rotation;
            created_ui.transform.LookAt(camera_);
            if(initialAlpha < 1.0f)
            {
                increaseMyAlpha();
            }
            
        } else{

            DestroyImmediate(created_ui);
            created_ui = null;
        }

    }

    void createUI(Leap.Hand right_hand, Vector3 relPosBetweenHands)
    {
        initialAlpha = 0.0f;
        Vector3 r_palm_Pos = right_hand.PalmPosition.ToVector3();
        Vector3 uiPos = camera_.transform.position + (camera_.forward * .503f);
        created_ui = (GameObject)Instantiate(UI, uiPos, Quaternion.identity);
        created_ui.GetComponent<Renderer>().material.color = new Color(Color.green.r, Color.green.g, Color.green.b, initialAlpha);
    }
    void increaseMyAlpha()
    {
        initialAlpha += increaseAlpha;
        created_ui.GetComponent<Renderer>().material.color = new Color(Color.green.r, Color.green.g, Color.green.b, initialAlpha);
    }

    void changeTo(GameObject new_ui)
    {
        Debug.Log("TOUCHED");
        UI = new_ui;
        Destroy(created_ui.gameObject);
        created_ui = null;
    }
}
