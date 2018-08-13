using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    [SerializeField] GameObject player;
    GameObject LevelManager;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        LevelManager = GameObject.FindGameObjectWithTag("LevelManager");
	}
	
	// Update is called once per frame
	void Update () {
        // Follow Player
        if(player != null)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);

        if(LevelManager.GetComponent<LevelManager>().isSpaceLevel)
        {
            transform.GetChild(0).position = new Vector3(transform.position.x + 3f, transform.position.y + 1.7f, 0f);
        }
        else
        {

        }
    }
}
