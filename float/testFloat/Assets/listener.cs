using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listener : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
		if (other.GetType() == typeof(SphereCollider))
		{
			Debug.Log("battleship trigger");
			this.GetComponent<battleship_tactic_script>().hp-= rocket_movement_script.damage;
			//Debug.Log("damage:" + rocket_movement_script.damage);
			//Debug.Log("hp: "+this.GetComponent<battleship_tactic_script>().hp);
			if (this.GetComponent<battleship_tactic_script>().hp <= 0)
			{
				this.GetComponent<Rigidbody>().useGravity = true;
			}
		}
	}

}
