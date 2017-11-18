using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listener : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("battleship trigger");
        this.GetComponent<Rigidbody>().useGravity = true;
    }
}
