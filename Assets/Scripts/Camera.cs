using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    [SerializeField] GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        // Follow Player
        if(player != null)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);

        transform.GetChild(0).position = new Vector3(transform.position.x, transform.position.y, 0f);
	}
}
