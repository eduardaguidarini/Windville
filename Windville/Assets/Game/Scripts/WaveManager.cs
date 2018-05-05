using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
	// Serializable wave system, for packing data together
	[System.Serializable]
	public class Wave
	{
		public int waveNumber;
		public int spawnQtt;
		public int spawnLevel;
		public float spawnTime;
	}

	public Wave[] waves;
	private Queue<Wave> waveQ;

	public float intermissionTime = 30;
	private float currentIntermissionTime;
	public Spawner spawner;
	public Text textotempoVolta;
	public GameObject tempoVolta;

	public Camera gameCamera;
	private Vector3 gameCameraRest, gameCameraRooster;
	public GameObject[] pannedObjects;
	public int yOffset;
	public float offsetTime = 2.5f;
	private float offsetTimer;

	private bool pannedUp = false;

	// Use this for initialization
	void Start ()
	{
		waveQ = new Queue<Wave> (waves);
		currentIntermissionTime = intermissionTime;
		gameCameraRest = gameCamera.transform.position;
		gameCameraRooster = gameCameraRest + new Vector3 (0, yOffset, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentIntermissionTime <= 5.5f) {
			textotempoVolta.color = Color.red;
		} else {
			textotempoVolta.color = Color.black;
		}

		// Not spawning, starts or is on intermission.
		if (waveQ.Count > 0 && !spawner.working && currentIntermissionTime > 0) {
			// CHECKS IF PANNED, IF IT IS, PANS BACK
			if (pannedUp) {
				offsetTimer = Time.time;
				Debug.Log ("Fired!");
				StartCoroutine (PanCam (gameCamera, gameCameraRest, offsetTimer));
				foreach (GameObject panObj in pannedObjects) {
					StartCoroutine (PanObj (panObj, (panObj.transform.position - new Vector3 (0, yOffset, 0)), offsetTimer));
				}
				pannedUp = false;
			}
			tempoVolta.SetActive (true);
			currentIntermissionTime -= Time.deltaTime;
			textotempoVolta.text = "" + currentIntermissionTime.ToString ("f0");
			// Atualizar Timer na tela

		} 
		// Not spawning, no intermission, starts next wave.
		else if (waveQ.Count > 0 && !spawner.working && currentIntermissionTime <= 0) {
			// CHECKS IF PANNED, IF IT IS NOT, PANS BEFORE GAME ON
			if (!pannedUp) {
				offsetTimer = Time.time;
				StartCoroutine (PanCam (gameCamera, gameCameraRooster, offsetTimer));
				foreach (GameObject panObj in pannedObjects) {
					StartCoroutine (PanObj (panObj, (panObj.transform.position + new Vector3 (0, yOffset, 0)), offsetTimer));
				}
				pannedUp = true;
			}
			tempoVolta.SetActive (false);
			spawner.SetNewWave (waveQ.Dequeue ());
			spawner.StartWave ();
			currentIntermissionTime = intermissionTime;

		} 
		// No more waves, spawner not working.
		else if (waveQ.Count == 0 && !spawner.working) {
			// Make winning face
			Debug.Log ("A winner is you, no moar enemies!");
		}
	}

	IEnumerator PanObj (GameObject obj, Vector3 fate, float time)
	{
		Vector3 startPos = obj.transform.position;
		Vector3 newPos;
		float elapsedTime = 0f;

		while (elapsedTime < 1) {
			elapsedTime += Time.deltaTime / offsetTime;
			obj.transform.position = Vector3.Lerp (startPos, fate, elapsedTime);
			yield return null;
		}
	}

	IEnumerator PanCam (Camera cam, Vector3 fate, float time)
	{
		Vector3 startPos = cam.transform.position;
		Vector3 newPos;
		float elapsedTime = 0f;

		while (elapsedTime < 1) {
			elapsedTime += Time.deltaTime / offsetTime;
			cam.transform.position = Vector3.Lerp (startPos, fate, elapsedTime);
			yield return null;
		}
	}

}
