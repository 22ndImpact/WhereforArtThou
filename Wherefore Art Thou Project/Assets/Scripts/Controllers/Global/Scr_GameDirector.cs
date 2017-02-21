using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_GameDirector : MonoBehaviour
{

    public static Scr_GameDirector inst;

    public Scr_Ctrl_Level currentLevel;

    public List<GameObject> levelList;

    public int currentLevelDifficulty;

    public Vector2 levelBoundary;

    Scr_Ctrl_Scene sceneController;

    void Awake()
    {
        inst = this;
    }

    // Use this for initialization
    void Start ()
    {
        GenerateNewLevel(1);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    public void CompleteLevel()
    {
        EndLevel();
        GenerateNewLevel(currentLevelDifficulty);
    }

    public void EndLevel()
    {
        UnloadCurrentLevel();

        currentLevelDifficulty++;
        
    }

    public void UnloadCurrentLevel()
    {
        Destroy(currentLevel.gameObject);
    }

    public void GenerateNewLevel(int difficulty)
    {
        GameObject newLevel = Instantiate(levelList[Random.Range(0, levelList.Count)]);
        newLevel.GetComponent<Scr_Ctrl_Level>().Initialization(currentLevelDifficulty, levelBoundary);
        
    }

    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
