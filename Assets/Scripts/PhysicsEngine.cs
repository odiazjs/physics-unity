using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour {

	public float mass; // [kg]
	public Vector3 velocity; // [m s^-1]
	public Vector3 netForce; // N [kg m s^-2]
	private List<Vector3> forceVectorList = new List<Vector3>();
	
	// Draw Forces trails
	public bool showTrails = true;
	private LineRenderer lineRenderer;
	private int numberOfForces;

	void Start () {
		SetupThrustTrails();
	}

	void FixedUpdate () {
		RenderTrails();
		SumForces();
		UpdatePosition();
    }
	
	void SetupThrustTrails () {
		forceVectorList = GetComponent<PhysicsEngine>().forceVectorList;
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		lineRenderer.SetColors(Color.yellow, Color.yellow);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.useWorldSpace = false;
	}

	void RenderTrails () {
		if (showTrails) {
			lineRenderer.enabled = true;
			numberOfForces = forceVectorList.Count;
			lineRenderer.SetVertexCount(numberOfForces * 2);
			int i = 0;
			foreach (Vector3 forceVector in forceVectorList) {
				lineRenderer.SetPosition(i, Vector3.zero);
				lineRenderer.SetPosition(i+1, -forceVector);
				i = i + 2;
			}
		} else {
			lineRenderer.enabled = false;
		}
	}

	public void AddForce (Vector3 force) {
		forceVectorList.Add(force);
	}

	void UpdatePosition () {
		// F = m * a || a = F / m
		Vector3 acceleration = netForce / mass;
		velocity += acceleration * Time.deltaTime;
		transform.position += velocity * Time.deltaTime;
	}

	void SumForces () {
		netForce = Vector3.zero;
		forceVectorList.ForEach(force => {
			netForce = netForce + force;
		});
		forceVectorList = new List<Vector3>();
	}
}
