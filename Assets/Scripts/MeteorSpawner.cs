using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour {

    [SerializeField] List<GameObject> meteors;
    [SerializeField] float waitTime = 3f;

    public bool horizontalMeteors;

    public bool left;

    public float horSpeed = 1;
    float trueHorSpeed;

	// Use this for initialization
	void Start () {
        if(!horizontalMeteors)
        {
            StartCoroutine(SpawnMeteor());
        }
        else
        {
            StartCoroutine(HorizontalMeteors());
            if(left)
            {
                trueHorSpeed = -horSpeed;
            }
            else
            {
                trueHorSpeed = horSpeed;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnMeteor()
    {
        while(true)
        {
            int choose = Random.Range(0, meteors.Count);
            GameObject newMeteor = Instantiate(meteors[choose], transform.position, Quaternion.identity);
            float speed = Random.Range(1f, 4f);
            newMeteor.AddComponent<Rigidbody2D>();
            newMeteor.GetComponent<Rigidbody2D>().gravityScale = 0f;
            newMeteor.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, -speed);
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator HorizontalMeteors()
    {
        while(true)
        {
            int choose = Random.Range(0, meteors.Count);
            GameObject newMeteor = Instantiate(meteors[choose], transform.position, Quaternion.identity);
            newMeteor.AddComponent<Rigidbody2D>();
            newMeteor.GetComponent<Rigidbody2D>().gravityScale = 0f;
            newMeteor.GetComponent<Rigidbody2D>().velocity = new Vector2(trueHorSpeed, 0f);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
