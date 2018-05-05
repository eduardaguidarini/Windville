using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moinho : MonoBehaviour
{
	public int moinhoVida = 100;

	public Player player;
	public Spawner spawner;
	public float tempoRecurso = 0;

	public int recurso = 10;
	public int reparo = 10;
	public int level = 1;
	public Text recursoText;
	public Text levelText;
	public Text reparoText;
	public Text vidaMoinhoText;
	public GameObject lvlText, recText, repText, vidaText;
	public bool naArea = false;
	public bool naAreaP1 = false;
	public bool naAreaP2 = false;

	void Start ()
	{
		lvlText.SetActive (false);
		recText.SetActive (false);
		repText.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		vidaMoinhoText.text = "Life " + moinhoVida;

		if (spawner.working) {
			tempoRecurso += Time.deltaTime;
		}
	
		// Checks health for death
		if (moinhoVida <= 0) {
			Debug.Log ("You Lose");
		}

		// Checks health for warning
		if (moinhoVida <= 25) {
			vidaMoinhoText.color = Color.red;
		} else {
			vidaMoinhoText.color = Color.black;
		}

		// Awards resources per second
		if (tempoRecurso >= 1) {
			tempoRecurso = 0;
			player.recurso += 1 * level;
		}

		reparoText.text = "Repair " + reparo;
		if (((naAreaP1 && Input.GetButtonDown ("Repara1")) || (naAreaP2 && Input.GetButtonDown ("Repara2"))) && player.recurso >= reparo && moinhoVida <= 95) {
			moinhoVida += 5;
			player.recurso -= reparo;
			reparo += 10;
		}

		// Hides text during waves
		if (spawner.working) {
			lvlText.SetActive (false);
			recText.SetActive (false);
			return;
		}

		if (player.recurso >= recurso) {
			recursoText.color = Color.green;
		} else {
			recursoText.color = Color.red;
		}

		if (player.recurso >= reparo) {
			reparoText.color = Color.green;
		} else {
			reparoText.color = Color.red;
		}


		if (level <= 3) {
			recursoText.text = "Ideas " + recurso;
			levelText.text = "Level " + level;
		} else {
			recText.SetActive (false);
			levelText.text = "Max Level";
		}

		if (((naAreaP1 && Input.GetButtonDown ("Interage1")) || (naAreaP2 && Input.GetButtonDown ("Interage2"))) && player.recurso >= recurso) {
			if (level == 0) {
				player.recurso -= 10;
				recurso += 10;
				level++;
			} else if (level == 1) {
				player.recurso -= 20;
				recurso += 20;
				level++;
			} else if (level == 2) {
				player.recurso -= 30;
				recurso += 30;
				level++;
			}
		}

	
	}

	// Colisions for text deploy
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			lvlText.SetActive (true);
			recText.SetActive (true);
			repText.SetActive (true);
			vidaText.SetActive (true);

			if (other.gameObject.GetComponent<Player> ().playerOne) {
				naAreaP1 = true;
			} else {
				naAreaP2 = true;
			}
		}     
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			lvlText.SetActive (true);
			recText.SetActive (true);
			repText.SetActive (true);
			vidaText.SetActive (true);

			if (other.gameObject.GetComponent<Player> ().playerOne) {
				naAreaP1 = true;
			} else {
				naAreaP2 = true;
			}
		}     
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			lvlText.SetActive (false);
			recText.SetActive (false);
			repText.SetActive (false);

			if (other.gameObject.GetComponent<Player> ().playerOne) {
				naAreaP1 = false;
			} else {
				naAreaP2 = false;
			}
		}     
	}

	public void Applydamage (int danoLevado)
	{
		moinhoVida -= danoLevado;
	}
}
