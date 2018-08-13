using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour {

    [SerializeField] List<GameObject> meteors;
    [SerializeField] float waitTime = 3f;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnMeteor());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnMeteor()
    {
        while(true)
        {
            print("itworks");
            int choose = Random.Range(0, meteors.Count);
            GameObject newMeteor = Instantiate(meteors[choose], transform.position, Quaternion.identity);
            float speed = Random.Range(1f, 4f);
            newMeteor.AddComponent<Rigidbody2D>();
            newMeteor.GetComponent<Rigidbody2D>().gravityScale = 0f;
            newMeteor.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, -speed);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
