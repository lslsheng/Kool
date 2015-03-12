﻿using UnityEngine;
using System.Collections;

public class CastingAid : MonoBehaviour {
	public GameObject trajectoryPrefab;
	private UserInputManager inputManager;
	private Animator animator;

	private bool isInitiated = false;
//	private WizardAttackMeans attackMeans;
	
	void Start()
	{
		// mapping input events
		inputManager = GetComponent<UserInputManager> ();
//		inputManager.OnPressLBumper += HandleLBumper;
//		inputManager.OnPressRBumper += HandleRBumper;
//		inputManager.OnPressButton += HandleButton;
		animator = GetComponentInChildren<Animator> ();
//		attackMeans = GetComponent<WizardAttackMeans> ();
	}

	// Update is called once per frame
	void Update () {
		ShowTrajAtAiming ();
	}

	void ShowTrajAtAiming ()
	{
		if(inputManager.rightInput.magnitude > 0
		   && !animator.GetBool("isCasting")
		   && !animator.GetCurrentAnimatorStateInfo(0).IsName("isCasting")
		   && !isInitiated)
		{
			GameObject trajectory = 
				Instantiate(trajectoryPrefab,this.transform.position,transform.rotation) 
					as GameObject;
			StartCoroutine(InnerCoolDown());
			trajectory.transform.Translate(new Vector3(0.0f, 0.0f, trajectory.transform.localScale.y));
			trajectory.transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f), 90.0f);
			trajectory.transform.parent = this.gameObject.transform;
		}
	}

	IEnumerator InnerCoolDown()
	{
		isInitiated = true;
		yield return new WaitForSeconds (0.5f);
		isInitiated = false;
	}

	bool isPosAiming = false;
	public GameObject castingCirclePrefab;
	GameObject castingCircle;
	float castingEnergy;
	public void StartAiming()
	{
		isPosAiming = true;
		StartCoroutine (ReleaseCastingCircle ());
	}

	IEnumerator ReleaseCastingCircle()
	{
		castingCircle = 
			Instantiate(castingCirclePrefab,this.transform.position,transform.rotation) 
				as GameObject;
		castingCircle.transform.parent = this.gameObject.transform;
		castingEnergy = 0;
		for(int i = 0; i < 50; ++i)
		{
			yield return new WaitForSeconds (0.1f);
			castingCircle.transform.localScale += new Vector3(1.0f, 0.0f, 1.0f);
			castingEnergy += 0.5f;
		}

		// End aiming if player does not give the second input in time
		isPosAiming = false;
		Destroy (castingCircle);

	}

	public Vector3 EndAiming()
	{
		if(!isPosAiming)
		{

			return new Vector3 ();
		}
		else
		{
			isPosAiming = false;
			return transform.position + castingEnergy * transform.forward;
		}

	}

}