using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestructor : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Destroy (gameObject, GetComponent<ParticleSystem> ().main.duration);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
