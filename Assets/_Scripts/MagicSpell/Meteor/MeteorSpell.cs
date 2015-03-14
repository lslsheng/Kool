﻿using UnityEngine;
using System.Collections;

public class MeteorSpell : MagicSpell {

	private GameObject meteor;

	public MeteorSpell()
	{
		meteor = SpellDB.meteor;
	}
	public override IEnumerator castMagic (GameObject caster, Vector3 hitpoint = default(Vector3)) 
	{
		
		Debug.Log("Meteor Activiated!");
		Vector3 castLocation = caster.transform.position + new Vector3 (0, 20, 0);
		// TODO Hard code
		Quaternion lookedQua = Quaternion.LookRotation (castLocation - hitpoint);

		GameObject gb = GameObject.Instantiate (meteor, castLocation, lookedQua) as GameObject;


		MovableUnit movUnit = gb.GetComponent<MovableUnit> ();
		movUnit.MoveTo (hitpoint);
		yield return new WaitForSeconds(0.1f);
	}
}
