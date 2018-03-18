using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldControl : MonoBehaviour {
	[SerializeField] Image UIHealthBar;
	[SerializeField] Image UIAmmoBar;
	public static float sensitivity = 1.5f;
	public bool _pause = false;
	[SerializeField] GameObject _enemyPrefab;
	private GameObject Player;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		UIHealthBar.rectTransform.anchoredPosition = new Vector2 ((-Screen.width/2)+220, (-Screen.height/2)+50);//220&50 offset

		UIAmmoBar.rectTransform.anchoredPosition = new Vector2 ((Screen.width/2)-220, (-Screen.height/2)+50);//220&50 offset
		Player = GameObject.FindGameObjectWithTag("Player");

		//sensitivity = float.Parse(System.IO.File.ReadAllText ("settings.txt"));
		for (int x = 0; x <= 15; x++) {InstantiateEnemy (Player, new Vector3 (45, 0, 45-x));}
		//InstantiateEnemy (Player, new Vector3 (45, 0, 45));
	}

	public void InstantiateEnemy(GameObject Aim, Vector3 position)
	{
		Instantiate(_enemyPrefab, position, new Quaternion (0, 0, 0, 0));

		GameObject[] obj = GameObject.FindGameObjectsWithTag ("Enemy");

		AIEnemy aiE = obj[obj.Length-1].GetComponent<AIEnemy> ();
		aiE.Aim_Player = Player;
		aiE.enabled = true;
	}

	public bool PAUSE{
		set{ _pause = value; }
		get{return _pause; }
	}


}
