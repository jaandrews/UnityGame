using UnityEngine;
using System.Collections;

public enum EquipmentType{
	Wand,
	Staff,
	Book,
	Sword,
	Axe,
	Omen,
	Bow
}

public enum EquipmentSlot{
	Head,
	Chest,
	Legs,
	RightHand,
	LeftHand
}

public class Equipment: MonoBehaviour{
	public string displayName;
	public EquipmentSlot slot;
	public int requiredLevel;
	public CharacterModifier characterModifier;
}


