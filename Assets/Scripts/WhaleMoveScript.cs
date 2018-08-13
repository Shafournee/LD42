using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMoveScript : MonoBehaviour {

    [SerializeField] float speed = .05f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate()
    {
        float step = Time.deltaTime * speed;

        transform.position = Vector3.MoveTowards(transform.position, transform.GetChild(0).position, step);
    }
}
