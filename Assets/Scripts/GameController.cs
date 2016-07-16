using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Faction
{
	Red,
	Blue
}

public enum GameState
{
	Prepare,
	CountDown,
	OnPlay,
	Pause,
	Score,
	End
}

public class GameController : MonoBehaviour, TimeCounterFinish
{
	static public GameController instence;
	public GameState gameState;
	public GameObject[] players;
	public GameObject ball;
	public Transform ballPosition;
	public Dictionary<Faction, int> scores = new Dictionary<Faction, int> ();
	public Text scoreText;
	public GameObject startButton;
	public GameObject continueButton;
	public TimeCounter timeCounter;

	void Start ()
	{
		instence = this;
		GameStart ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void GameStart ()
	{
		scores [Faction.Red] = 0;
		scores [Faction.Blue] = 0;
		gameState = GameState.Prepare;
		startButton.SetActive (true);
		ResetPlayer ();
		ResetBall ();
		scoreText.text = "0 - 0";
	}

	public void StartPlay (GameObject button)
	{
		button.SetActive (false);
		gameState = GameState.CountDown;
		timeCounter.StartCountDown (3, this);
	}

	public void NewRound ()
	{
		ResetPlayer ();
		ResetBall ();
		gameState = GameState.CountDown;
		timeCounter.StartCountDown (3, this);
	}

	private void ResetPlayer ()
	{
		for (int i = 0; i < players.Length; ++i) {
			players [i].GetComponent<PlayerController> ().ResetPlayer ();
		}
	}

	public void ResetBall ()
	{
		ball.transform.position = ballPosition.position;
		ball.transform.rotation = ballPosition.rotation;
		ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		ball.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
	}

	public void Score (Faction fa)
	{
		scores [fa] += 1;
		scoreText.text = scores [Faction.Blue].ToString () + " - " + scores [Faction.Red].ToString ();
		NewRound ();
	}

	public void PauseGame ()
	{
		if (gameState == GameState.OnPlay) {
			continueButton.SetActive (true);
			gameState = GameState.Pause;
			Time.timeScale = 0;
		}
	}

	public void ContinueGame ()
	{
		if (gameState == GameState.Pause) {
			continueButton.SetActive (false);
			gameState = GameState.OnPlay;
			Time.timeScale = 1;
		}

	}

	public void OnCounterFinish ()
	{
		gameState = GameState.OnPlay;
	}

}
