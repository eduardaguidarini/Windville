using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbacaxiNavalha : BulletInterface
{
	// Pineapple Bullet
	public float translationX;
	public float translationY;

	public float danoPlayer = 0.3f;
	public int velTiro = 5;
	public GameObject particleExplosao;


	void Start ()
	{

	}

	void Update ()
	{		 
		translationX = axisX * Mathf.Rad2Deg * Time.deltaTime / 100;
		translationY = axisY * Mathf.Rad2Deg * Time.deltaTime / 100;

		var degrees = Mathf.Atan2 (axisY, axisX);
		Vector3 newDirection = new Vector3 (Mathf.Cos (degrees) * Time.deltaTime * velTiro, Mathf.Sin (degrees) * Time.deltaTime * velTiro, 0f);

		transform.rotation = Quaternion.Euler (0f, 0f, degrees * Mathf.Rad2Deg - 90);
		transform.position += newDirection;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.GetComponent<Enemy1> ().Applydamage (danoPlayer);
			Destroy (this.gameObject);
		}

		if (other.gameObject.tag == "Plataforma") {
			Instantiate (particleExplosao, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}

	public void setLevel (int lvl)
	{
		if (lvl == 1) {
			danoPlayer = 0.3f;
			velTiro = 5;
			fireRate = 0.15f;
		} else if (lvl == 2) {
			danoPlayer = 0.4f;
			velTiro = 6;
			fireRate = 0.10f;
		} else if (lvl == 3) {
			danoPlayer = 0.5f;
			velTiro = 6;
			fireRate = 0.07f;
		}
	}
}
