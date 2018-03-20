using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_SaveLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SaveLoad testSaveLoad = FindObjectOfType<SaveLoad>();
        testSaveLoad.Save("testfile");
        testSaveLoad.Load("testfile");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
