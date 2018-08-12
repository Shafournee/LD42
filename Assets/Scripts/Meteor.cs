using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

    Vector3 dir;
    float rotSpeed;

	// Use this for initialization
	void Start () {
        int z = Random.Range(0, 2);
        if (z == 0)
            dir = -Vector3.forward;
        else
            dir = Vector3.forward;
        rotSpeed = Random.Range(10f, 100f);
    }
	
	// Update is called once per frame
	void Update () {
        
            transform.Rotate(dir * Time.deltaTime * rotSpeed);

    }
}
