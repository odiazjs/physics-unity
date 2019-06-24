using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidDrag : MonoBehaviour {

	[Range(1f, 2f)]
	public float velocityExponent;
	public float dragCoeficient;
	public PhysicsEngine physicsEngine;

	// Use this for initialization
	void Start () {
		physicsEngine = GetComponent<PhysicsEngine>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 velocityVector = -physicsEngine.velocity;
		float speed = velocityVector.magnitude;
		float dragSize = CalculateDrag(speed);
		Vector3 dragDirection = dragSize * velocityVector;
		physicsEngine.AddForce(dragDirection);
	}

	float CalculateDrag (float speed) {
		return dragCoeficient * Mathf.Pow(speed, velocityExponent);
	}
}
