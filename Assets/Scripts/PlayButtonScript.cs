using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void main()
    {
        SceneManager.LoadScene(0);
    }
    public void play()
    {
        SceneManager.LoadScene(1);
    }
    public void instructions()
    {
        SceneManager.LoadScene(2);
    }
    public void instructions2()
    {
        SceneManager.LoadScene(3);
    }
    public void quitbutton()
    {
        Application.Quit();
    }
}
