using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_listener : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("rocket triger");
        Destroy(this.gameObject);
    }
}
