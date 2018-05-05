using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
	public GameObject enemy1;
    
	private float timer = 0f;
	private float tempoSpawn;

	public float tempoSobra = 2f;
	public float quantity;
	//private int level;
	public bool working = false;
	public bool spawning = false;

	void Start ()
	{

	}

	public void SetNewWave (WaveManager.Wave wave)
	{
		// Sets for new wave, qtd and time from Wave.
		tempoSpawn = wave.spawnTime;
		quantity = wave.spawnQtt;
		//level = wave.spawnLevel;
	}

	public void StartWave ()
	{
		working = true;
		StartCoroutine (WaitSeconds (tempoSobra));
	}

	void Update ()
	{
		if (!spawning)
			return;

		timer += Time.deltaTime;

		// Only stops working if timer is over AND no more enemies remain.
		if (timer >= tempoSpawn) {
			if (quantity <= 0 && GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {
				working = false;
				spawning = false;
			} else if (quantity > 0) {
				float random = Random.Range (-10f, 10f);
				transform.position = new Vector3 (random, 8, 0);
				Instantiate (enemy1, transform.position, Quaternion.identity);
				timer = 0;
				quantity = quantity - 1;
			}
		}
	}

	IEnumerator WaitSeconds (float time)
	{
		yield return new WaitForSeconds (time);
		spawning = true;
	}
}
