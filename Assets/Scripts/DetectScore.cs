using UnityEngine;
using System.Collections;

public class DetectScore : MonoBehaviour
{
	public Faction faction;

	void OnTriggerEnter (Collider collider)
	{
		GameObject obj = collider.gameObject;
//		if (obj.tag == "Player") {
//			Debug.Log ("Player OK");
//		}
		if (obj.tag == "Finish") {
			GameController.instence.Score (faction);
		}
	}
}
