using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScript : MonoBehaviour {
    GameObject pauseMenu;
    bool paused;
	// Use this for initialization
	void Start () {
        paused = false;
        pauseMenu = GameObject.Find("pauseMenu");

	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
        }
        if (paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (!paused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
	}
    public void Resume()
    {
        paused = false;
    }
}
