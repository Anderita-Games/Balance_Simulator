using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameMaster The_Canvas;


	// Use this for initialization
	void Start () {
		The_Canvas = GameObject.Find("Canvas").GetComponent<GameMaster>();
	}
	
	// Update is called once per frame
	void Update () {

		if (this.transform.position.y <= -7.5) {
			Destroy(this.gameObject);
			The_Canvas.Game_State = "End";
		}

		this.transform.Rotate(new Vector3(0, 0, this.transform.localEulerAngles.z + Input.acceleration.x));

		if (Input.GetKey("right")) {
			this.transform.Rotate(new Vector3(0, 0, this.transform.localEulerAngles.z - .0000001f));
		}
		if (Input.GetKey("left")) {
			this.transform.Rotate(new Vector3(0, 0, this.transform.localEulerAngles.z + .0000001f));
		}
	}
}
