using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<ZG_AudioManager>().playDynamicSound("swordSwing");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
