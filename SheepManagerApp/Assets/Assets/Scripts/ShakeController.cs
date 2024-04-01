using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeController : MonoBehaviour {
    public float speed = 1.0f; //how fast it shakes
    public float dist = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

            //transform.position.x = 
            Vector3 temp = new Vector3(0, Mathf.Sin(Time.time * speed)*dist,0);
            transform.position += temp;
    }
}
