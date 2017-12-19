using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_listener : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
		if(other.GetType() == typeof(CapsuleCollider))
		{
			Debug.Log(other.GetType());
			Destroy(this.gameObject);
		}		
    }
}
