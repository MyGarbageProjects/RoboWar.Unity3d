using UnityEngine;
using System.Collections;

public class HorizontalLook : MonoBehaviour {

	//[SerializeField] float sensitivity = 9.0f;
	void FixedUpdate (){
		transform.Rotate(0, Input.GetAxis("Mouse X") * WorldControl.sensitivity, 0);
	}
}
