using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {

    bool suckingPlayer;

    GameObject Player;

    [SerializeField] float ForceMultiplier = 40f;
    [SerializeField] float ForceDivisor = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Suck();

        transform.Rotate(Vector3.forward * Time.deltaTime * 15f);
    }

    void Suck()
    {
        // If the player is in the succ range, pull them towards the hole
        if(suckingPlayer && Player != null)
        {
            Vector2 dir = (transform.position - Player.transform.position);

            Player.GetComponent<Rigidbody2D>().AddForce((dir * ForceMultiplier)/(dir.magnitude * ForceDivisor));
            print(dir.magnitude);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            suckingPlayer = true;
            Player = collider.gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            suckingPlayer = false;
            Player = null;
        }
    }
}
