using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Level : MonoBehaviour
{

    Vector2 levelBoundary;

    public int enemiesAlive;

    public List<Scr_Ctrl_Enemy> enemyList;

    int difficulty;

    int enemiesToSpawnBase = 5;
    int enemiesToSpawn;

    public GameObject enemyPrefab;

    public void Awake()
    {
        Scr_GameDirector.inst.currentLevel = this;
    }

    public void UpdateCUrrentEnemies(int _enemyChange)
    {
        enemiesAlive += _enemyChange;

        if(enemiesAlive <= 0)
        {
            Scr_GameDirector.inst.CompleteLevel();
        }
    }

    public void Initialization(int _difficulty, Vector2 _levelBoundary)
    {

        difficulty = _difficulty;
        enemiesToSpawn = enemiesToSpawnBase + difficulty;
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
                SpawnEnemy(attemptedSpawnLocation);
                enemyCounter += 1;
            }
        }

        
    }

    void SpawnEnemy(Vector3 spawnLocation)
    {
        spawnLocation.y = 0;

        GameObject newEnemy = (GameObject) Instantiate(enemyPrefab);
        
        if (Random.Range(0, 2) == 0)
        {
            enemyPrefab.GetComponent<Scr_Ctrl_Enemy>().Initialize(Scr_Ctrl_Player.Polarity.Blue);
        }
        else
        {
            enemyPrefab.GetComponent<Scr_Ctrl_Enemy>().Initialize(Scr_Ctrl_Player.Polarity.Pink);
        }

        enemiesAlive = enemyList.Count;



        newEnemy.transform.position = spawnLocation;
    }
}
