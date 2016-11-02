using UnityEngine;
using System.Collections;

public class MerpSpawner : MonoBehaviour {
    public GameObject merple;
    private int timer;
    private int timer2;
    private bool facingRight;
	// Use this for initialization
	void Start () {
        timer = 0;
        facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
        timer2++;
        if (timer == 120)
        {
            GameObject merp = Instantiate(merple);
            merp.transform.position = new Vector3(transform.position.x + 1f * 
                transform.localScale.x, transform.position.y, transform.position.z);
            merp.GetComponent<BasicMovement>().cliffyes = false;
            if (!facingRight)
            {
                merp.GetComponent<BasicMovement>().Flip();
            }
            merp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(10, 30)*merp.transform.localScale.x, Random.Range(5, 15)),ForceMode2D.Impulse);
            timer = 0;
        }
        timer++;
        if (timer2 == 1200) { Destroy(this.gameObject); }
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
