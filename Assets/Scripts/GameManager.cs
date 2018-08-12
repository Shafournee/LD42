﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] List<Sprite> shipSprites;

    bool shipIsMoving;

    [SerializeField] GameObject ship;

    // Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;

    // Access to the player
    [SerializeField] GameObject player;

    [SerializeField] GameObject canvas;

    GameObject restartText;

    [SerializeField] List<string> scenes;

    int currentScene = 1;

    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
        // If instance already exists and it's not this:
        else if (instance != this)
        {
            // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(StartingCutscene());
    }
	
	// Update is called once per frame
	void Update () {

		if(player == null)
        {
            if(restartText != null)
                restartText.SetActive(true);

            // Handle restarting level here lmao
            if (Input.GetKey(KeyCode.R))
            {
                RestartLevel();
            }
        }
	}

    // This is the function for restarting the level
    void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
        StartCoroutine(Wait());

    }

    // This gets info from the player to tell the GM the next level was loaded
    // Because I'm an idiot and dont know how to do this correctly
    public void NewLevelLoaded()
    {
        StartCoroutine(Wait());
    }

    // We wait a frame here, because unity sucks and it takes a little while to spawn the player
    // so if we don't wait it's fucking null, and that's really dumb, so here's my stupid coroutine
    // instead that literally just waits one frame, and does nothing else.
    IEnumerator Wait()
    {
        yield return null;
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        restartText = canvas.transform.GetChild(0).gameObject;
        restartText.SetActive(false);
    }

    // This is how you load the next level
    public void LoadNextLevel()
    {
        if(currentScene < scenes.Count)
        {
            currentScene++;
            SceneManager.LoadScene(currentScene);
            StartCoroutine(StartingCutscene());
        }
        else
        {
            print("you fucked up dude");
        }
    }

    // This plays the cutscene for when a level starts for the first time
    IEnumerator StartingCutscene()
    {
        StartCoroutine(Wait());
        yield return null;
        Vector3 playerPos = player.transform.position;
        playerPos = new Vector2(playerPos.x, playerPos.y + .7f);
        ship.transform.position = new Vector2(playerPos.x - 13f, playerPos.y + 2f);
        player.SetActive(false);
        shipIsMoving = true;
        int z = 16;
        while (shipIsMoving)
        {
            // Set ship sprite
            ship.GetComponent<SpriteRenderer>().sprite = shipSprites[z];

            yield return null;

            z++;

            if(z > 19)
            {
                z = 16;
            }

            // Move ship to starting position
            float step = Time.deltaTime * 9f;
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, playerPos, step);
            yield return null;
            // Check if ship is in starting pos, if so break out of loop
            if(ship.transform.position == playerPos)
            {
                shipIsMoving = false;
            }
        }

        // Deploy landing gear
        for(int i = 15; i > -1; i--)
        {
            ship.GetComponent<SpriteRenderer>().sprite = shipSprites[i];
            yield return new WaitForSeconds(.1f);
        }
        yield return null;

        player.SetActive(true);
    }

    public void CallEndingCutscene(Vector2 goalPos)
    {
        StartCoroutine(EndingCutscene(goalPos));
    }

    public IEnumerator EndingCutscene(Vector2 goalPos)
    {
        goalPos = new Vector2(goalPos.x, goalPos.y);
        ship.transform.position = goalPos;
        player.SetActive(false);
        ship.SetActive(true);

        // Remove landing gear
        for (int i = 0; i < 15; i++)
        {
            ship.GetComponent<SpriteRenderer>().sprite = shipSprites[i];
            yield return new WaitForSeconds(.1f);
        }
        yield return null;


        Vector3 finalPos = new Vector3(goalPos.x + 13f, goalPos.y + 2f);

        shipIsMoving = true;
        int z = 16;
        while (shipIsMoving)
        {
            // Set ship sprite
            ship.GetComponent<SpriteRenderer>().sprite = shipSprites[z];

            yield return null;

            z++;

            if (z > 19)
            {
                z = 16;
            }

            // Move ship to starting position
            float step = Time.deltaTime * 9f;
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, finalPos, step);
            yield return null;
            // Check if ship is in starting pos, if so break out of loop
            if (ship.transform.position == finalPos)
            {
                shipIsMoving = false;
            }
        }
        LoadNextLevel();
    }
}
