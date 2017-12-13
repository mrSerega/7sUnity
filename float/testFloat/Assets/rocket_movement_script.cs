using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_movement_script : MonoBehaviour {

    public float ttl = 30.0f;

    public float hp = 0.0f;

    public float time_step = 0.1f;

    public float speed_x = 0.0f;
    public float speed_y = 0.0f;
    public float speed_z = 20.0f;

    public float angle_x = 0.0f;
    public float angle_y = 0.0f;
    public float angle_z = 0.0f;

    public Transform tr;

	// Use this for initialization
	void Start () {
        ttl = ttl + Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        tr.Translate(speed_x*time_step, speed_y*time_step, speed_z*time_step);
        if(tr.position.y>0) tr.Translate(0, -0.05f, 0);
        if (Time.time >= ttl){
            Destroy(this.gameObject);
        }
    }

}
