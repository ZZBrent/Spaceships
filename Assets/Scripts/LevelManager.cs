using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

    public static void LoadNextLevel()
    {
        foreach (GameObject g in GameObject.Find("Player").GetComponent<PlayerController>().lifeList)
            Destroy(g);
        Debug.Log("Next Level load");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
