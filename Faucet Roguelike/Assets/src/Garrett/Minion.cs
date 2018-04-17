using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy {

	// Use this for initialization
	void Start () {
		initiate ();
	}
	
	// Update is called once per frame
	void Update () {
		updateEnemy ();
	}
}
