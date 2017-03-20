using UnityEngine;
using System.Collections;

public class Firing : MonoBehaviour {

    public GameObject bullet;
    bool can_fire;
	float speed = 50.0f;
    float cool_down;
    float offset = 0.50f;
    float time_since_fire;
	public bool fire = false;
	// Use this for initialization
	void Start () {
        can_fire = true;
        cool_down = 0.60f;
        time_since_fire = 0.0f;
	}

    void Update()
    {
        if(time_since_fire < cool_down)
        {
            time_since_fire += Time.deltaTime;

        } else
        {
            can_fire = true;
            time_since_fire = 0.0f;
        }

		if (fire && can_fire) {
			Fire ();
			fire = false;
		}
    }
	
    public void Fire()
    {
        if (can_fire)
        {
            can_fire = false;
			for (int i = 0; i < this.transform.childCount; i++) {
				Vector3 bullet_position = this.transform.GetChild (i).transform.position + transform.forward * Time.deltaTime * speed;
				GameObject bull = (GameObject)Instantiate (bullet, bullet_position, Quaternion.identity);
				bull.transform.rotation = this.transform.rotation;
			}

            
        }
    }


}
