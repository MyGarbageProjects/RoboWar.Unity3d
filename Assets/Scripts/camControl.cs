using UnityEngine;
using System.Collections;

public class camControl : MonoBehaviour {

	void Start () {

	}


	float _z = 0.0f;
	float _x = 0.0f;
	[SerializeField] float sensitivity = 9.0f;
	[SerializeField] float speed = 5.0f;
	[SerializeField] bool ControlRotate = false;
	void Update () {
		float deltaX = Input.GetAxis("Vertical") * speed;
		float deltaZ = Input.GetAxis("Horizontal") * speed;
		Vector3 movement = new Vector3(deltaX, 0 , deltaZ*-1);

		movement *= Time.deltaTime;

		movement = transform.TransformDirection(movement);
		
		
		_z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
		_x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		transform.position += Vector3.forward*_z;
		transform.position += Vector3.right *_x;

		if(Input.GetKeyUp(KeyCode.L)) {
			ControlRotate = !ControlRotate;
		}


		ControlRotateMethod();
	}


	void ControlRotateMethod() {
		if(ControlRotate) {
			transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0);
			transform.eulerAngles += new Vector3(Input.GetAxis("Mouse Y") * sensitivity, 0, 0)*-1;
			
		}
	}

	void FixUpdate() {
		
		//Debug.Log(_z);
	}
}