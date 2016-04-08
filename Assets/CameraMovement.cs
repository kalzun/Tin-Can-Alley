using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {

	float duration = 1;
	float strength = 0.1f;
	int vibrato = 1;
	float randomness = 10;

	Camera mainCam;

	// Use this for initialization
	void Start () {
		mainCam = GetComponent<Camera> ();	
	}
	
	// Update is called once per frame
	void Update () {
		mainCam.DOShakePosition (duration, strength, vibrato, randomness);
	}
}
