using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;

public class AddColliders : MonoBehaviour {

    Controller collisionController;
    GameObject finger_collider;
	// Use this for initialization
	void Start () {
        collisionController = new Controller();
        finger_collider = null;
	}
	
	// Update is called once per frame
	void Update()
    {
        Frame cur_frame = collisionController.Frame();

        for (int i = 0; i < cur_frame.Hands.Count; i++)
        {
            Leap.Hand current_hand = cur_frame.Hands[i];
            if(current_hand.IsRight)
            {
                Vector3 where_to = current_hand.Fingers[1].StabilizedTipPosition.ToVector3();

                if(finger_collider != null)
                {
                    finger_collider.transform.position = where_to;
                } else
                {
                    finger_collider = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    BoxCollider bc = (BoxCollider)finger_collider.gameObject.AddComponent(typeof(BoxCollider));
                    bc.center = Vector3.zero;
                }
            }

        }
    }
}
