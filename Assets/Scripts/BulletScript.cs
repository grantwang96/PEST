using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right* 8 * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name != "Net" || coll.gameObject.CompareTag("Basic Enemy") ||
            coll.gameObject.CompareTag("Basic Friend") || coll.gameObject.CompareTag("Shooty Enemy")||
            coll.gameObject.CompareTag("Jelly Enemy") || coll.gameObject.CompareTag("Jelly Friend"))
        {
            if (coll.gameObject.CompareTag("Basic Friend"))
            {
                coll.gameObject.GetComponent<BasicMovement>().health--;
            }
            Destroy(this.gameObject);
        }
    }
}
