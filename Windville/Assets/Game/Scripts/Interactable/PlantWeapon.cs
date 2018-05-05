using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantWeapon : MonoBehaviour
{
	[Range (0, 3)]
	public int id;
	public int recurso = 10;
	public int level = 0;

	public Text recursoText;
	public Text levelText;
	public Player player1;
	public Player player2;
	public GameObject lvlText, recText;
	public SpriteRenderer spriteRenderer;

	private bool naAreaP1 = false;
	private bool naAreaP2 = false;

	public Spawner spawner;

	void Start ()
	{
		lvlText.SetActive (false);
		recText.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{
		if (spawner.working) {
			lvlText.SetActive (false);
			recText.SetActive (false);
			return;
		}

		if (player1.recurso >= recurso) {
			recursoText.color = Color.green;
		} else {
			recursoText.color = Color.red;
		}

		if (level == 0) {			
			recursoText.text = "Ideas " + recurso;
			levelText.text = "Plant!";
		} else if (level == 1 || level == 2) {
			recursoText.text = "Ideas " + recurso;
			levelText.text = "Level " + level;
		} else if (level == 3) {
			recText.SetActive (false);
			levelText.text = "Max Level";
		}

		if (((naAreaP1 && Input.GetButtonDown ("Interage1")) || (naAreaP2 && Input.GetButtonDown ("Interage2"))) && player1.recurso >= recurso) {
			if (level == 0) {
				player1.recurso -= 10;
				recurso += 10;
				level++;
				spriteRenderer.enabled = !spriteRenderer.enabled;
			} else if (level == 1) {
				player1.recurso -= 20;
				recurso += 20;
				level++;
				spriteRenderer.enabled = true;
			} else if (level == 2) {
				player1.recurso -= 30;
				recurso += 30;
				level++;
			}

			if (naAreaP1) {
				player1.equipWeapon (id, level);
				player2.checkWeapon (id);
			} else if (naAreaP2) {
				player2.equipWeapon (id, level);
				player1.checkWeapon (id);
			}

		} else if (level >= 1) {
			if (naAreaP1 && Input.GetButtonDown ("Repara1")) {
				player1.equipWeapon (id, level);
				player2.checkWeapon (id);
			} else if (naAreaP2 && Input.GetButtonDown ("Repara2")) {
				player2.equipWeapon (id, level);
				player1.checkWeapon (id);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player" && !spawner.working) {
			lvlText.SetActive (true);
			recText.SetActive (true);
			if (other.gameObject.GetComponent<Player> ().playerOne) {
				naAreaP1 = true;
			} else {
				naAreaP2 = true;
			}
		}     
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player" && !spawner.working) {
			lvlText.SetActive (true);
			recText.SetActive (true);
			if (other.gameObject.GetComponent<Player> ().playerOne) {
				naAreaP1 = true;
			} else {
				naAreaP2 = true;
			}
		}     
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player" && !spawner.working) {
			lvlText.SetActive (false);
			recText.SetActive (false);
			if (other.gameObject.GetComponent<Player> ().playerOne) {
				naAreaP1 = false;
			} else {
				naAreaP2 = false;
			}
		}     
	}
}
