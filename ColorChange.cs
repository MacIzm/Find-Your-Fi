using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {
    
    public Color color;
	// Use this for initialization
	void Start () {

        gameObject.GetComponent<Renderer>().material.color = Color.grey;
    }
	
	
}
