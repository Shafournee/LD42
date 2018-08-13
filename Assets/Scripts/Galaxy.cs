using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour {

    [SerializeField] float rotSpeed = 1f;
    [SerializeField] int direct = 1;
    Vector3 dir;

	// Use this for initialization
	void Start () {
        if (direct == 0)
            dir = -Vector3.forward;
        else
            dir = Vector3.forward;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(dir * Time.deltaTime * rotSpeed);
    }
}
