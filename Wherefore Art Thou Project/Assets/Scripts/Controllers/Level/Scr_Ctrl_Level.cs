using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Level : MonoBehaviour
{

    Vector2 levelBoundary;

    int difficulty;

    int enemiesToSpawn = 10;

    public GameObject enemyPrefab;



	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Initialization(int _difficulty, Vector2 _levelBoundary)
    {
        difficulty = _difficulty;
        levelBoundary = _levelBoundary;
        GenerateEnemies();
    }

    void GenerateEnemies()
    {
        int enemyCounter = 0;

        while (enemyCounter < enemiesToSpawn)
        {
            Vector3 attemptedSpawnLocation;
            //Sets the raycast origin above the level
            attemptedSpawnLocation.y = -10;

            attemptedSpawnLocation.x = Random.Range(-levelBoundary.x, levelBoundary.x);
            attemptedSpawnLocation.z = Random.Range(-levelBoundary.y, levelBoundary.y);

            Ray ray = new Ray(attemptedSpawnLocation, new Vector3(0, -1, 0));

            RaycastHit hitInfo;

            if (!Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log(attemptedSpawnLocation);
                SpawnEnemy(attemptedSpawnLocation);
                enemyCounter += 1;
            }
        }

        
    }

    void SpawnEnemy(Vector3 spawnLocation)
    {
        spawnLocation.y = 0;

        if (Random.Range(0, 2) == 0)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.position = spawnLocation;
            enemyPrefab.GetComponent<Scr_Ctrl_Enemy>().Initialize(Scr_Ctrl_Player.Polarity.Blue);
        }
        else
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.position = spawnLocation;
            enemyPrefab.GetComponent<Scr_Ctrl_Enemy>().Initialize(Scr_Ctrl_Player.Polarity.Pink);
        }


    }
}
