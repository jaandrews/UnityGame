using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public float rotateSpeed = 5.0f;
	public float moveSpeed = .1f;
	public float speedModifier = .1f;
	private Vector2 lastMouseDown;
	private bool mouseActive;
	// Use this for initialization
	void Start () {
		lastMouseDown = Vector2.zero;
		mouseActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		Vector3 plus = Vector3.zero;
		Vector3 rot = this.transform.eulerAngles;
		Vector3 rotPlus = Vector3.zero;
		if(Input.GetKey("w")){
			 plus.z += speedModifier;
		}
		if(Input.GetKey("a")){
			 plus.x -= speedModifier;
		}
		if(Input.GetKey("s")){
			 plus.z -= speedModifier;
		}
		if(Input.GetKey("d")){
			 plus.x += speedModifier;
		}
		if(Input.GetMouseButtonDown(0)){
			float mouseX = Input.GetAxis("Mouse X");
			float mouseY = Input.GetAxis("Mouse Y");
			this.lastMouseDown = new Vector2(mouseX, mouseY);
			this.mouseActive = true;
			Debug.Log("dpwn" + this.lastMouseDown);
		}
		if(Input.GetMouseButtonUp(0)){
			this.mouseActive = false;
			Debug.Log("up");
		}
		if(Input.GetMouseButton(0)){
			if(this.mouseActive){
				float mouseX = Input.GetAxis("Mouse X");
				float mouseY = Input.GetAxis("Mouse Y");
				rotPlus.y +=  mouseX * rotateSpeed;
				rotPlus.x +=  mouseY * -rotateSpeed;
				Debug.Log(rotPlus);
			}
		}
		this.transform.Translate(plus, Space.Self);
		//this.transform.Rotate(rotPlus);
		//this.transform.position = ;
		this.transform.eulerAngles = rot + rotPlus;
	}
}

