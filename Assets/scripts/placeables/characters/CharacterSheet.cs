using UnityEngine;
using System.Collections;
using System;

public class CharacterSheet : MonoBehaviour {
	public string displayName;
	//attributes
	public int hp = 10;//max health
	public int mp = 5;//max mana
	public int ap = 2;//max action points
	//stats
	public int strength;
	public int intelligence;
	public int agility;
	public int defense;
	public int dodge;
	public int intuition;
	public int speed;
	//resistances
	public float earthResistance = 1.0f;
	public float fireResistance = 1.0f;
	public float iceResistance = 1.0f;
	public float waterResistance = 1.0f;
	public float airResistance = 1.0f;
	public float holyResistance = 1.0f;
	public float darkResistance= 1.0f;	
	public float ailmentResistance = 1.0f;
	//all character stats
	public Ailment[] ailmentImmunities;//ailments the character can't get
}



