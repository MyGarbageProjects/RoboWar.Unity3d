using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RaycastShoot : MonoBehaviour {
	enum Ceasefire {
		SingleFire,
		MultiFire}

	[SerializeField] private WorldControl _wordlControl;
	//exterior features
	[SerializeField] private Camera _fpsCam;
	[SerializeField] private Transform _gunEnd;
	[SerializeField] private Animator _animator;
	[SerializeField] private ParticleSystem _gunParticleSystem;
	[SerializeField] private AudioSource[] _gunAudio;
	[SerializeField] private Light _flash;
	[SerializeField] private GameObject _sparkPrefab;
	//Interal features
	[SerializeField] private bool _renderLine = false;
	[Range (0.06f, 1)]
	[SerializeField] private float shotDuration = .09f;
	[SerializeField] Ceasefire _ceasefire = Ceasefire.SingleFire;
	[SerializeField] private float _gunDamage = 1;
	[SerializeField] private float _weaponRange = 75f;
	[SerializeField] private float _hirForce = 100f;
	[Range(0.0f, 0.1f)]
	[SerializeField] private float _scatter = .005f;
	[SerializeField] private int _magazineCapacity = 30;
	[SerializeField] private int _totalAmmo = 120;
	[SerializeField] private int _ammoInMagazine = 30;


	[SerializeField] private string _nameAnimFire;
	[SerializeField] private string _nameAnimIdle;

	//triggers
	private bool _fire;
	private bool _reload = false;
	private bool _magazine_empty = false;

	//Debug func
	private LineRenderer _laserLine;

    //UI
    [SerializeField] private Text _screenAmmo;
    [SerializeField] private Text _screenTAmmo;


	void Start () {
		_laserLine = GetComponent<LineRenderer>();
	}
	
	
	void Update () {

		//UI on Screen
		_screenAmmo.text = +_ammoInMagazine > 9? _ammoInMagazine.ToString() :  "  "+_ammoInMagazine;
		_screenTAmmo.text = " / " + _totalAmmo;

		//Act
		_reload = _totalAmmo > 0? Input.GetButtonDown("Reload") : false;


		if(!_magazine_empty)		
			_fire = _ceasefire == Ceasefire.SingleFire? Input.GetButtonDown("Fire") : Input.GetButton("Fire");
		else{
			_fire = false;
			_animator.ResetTrigger(_nameAnimFire);
			_animator.SetTrigger (_nameAnimIdle);
		}


		//------------------------------------------------------------	
		/*Vector3 point = new Vector3 (_fpsCam.pixelWidth / 2, _fpsCam.pixelHeight / 2,0);
		Ray	ray = _fpsCam.ScreenPointToRay (point);
		Debug.DrawRay (ray.origin,  ray.direction*100f, Color.green);*/
		//------------------------------------------------------------
		if(Input.GetButtonUp("Fire") & !_magazine_empty)
		{
			_fire =false;
			_animator.ResetTrigger(_nameAnimFire);
			_animator.SetTrigger (_nameAnimIdle);
		}
		

	}

	void FixedUpdate(){

		if(_fire & (_ammoInMagazine > 0) & !_wordlControl.PAUSE){
			StartCoroutine(Shot());
			_animator.SetTrigger(_nameAnimFire);
			_laserLine.enabled = _renderLine;
		}

		if(_reload & !(_ammoInMagazine == _magazineCapacity) & !_wordlControl.PAUSE){
			StartCoroutine(reload());
		}
	}


	private IEnumerator reload(){
		_wordlControl.PAUSE = true;

		_animator.ResetTrigger ("Reload");
		_animator.ResetTrigger ("DefaultArms");
		_animator.ResetTrigger ("AttackArms");
		_animator.ResetTrigger ("SecondaryAttackArms");
		_animator.SetTrigger ("Reload");

		_gunAudio[2].Play();
		yield return new WaitForSeconds(_gunAudio[2].clip.length-2.5f);
		if(_totalAmmo >= _magazineCapacity){
			_totalAmmo -= (_magazineCapacity - _ammoInMagazine);
			_ammoInMagazine = _magazineCapacity;
		}
		else if(_totalAmmo > 0){
			int _temp = _totalAmmo;
			_temp = _totalAmmo-(_magazineCapacity - _ammoInMagazine);
			_ammoInMagazine = _ammoInMagazine+_totalAmmo<=_magazineCapacity? _ammoInMagazine+_totalAmmo : _magazineCapacity;
			_totalAmmo = _temp>=0? _temp : 0;
		}
		_wordlControl.PAUSE = false;
		_magazine_empty = false;
	}

	private IEnumerator Shot(){
		AmmoControl();
		_gunAudio[1].Play();
		_laserLine.SetPosition(0,_gunEnd.position);

		_wordlControl.PAUSE = true;
		Vector3 rayOrigin = _fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0));
		Vector3 _scatterBullets = _fpsCam.transform.forward + (new Vector3(_scatter * Random.Range(-1,2),_scatter * Random.Range(-1,2),_scatter * Random.Range(-1,2)));

		RaycastHit hit;
		if(Physics.Raycast(rayOrigin, _scatterBullets, out hit, _weaponRange)){
			//Debug.Log ("Distance From me to Block =  " + (Mathf.Sqrt (Mathf.Pow (this.transform.position.x - hit.transform.position.x, 2) + Mathf.Pow (this.transform.position.y - hit.transform.position.y, 2) + Mathf.Pow (this.transform.position.z - hit.transform.position.z, 2))));

			//get distance and cut gun damage
			float _interimDamage = _gunDamage;
			float _distandeFromMeToObject = Mathf.Sqrt (Mathf.Pow (this.transform.position.x - hit.transform.position.x, 2) + Mathf.Pow (this.transform.position.y - hit.transform.position.y, 2) + Mathf.Pow (this.transform.position.z - hit.transform.position.z, 2));
			if (_distandeFromMeToObject < _hirForce * 0.3f)
				_interimDamage = _gunDamage;//if distance < 30% of the total hitForce,do not lose anything
			else if (_distandeFromMeToObject < _hirForce * 0.80f)
				_interimDamage = _gunDamage * 0.75f;//if    30% < distance < 80%  of the total hitForce,lose 35% of total Gundamage
			else if (_distandeFromMeToObject < _hirForce)
				_interimDamage = _gunDamage * 0.5f;//if  80% < distance < 100% of the total fitForce, lose 50% of total Gundamage
			//end block

			if (hit.transform.gameObject.tag == "Enemy") {
				AIhealth enemy = hit.transform.gameObject.GetComponent<AIhealth>();
				//Debug.Log (_interimDamage);
				if(enemy.Health !=0)
					enemy.Hit (_interimDamage);
			}
			_laserLine.SetPosition(1,hit.point);
			StartCoroutine (SparkIndicator (hit.point));
		}
		else{
			_laserLine.SetPosition(1, rayOrigin+(_fpsCam.transform.forward * _weaponRange));
		}


		//Shot Effect-------------------------------------------------
		_flash.enabled = true;
		_gunParticleSystem.Play ();
		_gunAudio[0].Play ();
		yield return new WaitForSeconds (0.05f);
		_flash.enabled = false;
		yield return new WaitForSeconds (shotDuration-0.05f);
		_wordlControl.PAUSE = false;}
		
	private IEnumerator SparkIndicator(Vector3 pos){
		GameObject _dest = Instantiate(_sparkPrefab);
		_dest.transform.position = pos;
		yield return new WaitForSeconds (0.5f);
		Destroy(_dest);}

	private void AmmoControl(){
		if(_ammoInMagazine>0)
			_ammoInMagazine--;

		if(_ammoInMagazine <= 0)
			_magazine_empty = true;}
}
