using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

	public Camera cam;
	public GameObject prefabBall;
	public float force;

	Ray mouseRay;
	RaycastHit hit;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		mouseRay = cam.ScreenPointToRay (Input.mousePosition);

		if (Input.GetMouseButtonDown (0)){
			if (Physics.Raycast(mouseRay)){
				if (BallManager.thrownBalls < BallManager.maxBalls){
					// Debug.DrawRay (mouseRay.origin, mouseRay.direction, Color.red, 4f); 
					ThrowBall (BallManager.thrownBalls);
				}
			}	
				
		}
	}

	void ThrowBall(int balls){
		GameObject ballInstance = Instantiate (prefabBall, mouseRay.origin, prefabBall.transform.rotation) as GameObject;
		ballInstance.GetComponent<Rigidbody> ().AddForce (mouseRay.direction * force, ForceMode.Impulse);
		DelegatesAndEvents.BallThrown(balls);
	}
}
