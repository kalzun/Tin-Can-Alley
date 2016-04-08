using UnityEngine;
using System.Collections;

public class Boxes : MonoBehaviour {


	ParticleSystem psImpact;

	// Use this for initialization
	void Start () {
		psImpact = this.transform.FindChild ("PSImpact").GetComponent<ParticleSystem> ();;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision coll){
		if (coll.gameObject.tag == "Scoring"){
			DelegatesAndEvents.BoxesFall (this.gameObject);

		}
	}
}
