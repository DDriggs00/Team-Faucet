using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//MINION - subclass of Enemy that can be modified without affecting the Enemy superclass; currently there are no modifications other than graphics
public class Minion : Enemy {
	

	void Start () {
		initiate ();  //utilize the same initiation function as Enemy
	}
	
	// Update is called once per frame
	void Update () {
		updateEnemy ();  //utilize the same update function as Enemy
	}
}	
