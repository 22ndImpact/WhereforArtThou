using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_Button_Start : MonoBehaviour
{

    public string Scene;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void GoToScene()
    {
        SceneManager.LoadScene(Scene);
    }
}
