﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class MinionManager : MonoBehaviour {

	public GameObject MinionPrefab;
	public GameObject WardPrefab;

	public event Action NewMinion = delegate {};

	private float minX = -4;
	private float maxX =  4;
	private float minY = -4;
	private float maxY =  4;

	private int minionCount;
	private int maxMinions = 5;

//	private Queue<GameObject> _Minions = new Queue<GameObject>();

	void Awake () {
		minionCount = 0;
		var eventBus = GameObject.Find("EventBus").GetComponent<EventsScript>();
		eventBus.EventNewMinion += SpawnNewMinion;
		eventBus.EventDestroyedMinion += DestroyMinion;

	}

	private void DestroyMinion() {
		minionCount--;
	}

	private void SpawnNewMinion() {
		if (minionCount < maxMinions) {
			NewMinion.Invoke ();

			GameObject minion = GameObject.Instantiate (MinionPrefab);
			minion.transform.position = NewSpawnPoint ();
			minionCount++;
		}
	}
	private Vector2 NewSpawnPoint () {
		return this.gameObject.transform.position;
	}

	public bool Capacity() {
		return this.minionCount < 5;
	}
}
