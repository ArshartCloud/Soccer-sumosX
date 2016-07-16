using UnityEngine;
using System.Collections;

public enum PlayerState
{
	Rotate,
	Charge
}

public class PlayerController : MonoBehaviour
{
	public GameObject clockwork;
	public float autoRotateSpeed = 1f;
	public float chargeSpeed = 1f;
	public bool originalRotateClockwise;
	private bool rotateClockwise;
	public bool canPlay = true;
	public PlayerState playerState = PlayerState.Rotate;
	public Transform originalPosition;

	private Rigidbody _rb = null;

	private Rigidbody rb {
		get {
			if (_rb != null) {
				return _rb;
			}
			_rb = GetComponent<Rigidbody> ();
			return _rb;
		}
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (GameController.instence.gameState == GameState.OnPlay) {
			if (playerState == PlayerState.Rotate) {
				AutoRotate ();
			} else if (playerState == PlayerState.Charge) {
				ChargeForward ();
			}
		} else {
		}


	}

	void AutoRotate ()
	{
		if (rotateClockwise) {
			rb.angularVelocity = transform.up * autoRotateSpeed;
//			transform.RotateAround (transform.position, transform.up, autoRotateSpeed);
		} else {
			rb.angularVelocity = transform.up * autoRotateSpeed * -1;
//			transform.RotateAround (transform.position, transform.up * -1, autoRotateSpeed);
		}
	}

	public void ChargeForward ()
	{
		rb.velocity = transform.forward * chargeSpeed;
//		transform.Translate (transform.forward * chargeSpeed);
	}

	public void setState (PlayerState ps)
	{
		if (ps == PlayerState.Charge) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		} else if (ps == PlayerState.Rotate) {
			rb.velocity = Vector3.zero;
			rotateClockwise = !rotateClockwise;
		}
		playerState = ps;
	}


	public void AddAI ()
	{
		gameObject.GetComponent<PlayerAI> ().work = true;
		clockwork.SetActive (true);
		canPlay = false;
	}

	public void RemoveAI ()
	{
		gameObject.GetComponent<PlayerAI> ().work = false;
		setState (PlayerState.Rotate);
		clockwork.SetActive (false);
		canPlay = true;
	}

	public void ResetPlayer ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rotateClockwise = true;
		playerState = PlayerState.Rotate;
		transform.position = originalPosition.position;
		transform.rotation = originalPosition.rotation;
		rotateClockwise = originalRotateClockwise;
	}
}
