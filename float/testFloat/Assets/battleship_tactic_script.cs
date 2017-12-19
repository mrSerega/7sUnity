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

	public List<string> pre_commands;
	private List<List<int>> commands;
	private int com_counter = 0;

	public Vector3 current_destination;

    // Use this for initialization
    private void Start () {
		commands = new List<List<int>>();
        Debug.Log("battle ship init");
        last_time = Time.time;
		parse_com();
		Debug.Log(commands);
    }

	private void parse_com()
	{
		foreach (string com in pre_commands)
		{
			string[] pcom = com.Split(' ');
			List<int> tmp = new List<int>();
			for (int i = 0; i < pcom.Length; ++i)
			{
				tmp.Add(int.Parse(pcom[i]));
			}
			Debug.Log("errror "+tmp);
			commands.Add(tmp);
		}
	}

    // Update is called once per frame
    void Update()
    {
		move();
		mng();
        //float cur_time = Time.time;
        //if (cur_time - last_time > 2)
        //{
        //    last_time = cur_time;
        //    launch();
        //}
        //Debug.Log("battle ship move");
        //Debug.Log(cur_time - last_time);
    }

	private void mng()
	{
		if(com_counter >= commands.Count)
		{
			Debug.Log("end of coms");
			return;
		}
		float cur_time = Time.time;
		Debug.Log("com_counter is " + commands.Count);
		if(cur_time >= last_time + commands[com_counter][1])
		{
			switch (commands[com_counter][0]) {
				case 0:
					break;
				case 1:
					moveTo(new Vector3(commands[com_counter][2], 0, commands[com_counter][3]));
					break;
				case 2:
					launch(0); //forward
					break;
				case 3:
					launch(-1); //left
					break;
				case 4:
					launch(1); //right
					break;
			}
			com_counter++;
			last_time = cur_time;
		}

	}

    private void launch(int side)
    {
		GameObject obj;
		switch (side)
		{
			case -1:
				obj = Instantiate(rocket_prefab) as GameObject;
				obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				obj.transform.Translate(0, 2, 0);
				obj.transform.Rotate(0, tr.localRotation.eulerAngles.y, 0);
				break;
			case 0:
				obj = Instantiate(rocket_prefab) as GameObject;
				obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				obj.transform.Translate(0, 2, 0);
				obj.transform.Rotate(0, tr.localRotation.eulerAngles.y + 90, 0);
				break;
			case 1:
				obj = Instantiate(rocket_prefab) as GameObject;
				obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				obj.transform.Translate(0, 2, 0);
				obj.transform.Rotate(0, tr.localRotation.eulerAngles.y + 180, 0);
				break;
		}
        
    }

	private void move()
	{
		if (isOnPlace()) return;
		Vector3 destination_vector = current_destination - tr.position;
		float wish_angle = Mathf.Rad2Deg * Mathf.Atan2(destination_vector.z, destination_vector.x);
		float cur_angle = -tr.rotation.eulerAngles.y;
		if (cur_angle > 180) cur_angle -= 360;
		if (wish_angle > 180) wish_angle -= 360;
		if (cur_angle < -180) cur_angle += 360;
		if (wish_angle < -180) wish_angle += 360;


		//Debug.Log(destination_vector);
		//Debug.Log(cur_angle);
		//Debug.Log(wish_angle);
		//Debug.Log("###");

		if (Mathf.Abs(wish_angle - cur_angle) > 170)
		{
			tr.Rotate(angle_x, angle_y, angle_z);
			tr.Translate(speed_x * time_step, speed_y * time_step, speed_z * time_step);
			return;
		}

		if (cur_angle > wish_angle)
		{
			tr.Rotate(angle_x, angle_y, angle_z);
		}
		if (cur_angle < wish_angle)
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

	public void avoidHit(Vector3 let_cor)
	{
		Debug.Log("hit");
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
