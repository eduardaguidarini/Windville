using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	// Use this for initialization
	private Rigidbody2D rbPlayer;
	public float playerVel = 10;
	public bool playerOne = true;

	private float tempoTiro = 0f;
	private Rigidbody2D tiroClone;
	private float testeX, testeY = 0;

	public string player = "1";
	public bool vivo = true;

	public GameObject cesta_cherry;
	public GameObject cesta_abacaxi;
	public GameObject cesta_coco;

	public Spawner spawner;

	public int recurso = 10;
	public Text recursoText;
	public Moinho moinho;
	public Animator animator;
	public SpriteRenderer spriteRenderer;

	public Weapon currentWeapon;
	public Weapon[] weaponList;

	// Possible, equipable, weapons. This idealy should be put on a manager if we have time.
	[System.Serializable]
	public class Weapon
	{
		public GameObject weaponBullet;
	}

	public GameObject particulaPlayerMorreu;

	void Start ()
	{
		rbPlayer = GetComponent<Rigidbody2D> ();
		currentWeapon = weaponList [0];
	}


	void Update ()
	{
		//Seleciona Player
		if (!playerOne) {
			player = "2";
		}

		//Limite Tela
		Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0));
		Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		transform.position = new Vector2 (Mathf.Clamp (transform.position.x, minScreenBounds.x + 0.7f, maxScreenBounds.x - 0.7f), Mathf.Clamp (transform.position.y, minScreenBounds.y - 10.7f, maxScreenBounds.y + 0.7f));

		//Movimentação
		float moveHorizontal1 = Input.GetAxis ("Horizontal" + player);
		rbPlayer.velocity = new Vector2 (moveHorizontal1 * playerVel, 0);

		animator.SetFloat ("playerAnimVel", Mathf.Abs (moveHorizontal1));
		if (playerOne) {
			recursoText.text = "Ideas: " + recurso;	
		}

		//Troca lado personagem
		if (moveHorizontal1 > 0) {
			spriteRenderer.flipX = true;
			cesta_cherry.GetComponent<SpriteRenderer> ().flipX = true;
			cesta_abacaxi.GetComponent<SpriteRenderer> ().flipX = true;
			cesta_coco.GetComponent<SpriteRenderer> ().flipX = true;
		} else if (moveHorizontal1 < 0) {
			spriteRenderer.flipX = false;
			cesta_cherry.GetComponent<SpriteRenderer> ().flipX = false;
			cesta_abacaxi.GetComponent<SpriteRenderer> ().flipX = false;
			cesta_coco.GetComponent<SpriteRenderer> ().flipX = false;
		}

		//Cai fora da tela
		if (transform.position.y < -5 && vivo) {
			recursoText.color = Color.red;
			vivo = false;
			Instantiate (particulaPlayerMorreu, transform.position, Quaternion.identity);
			StartCoroutine (Morreu ());
			if (recurso >= 11) {
				recurso -= 10;
			} else {
				recurso = 0;
			}
		}

		//Tiro
		tempoTiro -= Time.deltaTime;

		// Only shoots during waves.
		if ((spawner.working) && (Input.GetAxisRaw ("MiraX" + player) != 0 || Input.GetAxisRaw ("MiraY" + player) != 0)) {
			testeX = Input.GetAxis ("MiraX" + player);
			testeY = Input.GetAxis ("MiraY" + player);
			if (vivo) {
				if (tempoTiro <= 0) {
					Atira ();
					tempoTiro = currentWeapon.weaponBullet.GetComponent<BulletInterface> ().fireRate;
				}
			}
		}
	}

	//Cria tiro
	public void Atira ()
	{
		tiroClone = (Rigidbody2D)Instantiate (currentWeapon.weaponBullet.GetComponent<Rigidbody2D> (), transform.position, Quaternion.identity);

		tiroClone.GetComponent<BulletInterface> ().axisX = testeX;
		tiroClone.GetComponent<BulletInterface> ().axisY = testeY;
	}

	//Tempo morte
	IEnumerator Morreu ()
	{
		yield return new WaitForSeconds (0.5f);
		recursoText.color = Color.black;
		yield return new WaitForSeconds (4.5f);
		transform.position = new Vector2 (0, -0.78f);
		vivo = true;
	}

	public void equipWeapon (int n, int lvl)
	{
		// 1 CherryBomb
		// 2 AbacaxiNavalha
		// 3 Coconut
		currentWeapon = weaponList [n];
		animator.SetTrigger ("playerAnimPega");

		// Toggles which weapon he's carrying.
		cesta_cherry.SetActive (false);
		cesta_abacaxi.SetActive (false);
		cesta_coco.SetActive (false);

		if (n == 1) {
			cesta_cherry.SetActive (true);
			currentWeapon.weaponBullet.GetComponent<BulletCherryBomb> ().setLevel (lvl);
		} else if (n == 2) {
			cesta_abacaxi.SetActive (true);
			currentWeapon.weaponBullet.GetComponent<BulletAbacaxiNavalha> ().setLevel (lvl);
		} else if (n == 3) {
			cesta_coco.SetActive (true);
			currentWeapon.weaponBullet.GetComponent<BulletCoconut> ().setLevel (lvl);
		}
	}

	// Only one player can equip a weapon at a time, so it "robs" the other one.
	public void checkWeapon (int n)
	{
		if (currentWeapon == weaponList [n]) {
			currentWeapon = weaponList [0];
		}
	}
}
