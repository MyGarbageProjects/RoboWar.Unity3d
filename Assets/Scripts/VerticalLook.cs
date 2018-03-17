using UnityEngine;
using System.Collections;

public class VerticalLook : MonoBehaviour {

	[SerializeField] GameObject Left_Arm;
	[SerializeField] GameObject Right_Arm;
	[SerializeField] GameObject _Head;
	[SerializeField] GameObject _Spine;
	//public float sensitivityVert = 9.0f;
	public float minimumVert = -45.0f;
	public float maxumumVert = 45.0f;
	private float angleX = 0.0f;

	void LateUpdate () {
		angleX -= Input.GetAxis("Mouse Y") * WorldControl.sensitivity;
		angleX = Mathf.Clamp(angleX, minimumVert, maxumumVert);
		//float rotationY = transform.localEulerAngles.y;
		//Quaternion Vector = Quaternion.AngleAxis (angleX,new Vector3 (1, 0, 0));

		if(_Spine!= null)
			_Spine.transform.rotation = Quaternion.Euler (angleX, _Spine.transform.rotation.eulerAngles.y, _Spine.transform.rotation.eulerAngles.z);
		
		if (Right_Arm != null)
			Right_Arm.transform.rotation = Quaternion.Euler (angleX, Right_Arm.transform.rotation.eulerAngles.y, Right_Arm.transform.rotation.eulerAngles.z);

		if (Left_Arm != null)
		//Left_Arm.transform.rotation = Quaternion.Euler(Left_Arm.transform.rotation.eulerAngles.x,Left_Arm.transform.rotation.eulerAngles.y,angleX);
		//Left_Arm.transform.rotation = Quaternion.Angle (Left_Arm.transform.rotation, Quaternion.u);
			Left_Arm.transform.rotation = Quaternion.Euler(angleX,Left_Arm.transform.rotation.eulerAngles.y,Left_Arm.transform.rotation.eulerAngles.z);

		//arm.transform.Rotate (new Vector3(1,0,0),_rotationX);
		//arm.transform.rotation = Vector;
		//arm.transform.rotation = Quaternion.AngleAxis(_rotationX,Vector3.right) * arm.transform.rotation;
		//arm.transform.Rotate (new Vector3 (_rotationX, rotationY, arm.transform.eulerAngles.z));
		//transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

		//_rotationX -= Input.GetAxis("Mouse Y") * WorldControl.sensitivity;
		//_rotationX = Mathf.Clamp(_rotationX, minimumVert, maxumumVert);
		//float rotationY = transform.localEulerAngles.y;
		//transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

		if(_Head != null)
			_Head.transform.rotation = Quaternion.Euler (angleX, _Head.transform.rotation.eulerAngles.y, _Head.transform.rotation.eulerAngles.z);
	}
}
