﻿using UnityEngine;
using System.Collections;
using System;

public class SpitterScript : MonoBehaviour {

	private enum State {
		IDLE,
		APPROACHING,
		ATTACKING,
		DEAD
	}
	private State STATE;

	private FOVScript fov;

	private static float walkingSpeed = 3.0F;

	private static float attackRange = 4.0F;
	private static float attackCooldown = 500F;

	private DateTime lastAttack;

	public GameObject bulletPrefab;

	void Start () {
		STATE = State.IDLE;
		fov = GetComponent<FOVScript> ();
		lastAttack = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
		bool canSee = fov.canSeePlayer ();

		// Unobstructed raycast between enemy and player
		if (canSee) {
			float distance = fov.getDistance ();
			double timeSinceLastAttack = DateTime.Now.Subtract (lastAttack).TotalMilliseconds;
			if (distance < attackRange) {
				if (timeSinceLastAttack >= attackCooldown) {
					STATE = State.ATTACKING;		// within attack range and able to attack
				} else {
					STATE = State.IDLE;				// within attack range but on cooldown
				}
			} else {
				STATE = State.APPROACHING;			// not within attack range
			}
		} else {
			STATE = State.IDLE;						// unaware of player
		}

	}

	void FixedUpdate () {

		switch (STATE) {

		case State.IDLE:							// IDLE: do nothing
			return;

		case State.ATTACKING:						// ATTACKING: create a bullet
			lastAttack = DateTime.Now;

			GameObject newBullet = (GameObject) GameObject.Instantiate (bulletPrefab, this.transform.position, Quaternion.identity);
			newBullet.GetComponent<BulletScript> ().direction = fov.getNormalizedDisplacement ();
			break;

		case State.APPROACHING:						// APPROACHING: move towards enemy
			Vector2 velocity = fov.getNormalizedDisplacement() * walkingSpeed;
			transform.Translate (velocity * Time.fixedDeltaTime);
			break;

		default:
			break;

		}
	}
}
