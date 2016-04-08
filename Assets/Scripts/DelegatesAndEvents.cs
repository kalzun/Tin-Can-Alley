using UnityEngine;
using System.Collections;

public class DelegatesAndEvents : MonoBehaviour {

	public delegate void BallHandler (int balls);
	public delegate void BoxHandler (GameObject box);

	public static event BallHandler thrownBall;
	public static event BoxHandler onBoxFall;


	public static void BallThrown (int balls){
		if (thrownBall != null)
			thrownBall (balls);
	}

	public static void BoxesFall (GameObject box){
		if (onBoxFall != null)
			onBoxFall (box);
	}

}