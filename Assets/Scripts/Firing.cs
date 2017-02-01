using UnityEngine;
using System.Collections;

public class Firing : MonoBehaviour {

    public GameObject bullet;
    bool can_fire;
    float cool_down;
    float offset = 0.50f;
    float time_since_fire;
	// Use this for initialization
	void Start () {
        can_fire = true;
        cool_down = 0.30f;
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
    }
	
    public void Fire()
    {
        if (can_fire)
        {
            can_fire = false;
            Vector3 bullet_position = transform.position;
            GameObject bull = (GameObject)Instantiate(bullet, bullet_position, Quaternion.identity);
            bull.transform.rotation = this.transform.rotation;

            
        }
    }


}
