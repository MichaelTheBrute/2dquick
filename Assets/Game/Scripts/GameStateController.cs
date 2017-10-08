using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateController : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

 	PlayerController pc = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnPlayer () {
		if (playerPrefab == null) {
			return;
		}

		pc = Instantiate (playerPrefab).GetComponent<PlayerController>();
		//gameCam.target = pc.transform;
	}

	void SpawnEnemy(int x, int y) {
		GameObject enemy = Instantiate (enemyPrefab);
		enemy.transform.position = new Vector3 (x, 0, y);
	}

	public void SpawnEnemies () {

		if (enemyPrefab == null) {
			return;
		}

		SpawnEnemy (10, 8);
		SpawnEnemy (5, 3);
		SpawnEnemy (-8, 12);
		SpawnEnemy (20, -20);
	}
}
