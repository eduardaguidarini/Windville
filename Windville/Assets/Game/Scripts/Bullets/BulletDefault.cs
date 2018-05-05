using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDefault : BulletInterface
{
	// Default Bullet
	public float translationX;
	public float translationY;

	public float danoPlayer = 1;
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
	}

}
