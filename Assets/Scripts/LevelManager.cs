using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {


    public int totalEnemies;
    public int spawnedEnemies;
    public int currentEnemies;
    public float spawnTime = 3f;

    public GameObject enemy;
    public Transform[] spawnPoints;

    public GameObject player1;
    public GameObject player2;

    public bool playersAlive;
	public bool isPaused = false;

	// Use this for initialization
	void Start () {
        totalEnemies = 100;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        playersAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (spawnedEnemies >= totalEnemies)
        {
            Debug.Log("Level Finished!");
        }

        if (player1.GetComponent<Movement>().health <= 0 || player2.GetComponent<Movement>().health <= 0)
        {
            playersAlive = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


		if (Input.GetButtonDown ("Pause")) {
			switch (isPaused) {
			case true:
				UnpauseGame ();
				break;

			case false:
				PauseGame ();
				break;
			}
		}
	}

    void Spawn()
    {
        if (player1.GetComponent<Movement>().health > 0 && player2.GetComponent<Movement>().health > 0)
        {
            if (currentEnemies <= 25)
            {
                int chosenSpawnPoint = Random.Range(0, spawnPoints.Length);

                Instantiate(enemy, spawnPoints[chosenSpawnPoint].position, spawnPoints[chosenSpawnPoint].rotation);
                spawnedEnemies++;
                currentEnemies++;
            }
        }
        else
        {
            return;
        }
    }

    public void OnEnemyDeath()
    {
        currentEnemies--;
        player1.GetComponent<Movement>().AddXP(50);
        player2.GetComponent<Movement>().AddXP(50);
    }

	public void PauseGame()
	{
		Time.timeScale = 0;
		isPaused = true;
	}

	public void UnpauseGame()
	{
		Time.timeScale = 1;
		isPaused = false;
	}
}
