using UnityEngine;
using System.Collections;

public class AIEnemy : MonoBehaviour {


	[SerializeField] GameObject aim_player;
	NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update () {
		agent.SetDestination (aim_player.transform.position);
	}
}
