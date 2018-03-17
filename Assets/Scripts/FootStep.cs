using UnityEngine;
using System.Collections;

public class FootStep : MonoBehaviour {

	[SerializeField] private AudioClip[] _FootSteps;
	private AudioSource _audiosourse;
	//private bool _stepping;
	private float _pitch; 
	private int _next =-1;


	void Start () {
		_audiosourse = GetComponent<AudioSource>();
	}

	void Update () {
		if (!_audiosourse.isPlaying) {
			if((Input.GetButton("Vertical") || Input.GetButton("Horizontal"))){
				bool running = Input.GetKey (KeyCode.LeftShift);
				_pitch = running? 2.2f : 1.2f;
				_audiosourse.pitch = _pitch;
				//StepTime = 0.5f;
				footFall();
			}
		}
	}


	void footFall()
	{
		//_stepping = true;
		_next++;
		if (_next > _FootSteps.Length-1)
			_next = 0;
		_audiosourse.PlayOneShot (_FootSteps [_next]);
		//audiosourse.PlayOneShot (FootSteps [Random.Range(0,FootSteps.Length)]);
		//yield return new  WaitForSeconds (FootSteps[_next].length );
		//stepping = false;

	}
}
