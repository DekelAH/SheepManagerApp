using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour {

    public GameObject furr = null;
    public bool show = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (furr) {
            furr.transform.gameObject.GetComponent<Renderer>().enabled = show;
        }
		
	}
}
