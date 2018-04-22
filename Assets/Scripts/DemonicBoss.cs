using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicBoss : MonoBehaviour {
    public float demonicSpeed = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, demonicSpeed);
	}
}
