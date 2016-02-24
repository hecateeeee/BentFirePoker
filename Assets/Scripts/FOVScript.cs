﻿using UnityEngine;
using System.Collections;

public class FOVScript : MonoBehaviour
{

	private Transform target;
	private CircleCollider2D thisCircleCollider;

	// Use this for initialization
	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		thisCircleCollider = GetComponent<CircleCollider2D> ();
	}

	public bool canSeePlayer()
	{
		Vector2 currentLocation = transform.position;
		Vector2 playerLocation = target.position;

		thisCircleCollider.enabled = false;
		RaycastHit2D hit = Physics2D.Raycast (currentLocation, playerLocation - currentLocation);
		thisCircleCollider.enabled = true;

		return (hit.collider.gameObject.tag == "Player");
	}

	public float getDistance() {
		return Vector2.Distance (transform.position, target.position);
	}

	public Vector2 getNormalizedDisplacement() {
		Vector2 displacement = (target.position - transform.position);
		displacement.Normalize ();
		return displacement;
	}
		
}
