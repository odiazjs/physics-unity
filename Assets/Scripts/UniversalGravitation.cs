using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour {

	private PhysicsEngine[] physicsEngineArray;
	private const float bigG = 6.673e-11f;			// [m^3 s^-2 kg^-1]

	// Use this for initialization
	void Start () {
		physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CalculateGravity();
	}

	void CalculateGravity () {
		foreach (PhysicsEngine engineA in physicsEngineArray) {
			foreach (PhysicsEngine engineB in physicsEngineArray) {
				if (engineA != engineB && engineA != this) {
					Debug.Log("Calculating gravitational force exerted on " + engineA.name +
						" due to the gravity of " + engineB.name);
					Vector3 offsetDistance = engineA.transform.position - engineB.transform.position;
					float rSquared = Mathf.Pow(offsetDistance.magnitude, 2f);
					float gravityMagnitude = bigG * engineA.mass * engineB.mass / rSquared;
					Vector3 gravityFeltVector = gravityMagnitude * offsetDistance.normalized;
					
					engineA.AddForce(-gravityFeltVector);
				}
			}
		}
	}
}
