using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

	public float _health = 100.0f;
	public Text _screenHealth;


	void Start(){
		_health /=100.0f;
	}

	void Update () {
        _screenHealth.text = (_health*100).ToString();
	}


	public void Hit(float damage){
		damage/=100.0f;
		if(_health >= damage)
			_health -=damage;
		else
			Die();
	}


	private void Die(){
		_health = 0;
	}
}
