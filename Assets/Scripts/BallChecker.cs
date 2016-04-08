using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BallChecker : MonoBehaviour {


	public Camera slowCam;

	[SerializeField]Camera mainCam; 
	RaycastHit hit;

	// DO shake Rotation:
	float duration = 2;
	float strength = 2;
	int vibrato = 3;
	float randomness = 30;


	// Use this for initialization
	void Start () {
		slowCam = GameObject.Find ("SlowCam").GetComponent<Camera> ();
		mainCam = GameObject.Find ("Main Camera").GetComponent<Camera> (); 
	}

	void OnEnable(){
		
	}

	// Update is called once per frame
	void Update () {
		
		if (Physics.Raycast(transform.position, transform.up, out hit, 100f)){
			if (hit.collider.tag == "Box" ){
				StartCoroutine (SlowMo (2));

			}
			Debug.DrawRay (transform.position, transform.forward * 1000, Color.red, 10f); 
		}

		}	

	void OnCollisionEnter (Collision coll){
		if (coll.gameObject.tag == "Box"){
			slowCam.DOShakeRotation (duration, strength, vibrato, randomness);

		}
	}

	IEnumerator SlowMo (float time){
		slowCam.depth = 0;
		Time.timeScale = 0.4f;
		yield return new WaitForSeconds (time);
		StartCoroutine (SetOriginalTimeCam (1));
	}

	IEnumerator SetOriginalTimeCam (float time){
		yield return new WaitForSeconds (time);
		slowCam.depth = -2;
		Time.timeScale = 1f;
		StartCoroutine (DestroyGO (this.gameObject));
	}

	IEnumerator DestroyGO(GameObject go){
		yield return new WaitForSeconds (3f);
		Destroy (go);
	}

}

