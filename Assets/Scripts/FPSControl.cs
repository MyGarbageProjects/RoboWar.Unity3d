using UnityEngine;
using System.Collections;

public class FPSControl : MonoBehaviour {

	[SerializeField] private WorldControl _wordlControl;

	[SerializeField] private GameObject _MainWeapon;
	[SerializeField] private GameObject _SecondaryWeapon;
	[SerializeField] private GameObject _DefaultWeapon;
	[SerializeField] private Transform _Hand;
	[SerializeField] private float _walk = 5.0f;
	[SerializeField] private float _run = 12.0f;
	//[SerializeField] private float _gravity = -9.0f;
	[SerializeField] private float _JumpForce = 200.0f;
	[SerializeField] private bool _running = false;
	private bool _switchWeapon = false;
	private EnumWeapon _enumWeapon;

	private bool _vertical = false;
	private bool _jump = false;
	public bool _canjump = false;
	private float _deltaX;
	private float _deltaZ;
	private Vector3 _movement;

	Rigidbody _rb;
	Animator _animator;

	//private bool _pause = false;
	void Start () {
		_rb = GetComponent<Rigidbody> ();
		_animator = GetComponent<Animator>();
	}

	void Update () {
		//correction of movement speed
		float _speed = 0.0f;
		_vertical = Input.GetButton("Vertical");
		_running = Input.GetButton ("Run");
		_speed = !_running ? _walk : _run;
		//rule movement
		_deltaZ = Input.GetAxis ("Vertical")*_speed;
		_deltaX = Input.GetAxis ("Horizontal")*_speed;
		_movement = new Vector3(_deltaX, 0, _deltaZ);
		_movement = Vector3.ClampMagnitude(_movement, _speed);
		_movement = transform.TransformDirection(_movement);

		//Animation.position arms
		if(Input.GetButtonDown("MainPositionArms"))
		{_enumWeapon = EnumWeapon.Main;_switchWeapon = true;}
		else if(Input.GetButtonDown("SecondaryPositionArms"))
		{_enumWeapon = EnumWeapon.Secondary;_switchWeapon = true;}
		else if(Input.GetButtonDown("DefaultPositionArms"))
		{_enumWeapon = EnumWeapon.Default;_switchWeapon = true;}

		//Act
		_jump = Input.GetButtonDown("Jump");

	}

	/*bool DefaultArms = true;
	bool AttackArms = false;
	bool SecondaryAttackArms = false;*/

	void FixedUpdate() {
		//Анимация и ходъбы
		if (_vertical) {
			float setSpeedPercent = _running == true ? 1.0f : 0.5f;
			_animator.SetFloat ("Speed", setSpeedPercent);
		}
		else
			_animator.SetFloat ("Speed", 0.0f);


		//if(_change)
			//switch_weapon(_enumWeapon);
		if(_switchWeapon & !_wordlControl.PAUSE){
			_wordlControl.PAUSE = true;
			_switchWeapon = false;
			StartCoroutine (switch_weapon (_enumWeapon));
		}


		if(_jump & _canjump) {
			_canjump = false;
			_rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
			_rb.AddForce(0, _JumpForce, 0);

		}
			
		/*_rb.velocity = _movement;
		_rb.AddForce (transform.up * _jumpHeight, ForceMode.Impulse);
		Передвижение----------------------------------------------------------------------------------
		float deltaX = Input.GetAxis("Horizontal")* _speed;
		float deltaZ = Input.GetAxis("Vertical") * _speed;
		_movement = new Vector3(deltaX, 0, deltaZ);
		_movement = Vector3.ClampMagnitude(_movement, _speed);
		_movement = transform.TransformDirection(_movement);*/

		transform.position += _movement*Time.deltaTime;

		//респаун
		if (transform.position.y < -20)
			transform.position = new Vector3 (0, 0, 0);
	}



	enum EnumWeapon{
		Default,
		Main,
		Secondary }

	private IEnumerator switch_weapon(EnumWeapon type){
		if(type == EnumWeapon.Main){
			_animator.ResetTrigger ("AttackArms");
			_animator.ResetTrigger ("Reload");
			_animator.ResetTrigger ("DefaultArms");
			_animator.ResetTrigger ("SecondaryAttackArms");
			_animator.SetBool ("MainWeapon", true);
			_animator.SetTrigger ("AttackArms");
			yield return new WaitForSeconds(0.5f);
			_DefaultWeapon.SetActive(false);
			_SecondaryWeapon.SetActive(false);
			_MainWeapon.SetActive (true);
			//_wordlControl.PAUSE = false;
		}
		else if(type == EnumWeapon.Secondary){
			_animator.ResetTrigger ("Reload");
			_animator.ResetTrigger ("SecondaryAttackArms");
			_animator.ResetTrigger ("DefaultArms");
			_animator.ResetTrigger ("AttackArms");
			_animator.SetBool ("MainWeapon", false);
			_animator.SetTrigger ("SecondaryAttackArms");
			yield return new WaitForSeconds(0.5f);
			_DefaultWeapon.SetActive(false);
			_MainWeapon.SetActive (false);
			_SecondaryWeapon.SetActive(true);
			//_wordlControl.PAUSE = false;
		}
		else if(type == EnumWeapon.Default){
			_animator.ResetTrigger ("Reload");
			_animator.ResetTrigger ("DefaultArms");
			_animator.ResetTrigger ("AttackArms");
			_animator.ResetTrigger ("SecondaryAttackArms");
			_animator.SetBool ("MainWeapon", false);
			_animator.SetTrigger ("DefaultArms");
			yield return new WaitForSeconds(0.5f);
			_MainWeapon.SetActive (false);
			_SecondaryWeapon.SetActive(false);
			_DefaultWeapon.SetActive(true);
			//_wordlControl.PAUSE = false;
		}_wordlControl.PAUSE = false;}

	/*private IEnumerator reload(){
		_animator.ResetTrigger ("Reload");
		_animator.ResetTrigger ("DefaultArms");
		_animator.ResetTrigger ("AttackArms");
		_animator.ResetTrigger ("SecondaryAttackArms");
		_animator.SetTrigger ("Reload");
		return null;
	}*/

	void OnCollisionStay(Collision colinfo)
	{
		_canjump = true;
	}
}