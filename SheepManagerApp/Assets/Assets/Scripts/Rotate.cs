using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public float speed = 1.0f;
    public float x_axis = 0;
    public float y_axis = 0;
    public float z_axis = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rotation = new Vector3(x_axis, y_axis, z_axis);
        transform.Rotate(Vector3.back * Time.deltaTime * speed);
    }
}
