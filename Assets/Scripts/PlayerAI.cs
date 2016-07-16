using UnityEngine;
using System.Collections;

public class PlayerAI : MonoBehaviour
{
	private GameObject ball;
	public float detectAngle;
	public bool work = false;
	private PlayerController playerController;
	// Use this for initialization
	void Start ()
	{
		ball = GameObject.FindGameObjectWithTag ("Finish");
		playerController = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (work) {
			if (Mathf.Abs (Vector3.Angle (transform.forward, ball.transform.position - transform.position)) < detectAngle) {
				if (playerController.playerState == PlayerState.Rotate)
					GetComponent<PlayerController> ().setState (PlayerState.Charge);
			} else if (Mathf.Abs (Vector3.Angle (transform.forward, ball.transform.position - transform.position)) > detectAngle * 3) {
				if (playerController.playerState == PlayerState.Charge)
					GetComponent<PlayerController> ().setState (PlayerState.Rotate);
			}
		}
	}
}
