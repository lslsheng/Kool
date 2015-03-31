﻿using UnityEngine;
using System.Collections;

public class TutorialStatus : MonoBehaviour {
	private static int NumOfPlayers;

	// Use this for initialization
	void Start () {
		NumOfPlayers = GameStatus.TotalPlayerNum;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < GameStatus.TotalPlayerNum; ++i)
		{
			GameObject player = GameStatus.GetPlayerObjById(i);
			if (player){
				PlayerData pd = player.GetComponent<PlayerData>();
				if (pd.health < 0.6){
					pd.health = 1;
				}
			}
		}
	}
}