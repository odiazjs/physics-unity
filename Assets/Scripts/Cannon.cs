using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

	public float maxLaunchSpeed;
	public AudioClip windUpSound, launchSound;
	public PhysicsEngine ball;
	public float launchSpeed;
	private AudioSource audioSource;
	private float extraSpeedPerFrame;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = windUpSound; // so we know the length of the clip
		extraSpeedPerFrame = ( maxLaunchSpeed * Time.fixedDeltaTime ) / audioSource.clip.length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		// Increase launch speed
		launchSpeed = 0;
		InvokeRepeating("IncreaseLaunchSpeed", 0.5f, Time.fixedDeltaTime);
		audioSource.clip = windUpSound;
		audioSource.Play();
	}

	void OnMouseUp () {
		Debug.Log("Cannon clicked!!!");
		CancelInvoke("IncreaseLaunchSpeed");
		audioSource.Stop();
		audioSource.clip = launchSound;
		audioSource.Play();

		PhysicsEngine newBall = Instantiate(ball) as PhysicsEngine;
		newBall.transform.parent = GameObject.Find("BallSpawn").transform;
		Vector3 launchVelocity = new Vector3(1, 1, 0).normalized * launchSpeed;
		newBall.velocity = launchVelocity;
	}

	void IncreaseLaunchSpeed () {
		if (launchSpeed <= maxLaunchSpeed) {
			launchSpeed += extraSpeedPerFrame;
		}
	}
}
