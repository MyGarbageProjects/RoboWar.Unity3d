using UnityEngine;
using System.Collections;

public class testEnemy : MonoBehaviour {

	public Transform _saw;
	public int _speed_rotation = 10;


	void FixedUpdate(){
		_saw.Rotate(0, Time.deltaTime * _speed_rotation, 0);
	}
}
