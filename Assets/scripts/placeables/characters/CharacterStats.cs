using UnityEngine;
using System.Collections;
using System;

public enum CharacterResistance{
	Fire,
	Water,
	Ice,
	Air,
	Earth,
	Holy,
	Dark,
	Ailment
}

public enum CharacterStat{
	Strength,//increase attack ability
	Intelligence,//increase casting ability
	Agility,//able to move on the mAP more effectively
	Defense,//base defense against attacks
	MagicalDefense,//base defense against magic
	Dodge,//able to miss physical attacks
	Intuition,//able to miss magical attacks
	Speed//able to take more turns
}

[Serializable]
public class StatModifier{
	public CharacterStat stat;
	public int modifier;
}

[Serializable]
public class ResistanceModifier{
	public CharacterResistance resistance;
	public float addModifier;
}

[Serializable]
public class CharacterModifier{
	public StatModifier[] statModifiers;
	public Ailment[] immunities;
	public ResistanceModifier[] resistanceModifiers;
	public int hpModifier;
	public int mpModifier;
	public int apModifier;
}






		

