using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmptyWeapon : MonoBehaviour {

    public Text _screenAmmo;
    public Text _screenTAmmo;

	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_screenAmmo.text = "∞/";
		_screenTAmmo.text = "∞";
	}
}
