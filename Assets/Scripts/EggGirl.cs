using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EggGirl : MonoBehaviour {
    public bool winState;
    private int timer;
    private float rotated;
    private bool leftTilt;
    private float increaser;
    private bool canJump;
    // Use this for initialization
    void Start () {
        canJump = true;
        timer = 0;
        increaser = 15f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.eulerAngles = new Vector3(0, 0, rotated);
        if (rotated >= 5 || rotated <= -5)
        {
            if (rotated >= 5) { rotated = 5; }
            else { rotated = -5; }
            increaser *= -1;
            leftTilt = !leftTilt;
        }
        rotated += increaser * Time.deltaTime;
        if (winState)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
            if (timer < 120) { timer+= 1; }
            else { SceneManager.LoadScene(4); }
        }
	}
}
