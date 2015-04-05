using UnityEngine;
using System.Collections;
using System;

public enum DamageType{
	NonElemental, //can't have resistances to.
	Physical,
	Fire,
	Water,
	Ice,
	Air,
	Earth,
	Holy,
	Dark	
}

public enum DamageUnit{
	Point,
	Percent
}

public enum DamagePointType{
	Hp,
	Mp,
	Ap
}

[Serializable]
public class ElementalDamage{
	public DamageType type;
	public int amount;
	public DamageUnit unit;
	public DamagePointType point;
	
	public ElementalDamage(DamageType type, int amount,DamageUnit unit, DamagePointType pt){
		this.type = type;
		this.amount = amount;
		this.unit = unit;
		this.point = pt;
	}
	
	public ElementalDamage(DamageType type, int amount, DamageUnit unit): this(type, amount, unit, DamagePointType.Hp){
	}
}
		
public enum Ailment{
	Poison, //take poison damage at start of turn
	Frozen, //take slight ice damage at start of turn, movement halved
	burning, //take fire damage at start of turn 
	Silence, //can't cast magic
	Blind, //very hard to physically hit target
	Sleep, //no turn
	Undead, //healing hurts
	Tangled // can't move
}

public class Damage{
	public ElementalDamage[] elementalDamages;
	public Ailment[] ailments;
	
	public Damage(ElementalDamage[] elements, Ailment[] ailments){
		this.elementalDamages = elements;
		this.ailments = ailments;
	}
}

