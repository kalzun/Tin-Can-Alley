using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


public class BallManager : MonoBehaviour {

	public static int maxBalls = 3;
	public static int thrownBalls = 0;

	public List<Image> ballImages;

	// Use this for initialization
	void Start () {
		// Subscribe
		DelegatesAndEvents.thrownBall += this.UpdateBallAmount;

		// Update infoPanel with amount of balls available:
		StartCoroutine (OnStartBallImages(0.5f));
	}
	

	void UpdateBallAmount(int balls){
		thrownBalls++;
		StartCoroutine (UpdateBallImages (thrownBalls));
		Debug.Log ("Thrown ball: " + thrownBalls + " of max " + maxBalls); 
	}

	IEnumerator OnStartBallImages(float time){
		
		for (int i = 0; i < ballImages.Count; i++){
			ballImages [i].DOColor (Color.yellow, 1f);
			yield return new WaitForSeconds (time);
		}
	}

	IEnumerator UpdateBallImages(int ballNumber){
		for (int i = 0; i < ballImages.Count; i++){
			int lastImage = ballImages.Count - ballNumber;
			ballImages [lastImage].DOColor (Color.white, 1f);
			yield return new WaitForSeconds (0.5f);
		}
	}

}
