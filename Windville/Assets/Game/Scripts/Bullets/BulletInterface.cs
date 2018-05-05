using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletInterface : MonoBehaviour
{
	public float fireRate;
	public float axisX;
	public float axisY;

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}
}
