using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public interface TimeCounterFinish
{
	void OnCounterFinish ();
}

public class TimeCounter : MonoBehaviour
{
	private bool startCount;
	private float targetTime;
	private TimeCounterFinish monitor;
	private float countDownTime;
	public Text countDownText;


	// Use this for initialization
	void Start ()
	{
	
	}

	public void StartCountDown (float time, TimeCounterFinish moni)
	{
		monitor = moni;
		targetTime = Time.time + time + 0.9f;
		countDownTime = time;
		startCount = true;
		countDownText.gameObject.SetActive (true);
	}

	// Update is called once per frame
	void Update ()
	{
		if (startCount) {
			if (Time.time >= targetTime) {
				startCount = false;
				countDownText.gameObject.SetActive (false);
				monitor.OnCounterFinish ();
			} else {
				int num = (int)(targetTime - Time.time);
				countDownText.text = num.ToString ();
			}
		}

	}
}
