using UnityEngine;
using System.Collections;

public class Sandvich : MonoBehaviour {
    public Vector3 origin;
    private bool goingDown = false;
    private float speed = 1f;
	// Use this for initialization
	void Start () {
        origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!goingDown)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (transform.position.y >= origin.y + 0.5f)
            {
                goingDown = true;
            }
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            if (transform.position.y <= origin.y - 0.5f)
            {
                goingDown = false;
            }
        } 
	}
}
