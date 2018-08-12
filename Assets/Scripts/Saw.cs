using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {

    [SerializeField] Transform startPos;

    [SerializeField] Transform endPos;

    Transform currentDir;

    [SerializeField] float speed = 1f;


	// Use this for initialization
	void Start () {
        currentDir = endPos;
    }
	
	// Update is called once per frame
	void Update () {
        MoveToPoint();

        transform.Rotate(-Vector3.forward * Time.deltaTime * 2000f);

    }

    void MoveToPoint()
    {
        float step = Time.deltaTime * speed;

        transform.position = Vector3.MoveTowards(transform.position, currentDir.position, step);

        if (transform.position == endPos.position)
        {
            currentDir = startPos;
        }
        else if (transform.position == startPos.position)
        {
            currentDir = endPos;
        }
    }
}
