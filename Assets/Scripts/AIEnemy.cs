using UnityEngine;
using System.Collections;

public class AIEnemy : MonoBehaviour {


	[SerializeField] GameObject aim_player;
	NavMeshAgent agent;
	Animator _animator;

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

		_animator = GetComponent<Animator> ();
		//agent.SetDestination (Aim_Player.transform.position);
	}

	bool animationAttack=false;
	void FixedUpdate () {
		agent.SetDestination (Aim_Player.transform.position);
		Vector3 dist = this.transform.position - Aim_Player.transform.position;
		if (dist.x < 10)
			animationAttack = true;
		else 
			animationAttack = false;
		
		if (animationAttack)
			_animator.SetBool ("Attack",true);
		else
			_animator.SetBool ("Attack",false);
		
	}
}
