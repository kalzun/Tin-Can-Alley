using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class MouseLook : MonoBehaviour {

	public Camera cam;
	public GameObject prefabBall;
	public Slider powerSlider;
	public Image sliderFill;
	public float force;
	public float multiplier = 1f;
	public float minimumForce;
	public float maxForce = 15f;


	BallManager ballManager;
	float _greenArea = 10;
	float _redArea = 20;
	float _buttonPressTime;
	[SerializeField]float _addForce;
	Ray mouseRay;
	RaycastHit hit;



	// Use this for initialization
	void Start () {
		ballManager = GameObject.Find ("GameManager").GetComponent<BallManager> ();
		
	}

	public void OnGUI() {
		if (GUI.Button (new Rect (20, 20, 200, 30), "Restart")) {
			BallManager.thrownBalls = 0;
			Debug.Log ("ThrownBalls : " + BallManager.thrownBalls); 
			StartCoroutine(ballManager.OnStartBallImages (1f));

		}
	}
	
	// Update is called once per frame
	void Update () {
		mouseRay = cam.ScreenPointToRay (Input.mousePosition);

		if (Input.GetMouseButton (0)){
			_buttonPressTime = Time.time;
			_addForce += Time.time - _buttonPressTime * multiplier;

			if (_addForce > maxForce){
				_addForce = maxForce;
			}

			powerSlider.transform.parent.gameObject.SetActive (true);
			powerSlider.value = _addForce;

		}

		if (_addForce >= _greenArea && _addForce < _redArea){
			StartCoroutine(ColorSliderGreen ());
		}

		else if (_addForce >= _redArea){
			StartCoroutine(ColorSliderRed ());
		}

		if (Input.GetMouseButtonUp (0)){
			if (Physics.Raycast(mouseRay)){
				if (BallManager.thrownBalls < BallManager.maxBalls){
					// Debug.DrawRay (mouseRay.origin, mouseRay.direction, Color.red, 4f); 
					ThrowBall (BallManager.thrownBalls);
				}
			}	
				
		}
	}

	IEnumerator ColorSliderGreen (){
		sliderFill.DOColor (Color.green, 5f);
		yield return new WaitForSeconds (1f); 
	}

	IEnumerator ColorSliderRed (){
		sliderFill.DOColor (Color.red, 5f);
		yield return new WaitForSeconds (1f); 
	}

	void ThrowBall(int balls){

		force = _addForce;
		GameObject ballInstance = Instantiate (prefabBall, mouseRay.origin, prefabBall.transform.rotation) as GameObject;
		ballInstance.GetComponent<Rigidbody> ().AddForce (mouseRay.direction * force, ForceMode.Impulse);
		DelegatesAndEvents.BallThrown(balls);
		Reset ();
		powerSlider.transform.parent.gameObject.SetActive (false);
	}

	void Reset(){
		_addForce = 0;
		powerSlider.value = 0;
		sliderFill.color = Color.yellow;
	}
}
