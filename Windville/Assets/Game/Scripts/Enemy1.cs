using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{

	public Vector2 alvo;
	public GameObject moinho;

	public float enemyVel = 0.5f;
	public float enemyVida = 1;
	public bool deuDano = false;
	public bool naArea = false;
	public float timerDano;

	void Start ()
	{
		// Sets path and ref
		alvo = new Vector2 (Random.Range (-0.5f, 0.5f), Random.Range (-0.35f, 1.5f));
		moinho = GameObject.FindWithTag ("Moinho");
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Maks move
		transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), alvo, enemyVel * Time.deltaTime);

		if (enemyVida <= 0) {
			Destroy (gameObject);
		}

		timerDano += Time.deltaTime;
		if (deuDano && timerDano >= 3) {
			deuDano = false;
		}

		// Ticks damage.
		if (naArea && !deuDano) {
			moinho.GetComponent<Moinho> ().moinhoVida -= 5;
			deuDano = true;
			timerDano = 0;
		}
	}

	public void Applydamage (float danoRecebido)
	{
		enemyVida -= danoRecebido;
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.tag == "Moinho") {
			naArea = true;
		}     
	}
}
