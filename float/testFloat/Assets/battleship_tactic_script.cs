using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleship_tactic_script : MonoBehaviour {

    public GameObject rocket_prefab;

    public float hp = 0.0f;

    public float time_step = 0.1f;

    public float speed_x = 1.0f;
    public float speed_y = 0.0f;
    public float speed_z = 0.0f;

    public float angle_x = 0.0f;
    public float angle_y = 0.0f;
    public float angle_z = 0.0f;

    public Transform tr;
    public Collider cl;

    private float last_time = Time.time;

    // Use this for initialization
    void Start () {
        Debug.Log("battle ship init");
        last_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float cur_time = Time.time;
        if (cur_time - last_time > 10)
        {
            last_time = cur_time;
            GameObject obj = Instantiate(rocket_prefab) as GameObject;
        }
        //Debug.Log("battle ship move");
        //tr.Translate(speed_x * time_step, speed_y * time_step, speed_z * time_step);
        //tr.Rotate(angle_x, angle_y, angle_z);
        Debug.Log(cur_time - last_time);
    }
}
