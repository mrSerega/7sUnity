using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleship_tactic_script : MonoBehaviour {

    public GameObject rocket_prefab;

    public int hp = 0;

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
	public Vector3 wish_angle_1;

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
    private void Update()
    {
		if (!move())
		{
			Debug.Log("!!!!");
			rot();
		}
		mng();
    }

	private void mng()
	{
		if(com_counter >= commands.Count)
		{
			return;
		}
		float cur_time = Time.time;
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
				case 5:
					setRotation(commands[com_counter][2]);
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
				//center
				obj = Instantiate(rocket_prefab) as GameObject;
				obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				obj.transform.Rotate(0, tr.localRotation.eulerAngles.y + 90, 0);
				obj.transform.Translate(0, 2f, -1f);
				obj.transform.Rotate(0, -(90), 0);
				//left
				obj = Instantiate(rocket_prefab) as GameObject;
				obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				obj.transform.Rotate(0, tr.localRotation.eulerAngles.y + 90, 0);
				obj.transform.Translate(0.1f, 2f, -0.7f);
				obj.transform.Rotate(0, -(90), 0);
				//right
				obj = Instantiate(rocket_prefab) as GameObject;
				obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				obj.transform.Rotate(0, tr.localRotation.eulerAngles.y + 90, 0);
				obj.transform.Translate(0.2f, 2f, -1.3f);
				obj.transform.Rotate(0, -(90), 0);
				break;
			case 0:
				//only
				obj = Instantiate(rocket_prefab) as GameObject;
				obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				obj.transform.Translate(0, 2, 0);
				obj.transform.Rotate(0, tr.localRotation.eulerAngles.y + 90, 0);
				break;
			case 1:
				//center
				obj = Instantiate(rocket_prefab) as GameObject;
				obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				obj.transform.Rotate(0, tr.localRotation.eulerAngles.y + 90, 0);
				obj.transform.Translate(0, 2f, -1f);
				obj.transform.Rotate(0, -(270), 0);
				//left
				obj = Instantiate(rocket_prefab) as GameObject;
				obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				obj.transform.Rotate(0, tr.localRotation.eulerAngles.y + 90, 0);
				obj.transform.Translate(0.1f, 2f, -0.7f);
				obj.transform.Rotate(0, -(270), 0);
				//right
				obj = Instantiate(rocket_prefab) as GameObject;
				obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				obj.transform.Rotate(0, tr.localRotation.eulerAngles.y + 90, 0);
				obj.transform.Translate(0.2f, 2f, -1.3f);
				obj.transform.Rotate(0, -(270), 0);
				break;
		}
        
    }

	private bool move()
	{
		if (tr.position.y < -2)
		{
			Destroy(this.gameObject);
		}

		if (isOnPlace()) return false;
		Vector3 destination_vector = current_destination - tr.position;
		float wish_angle = Mathf.Rad2Deg * Mathf.Atan2(destination_vector.z, destination_vector.x);
		float cur_angle = -tr.rotation.eulerAngles.y;
		if (cur_angle > 180) cur_angle -= 360;
		if (wish_angle > 180) wish_angle -= 360;
		if (cur_angle < -180) cur_angle += 360;
		if (wish_angle < -180) wish_angle += 360;

		if (Mathf.Abs(wish_angle - cur_angle) > 170)
		{
			tr.Rotate(angle_x, angle_y, angle_z);
			tr.Translate(speed_x * time_step, speed_y * time_step, speed_z * time_step);
			return true;
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
		return true;
	}

	private void moveTo(Vector3 destination)
	{
		current_destination = destination;
		return;
	}

	private bool isOnPlace()
	{
		Vector3 destination_vector = current_destination - tr.position;
		float epsZ = Mathf.Abs(destination_vector.z);
		float epsX = Mathf.Abs(destination_vector.x);
			
		if (epsZ < place_eps && epsX < place_eps) return true;
		return false;
	}

	private void rot()
	{
		float wish_angle = wish_angle_1.y;
		float cur_angle = tr.rotation.eulerAngles.y;
		if (cur_angle > 180) cur_angle -= 360;
		if (wish_angle > 180) wish_angle -= 360;
		if (cur_angle < -180) cur_angle += 360;
		if (wish_angle < -180) wish_angle += 360;

		if (Mathf.Abs(wish_angle - cur_angle) < 5)
		{
			return;
		}
		if(wish_angle < cur_angle)
		{
			tr.Rotate(angle_x, -angle_y, angle_z);
		}
		if (wish_angle > cur_angle)
		{
			tr.Rotate(angle_x, angle_y, angle_z);
		}
		Debug.Log(wish_angle);
	}

	private void setRotation(float y_angle)
	{
		wish_angle_1.y = y_angle;
	}

}
