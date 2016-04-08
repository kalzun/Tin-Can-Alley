using UnityEngine;
using System.Collections;

public class BoxesManager : MonoBehaviour {

	public int boxCounter = 0;

	// Use this for initialization
	void Start () {
		// Subscribe
		DelegatesAndEvents.onBoxFall += this.UpdateBoxCount;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateBoxCount (GameObject box){
		boxCounter++;

	}
}
