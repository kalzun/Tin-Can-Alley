using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class BallChecker : MonoBehaviour {


	public Camera slowCam;
	public GameObject hitPicPrefab;
	public GameObject camPrefab;
	public Transform playerTransform;
	public BallManager ballManager;


	[SerializeField]Camera mainCam; 
	RaycastHit hit;
	bool _shouldRaycast = true; 
	float offsetCamPrefab = 10f;
	float offsetImageInst = -0.5f;

	// DO shake Rotation:
	float duration = 2;
	float strength = 2;
	int vibrato = 3;
	float randomness = 30;


	// Use this for initialization
	void Start () {
		slowCam = GameObject.Find ("SlowCam").GetComponent<Camera> ();
		mainCam = GameObject.Find ("Main Camera").GetComponent<Camera> (); 
		playerTransform = GameObject.Find ("Player").GetComponent<Transform> (); 
		ballManager = GameObject.Find ("GameManager").GetComponent<BallManager> ();
	}

	void OnEnable(){
		_shouldRaycast = true;

	}

	// Update is called once per frame
	void Update () {
		
		if (Physics.Raycast(transform.position, transform.up, out hit, 100f)){
			if (hit.collider.tag == "Box"  && !_shouldRaycast){
				StartCoroutine (SlowMo (2));

			}
			// Debug.DrawRay (transform.position, transform.forward * 1000, Color.red, 10f); 
		}

		if (Physics.Raycast(transform.position, transform.up, out hit, 1f)){
			if (hit.collider.tag == "Destructable" && _shouldRaycast){
				Debug.Log ("HitBackground, distance: " + hit.distance);
				Debug.DrawRay (transform.position, transform.forward * 1000, Color.red, 10f); 
				_shouldRaycast = false;
			}
			// Debug.DrawRay (transform.position, transform.forward * 1000, Color.red, 10f); 
		}

		}	

	void OnCollisionEnter (Collision coll){
		if (coll.gameObject.tag == "Box"){
			slowCam.DOShakeRotation (duration, strength, vibrato, randomness);
		}

		else if(coll.gameObject.tag == "Scoring" || coll.gameObject.tag == "Ground" ){
			_shouldRaycast = false;
		}
		else if(coll.gameObject.tag == "Destructable"){
			
			ContactPoint contact = coll.contacts[0];
			// ContactPoint contact = coll.contacts;

			GameObject imageInst = Instantiate (hitPicPrefab, new Vector3 (contact.point.x, contact.point.y, contact.point.z), coll.transform.rotation) as GameObject;


			GameObject camInst = Instantiate (camPrefab, new Vector3 (imageInst.transform.position.x, imageInst.transform.position.y, imageInst.transform.position.z), transform.rotation) as GameObject;
			// hitPicPrefab.transform.parent.transform.parent.gameObject.SetActive (true);
			// hitPicPrefab.transform.parent.transform.parent.gameObject.transform.SetParent (coll.gameObject.transform);

			imageInst.SetActive (true);
			camInst.SetActive (true);
			camInst.transform.SetParent (imageInst.gameObject.transform);
			camInst.transform.rotation = Quaternion.LookRotation (transform.position - mainCam.transform.position);							// LookAt (2 * mainCam.transform); // Quaternion.Inverse (transform.rotation) * mainCam.transform.rotation;
			imageInst.transform.SetParent (coll.gameObject.transform);
			if (imageInst.transform.localPosition.x != offsetImageInst){
				Debug.Log ("Setting X to : " + offsetImageInst + " on localPosition: " + imageInst.transform.localPosition.x); 
				imageInst.transform.localPosition = new Vector3 (offsetImageInst, imageInst.transform.localPosition.y, imageInst.transform.localPosition.z);
			}

			SpawnHitDecal (imageInst, camInst);


			Debug.Log (contact.point );
			_shouldRaycast = false;
			Destroy (this.gameObject);
		}
	}

	void SpawnHitDecal(GameObject imageInst, GameObject camInst){
		for (int i = 0; i < BallManager.thrownBalls; i++){
			BallManager.activeRenderTextures++;
			imageInst.transform.GetComponentInChildren<RawImage> ().texture = ballManager.renderTextures [i];
			camInst.GetComponent<Camera> ().targetTexture = ballManager.renderTextures [i] as RenderTexture;
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

