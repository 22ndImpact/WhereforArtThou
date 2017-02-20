using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GameDirector : MonoBehaviour
{
    public List<GameObject> levelList;

    public int currentLevelDifficulty;

    public Vector2 levelBoundary;

    // Use this for initialization
    void Start ()
    {
        GenerateNewLevel(1);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    void UnloadCurrentLevel()
    {

    }

    public void GenerateNewLevel(int difficulty)
    {
        GameObject newLevel = Instantiate(levelList[Random.Range(0, levelList.Count)]);
        newLevel.GetComponent<Scr_Ctrl_Level>().Initialization(currentLevelDifficulty, levelBoundary);
    }
}
