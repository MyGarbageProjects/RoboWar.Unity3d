using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldControl : MonoBehaviour {
	[SerializeField] Image UIHealthBar;
	[SerializeField] Image UIAmmoBar;
	public static float sensitivity = 1.5f;
	public bool _pause = false;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		UIHealthBar.rectTransform.anchoredPosition = new Vector2 ((-Screen.width/2)+220, (-Screen.height/2)+50);//220&50 offset

		UIAmmoBar.rectTransform.anchoredPosition = new Vector2 ((Screen.width/2)-220, (-Screen.height/2)+50);//220&50 offset

		//sensitivity = float.Parse(System.IO.File.ReadAllText ("settings.txt"));

	}


	public bool PAUSE{
		set{ _pause = value; }
		get{return _pause; }
	}


}
