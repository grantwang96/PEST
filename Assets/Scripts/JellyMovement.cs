using UnityEngine;
using System.Collections;

public class JellyMovement : MonoBehaviour {
    private float speed = 1.5f;
    private float moveDist = 1.5f;
    public Vector3 origin;
    public bool goingDown;
	// Use this for initialization
	void Start () {
        origin = transform.position;
        goingDown = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!goingDown)
        {
            if(transform.position.y<origin.y + moveDist)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else
            {
                goingDown = true;
            }
        }
        else
        {
            if (transform.position.y > origin.y - moveDist)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
            else
            {
                goingDown = false;
            }
        }
	}
}
