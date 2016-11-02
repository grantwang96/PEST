using UnityEngine;
using System.Collections;

public class MaxMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newpos = new Vector3(Random.Range(380, 400), Random.Range(560, 580), transform.position.z);
        transform.position = newpos;
	}
}
