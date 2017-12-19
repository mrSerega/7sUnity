using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listener : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
		//Debug.Log(other.GetType());
		//if (other.GetType() == typeof(SphereCollider))
		//{
		//	Debug.Log("SEE!!!");
		//}
		if (other.GetType() == typeof(CapsuleCollider))
		{
			Debug.Log("battleship trigger");
			this.GetComponent<Rigidbody>().useGravity = true;
		}
	}

}
