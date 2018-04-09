using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_GenObstacle : MonoBehaviour
{

	public List<GameObject> goodInteractables;
	public List<GameObject> badInteractables;
	public List<GameObject> rocks;
	public GameObject Exit;

	public GameObject generateInteractable(int weight) {
		// Weight is an integer between 0 and 100, where 100 = always good and 0 = always bad
		int RandNum = Random.Range(0, 101);
		if(RandNum < weight) {
			// return random good item
			return goodInteractables[Random.Range(0, goodInteractables.Count)];
		}
		else {
			//return random bad item
			return badInteractables[Random.Range(0, badInteractables.Count)];
		}
	}
	public GameObject generateRock() {
		return rocks[Random.Range(0,rocks.Count)];
	}
	public GameObject generateExit() {
		return Exit;
	}
}