using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_camera_move : MonoBehaviour {

	public Transform tr;
	public Vector3 current_destination;

	public float speed_x = 0.0f; //change only this speed
	public float speed_y = 0.0f;
	public float speed_z = 0.0f;

	public float time_step = 0.1f;
	public float place_eps = 20.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		move();
	}

	private void move()
	{
			tr.Translate(speed_x * time_step, speed_y * time_step, speed_z * time_step);
			return;
	}
}
