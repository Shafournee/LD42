using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;

    // Access to the player
    [SerializeField] GameObject player;

    [SerializeField] GameObject restartText;

    [SerializeField] List<string> scenes;

    int currentScene = 0;

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
        restartText.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
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

    // We wait a frame here, because unity sucks and it takes a little while to spawn the player
    // so if we don't wait it's fucking null, and that's really dumb, so here's my stupid coroutine
    // instead that literally just waits one frame, and does nothing else.
    IEnumerator Wait()
    {
        yield return null;
        player = GameObject.FindGameObjectWithTag("Player");
        restartText = GameObject.FindGameObjectWithTag("RestartText");
        restartText.SetActive(false);
    }


    // This is how you load the next level
    public void LoadNextLevel()
    {
        if(currentScene < scenes.Count)
        {
            currentScene++;
            SceneManager.LoadScene(currentScene);
            Wait();
        }
        else
        {
            print("you fucked up dude");
        }
    }
}
