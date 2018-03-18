using UnityEngine;
using System.Collections;

public class AIhealth : MonoBehaviour {


	[SerializeField]private float _health = 100.0f;


	void Start(){
		_health /=100.0f;
	}

	public void Hit(float damage){
		damage/=100.0f;
		if(_health >= damage)
			_health -=damage;
		else
			StartCoroutine(Die());
		//Debug.Log(_health);
	}

	public float Health
	{
		get{return _health*100.0f;}
	}


	IEnumerator Die(){
		_health = 0;
		this.transform.Rotate (new Vector3(-90,0,0));
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = false;
		NavMeshAgent nma = GetComponent<NavMeshAgent> ();
		if(nma != null)
			nma.enabled = false;
		AIEnemy ai = GetComponent<AIEnemy> ();
		if (ai != null)
			ai.enabled = false;
		Animator anim = GetComponent<Animator> ();
		if (anim != null)
			anim.enabled = false;
		
		//rb.isKinematic = true;

		yield return new WaitForSeconds(5.5f);
		Destroy(this.gameObject);
		WorldControl.countEnemyNow--;
	}
}
