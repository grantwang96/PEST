using UnityEngine;
using System.Collections;

public class FeetScript : MonoBehaviour {

    public bool canJump;
    public bool playingParticles;

	// Use this for initialization
	void Start () {
        canJump = true;
        playingParticles = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Floor" || coll.gameObject.tag=="Jelly Head")
        {
            canJump = true;
            Debug.Log("Entered Ground!");
            if (coll.gameObject.tag=="Jelly Head")
            {
                if (!GameObject.Find("LandJelly").GetComponent<AudioSource>().isPlaying)
                { GameObject.Find("LandJelly").GetComponent<AudioSource>().Play(); }
            }
            else
            {
                if (!GameObject.Find("Land").GetComponent<AudioSource>().isPlaying)
                { GameObject.Find("Land").GetComponent<AudioSource>().Play(); }
            }
            GetComponentInParent<Player_Movement>().isInjured = false;
            GameObject.Find("Body").GetComponent<Animator>().SetBool("Injured", false);
            if (GetComponentInParent<Player_Movement>().jumped)
            {
                GameObject.Find("Body").GetComponent<Animator>().Play("Land");
                GetComponentInParent<Player_Movement>().jumped = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Floor")|| coll.gameObject.CompareTag("Jelly Head"))
        {
            canJump = true;
            Debug.Log("Landed!");
        }
        if (coll.gameObject.transform.parent != null)
        {
            if (coll.gameObject.transform.parent.name == "Platforms") {
                if (Input.GetKey(GameObject.Find("Player").GetComponent<Player_Movement>().rightKey) ||
                    Input.GetKey(GameObject.Find("Player").GetComponent<Player_Movement>().leftKey))
                {
                    if (!playingParticles)
                    {
                        playingParticles = true;
                        GameObject.Find("GroundWalk").GetComponent<ParticleSystem>().Play();
                        Debug.Log("Playing Particles!");
                    }
                }
                if (Input.GetKeyUp(GameObject.Find("Player").GetComponent<Player_Movement>().rightKey) ||
                    Input.GetKeyUp(GameObject.Find("Player").GetComponent<Player_Movement>().leftKey))
                {
                    playingParticles = false;
                    GameObject.Find("GroundWalk").GetComponent<ParticleSystem>().Stop();
                    Debug.Log("Stopping Particles!");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Jelly Head") { canJump = false; }
        Debug.Log("Left the Ground!");
        if (coll.gameObject.transform.parent != null)
        {
            if (coll.gameObject.transform.parent.name == "Platforms")
            {
                GameObject.Find("GroundWalk").GetComponent<ParticleSystem>().Stop();
                playingParticles = false;
            }
        }
    }
}
