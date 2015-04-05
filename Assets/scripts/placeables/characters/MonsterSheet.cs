using UnityEngine;
using System.Collections;
using System;

public class MonsterSheet: CharacterSheet{
	public ItemDrop[] dropItems;
	public IntRange goldDrop;
	public Strategy strategy;
	public int experienceGain;
}
