using UnityEngine;
using System.Collections;

public class Firing : MonoBehaviour {
	//Used for shooting any gun object

    public GameObject bullet;
    bool can_fire;

	//Tag list for if it is an enemies bullet or the players
	public enum Tag {
		Enemy, Default
	};
	public Tag myTag;
	//Speed of bullet
	float speed = 5.0f;
	//Cool down time
    float cool_down;
    float offset = 0.50f;
	//Value used to see if bullet was fired longer thanc cool down
    float time_since_fire;
	public bool fire = false;

	//base bullet damage
	float myDamage = 10.0f;
	// Use this for initialization
	void Start () {
        can_fire = true;
		cool_down = 0.60f;
        time_since_fire = 0.0f;
	}

	//Fire if can fire and am firing
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
			//Create bullet, set it's damage, set who owns it and shoot
            can_fire = false;
			for (int i = 0; i < this.transform.childCount; i++) {
				Vector3 bullet_position = this.transform.GetChild (i).transform.position + transform.forward * this.transform.localScale.z * Time.deltaTime * speed;
				GameObject bull = (GameObject)Instantiate (bullet, bullet_position, Quaternion.identity);
				if (myTag == Tag.Enemy) {
					bull.tag = "EnemyBullet";
				}
				bull.GetComponent<Bullet> ().SetDamage (myDamage);
				bull.GetComponent<Bullet> ().myOwner = this.transform.parent.gameObject.GetInstanceID ();
				bull.GetComponent<Bullet> ().shipShotFrom = this.transform.parent.gameObject;
				bull.transform.rotation = this.transform.rotation;
			}

            
        }
    }

	//Set damage to be a specific amount based on an updated number.
	public void UpdateDamage(float newDamage) {
		myDamage = newDamage;
	}


}
