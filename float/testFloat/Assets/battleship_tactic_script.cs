using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleship_tactic_script : MonoBehaviour {

    public GameObject rocket_prefab;

    public float hp = 0.0f;

    public float time_step = 0.1f;

	public float place_eps = 5.0f;

    public float speed_x = 0.0f; //change only this speed
    private float speed_y = 0.0f;
    private float speed_z = 0.0f;

    private float angle_x = 0.0f;
    public float angle_y = 0.0f; //change only this angle
    private float angle_z = 0.0f;

    public Transform tr;
    public Collider cl;

    private float last_time = 0;

	public Vector3 current_destination;

    // Use this for initialization
    void Start () {
        Debug.Log("battle ship init");
        last_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		move();
        //float cur_time = Time.time;
        //if (cur_time - last_time > 2)
        //{
        //    last_time = cur_time;
        //    launch();
        //}
        //Debug.Log("battle ship move");
        //Debug.Log(cur_time - last_time);
    }

    private void launch()
    {
        GameObject obj = Instantiate(rocket_prefab) as GameObject;
        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        obj.transform.Translate(0, 2, 0);
        obj.transform.Rotate(0, tr.localRotation.eulerAngles.y+90, 0);
    }

	private void move()
	{
		if (isOnPlace()) return;
		Vector3 destination_vector = current_destination - tr.position;
		double wish_angle = Mathf.Atan2(destination_vector.z, destination_vector.x);
		double cur_angle = tr.rotation.y;

		Debug.Log(cur_angle);
		Debug.Log(wish_angle);
		Debug.Log("###");

		if (cur_angle > wish_angle)
		{
			tr.Rotate(angle_x, angle_y, angle_z);
		}
		if (cur_angle <= wish_angle)
		{
			tr.Rotate(angle_x, -angle_y, angle_z);
		}
		tr.Translate(speed_x * time_step, speed_y * time_step, speed_z * time_step);
		return;
	}

	private void moveTo(Vector3 destination)
	{
		current_destination = destination;
		return;
	}

	private Vector3 target(int id)
	{
		return new Vector3(0, 0, 0);
	} 

	private void shoot(Vector3 target_cor)
	{
		return;
	} 

	private void avoidHit(Vector3 let_cor)
	{
		return;
	}

	private int isShoot()
	{
		return 0;
	}

	private bool isOnPlace()
	{
		Vector3 destination_vector = current_destination - tr.position;
		float epsZ = Mathf.Abs(destination_vector.z);
		float epsX = Mathf.Abs(destination_vector.x);

		//Debug.Log(epsZ);
		//Debug.Log(epsX);
		//Debug.Log("###");
			
		if (epsZ < place_eps && epsX < place_eps) return true;
		return false;
	}

}
