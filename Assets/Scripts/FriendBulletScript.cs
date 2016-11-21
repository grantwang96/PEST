using UnityEngine;
using System.Collections;

public class FriendBulletScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * 8* Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Basic Enemy") || coll.gameObject.CompareTag("Shooty Enemy"))
        {
            if(coll.gameObject.CompareTag("Basic Enemy"))
            {
                coll.gameObject.GetComponent<BasicMovement>().death();
            }
            else
            {
                coll.gameObject.GetComponent<ShootyMovement>().death();
            }
            Destroy(this.gameObject);
        }
        else if (coll.gameObject.name != "Net" && coll.gameObject.name !="Player" && coll.gameObject.CompareTag("Basic Friend")
            && coll.gameObject.CompareTag("Jelly Friend") && coll.gameObject.CompareTag("Shooty Friend"))
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(this.gameObject);
    }
}
