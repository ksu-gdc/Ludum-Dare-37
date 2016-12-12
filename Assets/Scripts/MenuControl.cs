using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

	// Use this for initialization
	public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
	}
	
}