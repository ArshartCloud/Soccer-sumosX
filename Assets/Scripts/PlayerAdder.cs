using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerAdder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public GameObject player;
	private PlayerController playerController;
	private bool isAI = false;

	void Start ()
	{
//		setState (ButtonState.Add);
		playerController = player.GetComponent<PlayerController> ();
	}


	public void OnPointerDown (PointerEventData eventData)
	{
		if (player.activeSelf == true) {
			if (GameController.instence.gameState == GameState.OnPlay && playerController.canPlay == true) {
				playerController.setState (PlayerState.Charge);
			}
		}
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		if (player.activeSelf == true) {
			if (GameController.instence.gameState == GameState.OnPlay && playerController.canPlay == true) {
				playerController.setState (PlayerState.Rotate);
			}
		}
	}

	public void ButtonOnClick ()
	{
		if (GameController.instence.gameState == GameState.Prepare) {
			if (player.activeSelf == false) {
				player.SetActive (true);
			} else {
				player.SetActive (false);
				playerController.ResetPlayer ();
			}
		}
	}

	public void SwitchAI ()
	{
		if (isAI) {
			playerController.RemoveAI ();
			isAI = false;
			
		} else {
			playerController.AddAI ();
			isAI = true;
		}
	}
}
