using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animTransfer : MonoBehaviour {

	public Animator animator;
	public PlayerInteract playerInteract;
	void Start () {
		playerInteract = FindObjectOfType<PlayerInteract>();
		playerInteract.notificationAnim = animator;
	}
}
