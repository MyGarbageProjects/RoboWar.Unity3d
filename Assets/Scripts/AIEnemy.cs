using UnityEngine;
using System.Collections;

public class AIEnemy : MonoBehaviour {


	[SerializeField] GameObject aim_player;
	NavMeshAgent agent;

	public GameObject Aim_Player
	{
		set
		{
			aim_player = value;
		}
		get
		{
			return aim_player;
		}
	}

	void Start () {
		agent = GetComponent<NavMeshAgent> ();

		agent.SetDestination (Aim_Player.transform.position);
	}

	void Update () {
		//agent.SetDestination (Aim_Player.transform.position);
	}
}
