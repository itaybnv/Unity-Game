using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeScript : MonoBehaviour {
    //Variables
    Vector3 FreezePosition;



    //Methods
	// Use this for initialization
	void Start () {
        FreezePosition = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = FreezePosition;
	}
}
