using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public int speed;
	public Text instructionalText;
	private Vector3 movementVector;
	private Vector3 growthOnPickup;
	private Rigidbody player;
	private Vector3 startPosition;
	private int heightToTriggerRespawn;

	void Start () {
		instructionalText.text = "Get to the end";
		heightToTriggerRespawn = -30;
		startPosition = transform.position;
		player = GetComponent<Rigidbody>();
		growthOnPickup = new Vector3(0.5f,0.5f,0.5f);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.y < heightToTriggerRespawn ) {
			transform.position = startPosition;
			player.velocity = Vector3.zero;
			instructionalText.text = "";
		} else {
			movementVector = new Vector3(Input.GetAxis ("Horizontal"), 0.0f, Input.GetAxis ("Vertical"));
			player.AddForce(movementVector * speed);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "PickupGrow") {
			other.gameObject.active = false;
			transform.localScale += growthOnPickup;
			instructionalText.text = "";
		} else if (other.tag == "PickupShrink") {
			other.gameObject.active = false;
			transform.localScale -= growthOnPickup;
		} else if (other.tag == "PickupFinal") {
			other.gameObject.active = false;
			instructionalText.text = "You Win!!!";
		}
	}
}
