using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Attribute{
	public int currentValue;
	public int maxValue;
	//min value is always 0

	public Attribute(int max, int current){
		this.currentValue = current;
		this.maxValue = max;
	}	
}

[Serializable]
public class Stat{
	private int baseValue; //from class
	private int modifier; //from equipment
	
	public Stat(int baseValue){
		this.baseValue = baseValue;
		this.modifier = 0;
	}
	
	public void AddModifier(int amount){
		this.modifier += amount;
	}
	
	public int GetValue(){
		return this.baseValue + this.modifier;
	}
}

[Serializable]
public class DamageResistance{
	public DamageType damageType;//resistance from
	private float boost; //from in battle actions
	private float baseMultiplier; //from items, stats and class 
	
	public DamageResistance(DamageType dt, float baseMultiplier){
		this.damageType = dt;
		this.baseMultiplier = baseMultiplier;
		this.boost = 0;
	}
	
	/// <summary>
	/// Gets the multiplier for damage;
	/// 1.0f = regular, 0.0f = immune, -0.5 = take no damage, gain 50% as health
	/// </summary>
	/// <returns>
	/// The multiplier.
	/// </returns>
	/// 
	public float GetMultiplier(){
		return baseMultiplier + boost;	
	}
	
	/// <summary>
	/// Adds to boost. Can be + or -
	/// </summary>
	/// <param name='amount'>
	/// Amount to add
	/// </param>
	public void AddToBoost(float amount){
		this.boost += amount;
	}
	
	/// <summary>
	/// Adds to base multiplier. Can be + or -
	/// </summary>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	public void AddToBase(float amount){
		this.baseMultiplier += amount;
	}
}

public enum Passive{
	Waterwalk,
	SwaMPwalk,
	Lavawalk,
	Floating,
	Flying
}

public class Character: Actor{
	public int hp {get {return HP.currentValue;}}
	public int maxHp {get {return HP.maxValue;}}
	public int mp {get {return MP.currentValue;}}
	public int maxMp {get {return MP.maxValue;}}
	public int ap {get {return AP.currentValue;}}
	public int maxAp {get {return AP.maxValue;}}
	private Attribute HP;//health points
	private Attribute MP;//mana points
	private Attribute AP;//action points
	private Stat[] stats; //all character stats
	private DamageResistance[] resistances;//resistance to damage types
	private bool[] ailments; //ailments that the character has
	private bool[] immunities;//ailments the character can't get
	private Passive[] passives;//passive abilities
	
	public Character(){
		Debug.Log("character creation start");
		//setup attributes
		this.AP = new Attribute(10,10);
		this.HP = new Attribute(10,10);
		this.MP = new Attribute(3,3);
		
		//setup stats
		this.stats = new Stat[Enum.GetNames(typeof(CharacterStat)).Length];
		foreach(int statInt in Enum.GetValues(typeof(CharacterStat))){
			this.stats[statInt] = new Stat(3);
		}
		
		//setup resistances
		this.resistances = new DamageResistance[Enum.GetNames(typeof(DamageType)).Length];
		foreach(int damageInt in Enum.GetValues(typeof(DamageType))){
			this.resistances[damageInt] = new DamageResistance((DamageType)damageInt, 1.0f);
		}
		
		//setup ailments
		this.ailments = new bool[Enum.GetNames(typeof(Ailment)).Length];
		this.immunities = new bool[Enum.GetNames(typeof(Ailment)).Length];
		foreach(int ailmentInt in Enum.GetValues(typeof(Ailment))){
			this.ailments[ailmentInt] = false;
			this.immunities[ailmentInt] = false; 
		}
		Debug.Log("Character creation ended");
	}
	
	#region health
	/// <summary>
	/// Gain health.
	/// </summary>
	/// <returns>
	/// The health gained
	/// </returns>
	/// <param name='amount'>
	/// amount to gain
	/// </param>
	private int GainHealth(int amount){
		int maxGain = this.HP.maxValue - this.HP.currentValue;
		int toGain = amount < maxGain ? amount : maxGain;
		this.HP.currentValue += toGain;
		return toGain;
	}
	
	
	private int LoseHealth(int amount){
		int amountLost = this.HP.currentValue < amount ? this.HP.currentValue: amount; 
		this.HP.currentValue -= amountLost;
		return amountLost;
	}
	
	
	public bool IsDead(){
		return this.HP.currentValue == 0;
	}
	
	#endregion
	
	#region mana
	/// <summary>
	/// Gains the mana.
	/// </summary>
	/// <returns>
	/// The mana gained
	/// </returns>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	private int GainMana(int amount){
		int maxGain = this.MP.maxValue - this.MP.currentValue;
		int toGain = amount < maxGain ? amount : maxGain;
		this.MP.currentValue += toGain;
		return toGain;
	}
	
	/// <summary>
	/// Loses the mana.
	/// </summary>
	/// <returns>
	/// The mana lost
	/// </returns>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	private int LoseMana(int amount){
		int amountLost = this.MP.currentValue < amount ? this.MP.currentValue: amount; 
		this.MP.currentValue -= amountLost;
		return amountLost;
	}
	#endregion
	
	#region action points
	/// <summary>
	/// Gains the action points.
	/// </summary>
	/// <returns>
	/// The action points gained
	/// </returns>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	private int GainActionPoints(int amount){
		int maxGain = this.AP.maxValue - this.AP.currentValue;
		int toGain = amount < maxGain ? amount : maxGain;
		this.AP.currentValue += toGain;
		return toGain;
	}
	
	/// <summary>
	/// Lose action points.
	/// </summary>
	/// <returns>
	/// The action points lost 
	/// </returns>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	private int LoseActionPoints(int amount){
		int amountLost = this.AP.currentValue < amount ? this.AP.currentValue: amount; 
		this.AP.currentValue -= amountLost;
		return amountLost;
	}
	#endregion
	
	#region damage
	public void TakeDamage(Damage damage){
		
	}
	#endregion
	
	#region stats
	/// <summary>
	/// Gets the stat value.
	/// </summary>
	/// <returns>
	/// The stat value.
	/// </returns>
	/// <param name='stat'>
	/// Stat.
	/// </param>
	public int GetStatValue(CharacterStat stat){
		return this.stats[(int)stat].GetValue();
	}
	
	#endregion
	
	#region resistances
	/// <summary>
	/// Boosts the resistance by given amount. Can be + or - amount.
	/// from in battle spells and effects.
	/// </summary>
	/// <param name='damageType'>
	/// Damage type
	/// </param>
	/// <param name='amount'>
	/// Amount to boost
	/// </param>
	public void AddResistance(DamageType damageType, float amount){
		this.resistances[(int) damageType].AddToBoost(amount);
	}
	
	/// <summary>
	/// Gets the damage multiplier.
	/// </summary>
	/// <returns>
	/// The damage multiplier.
	/// </returns>
	/// <param name='damageType'>
	/// Damage type.
	/// </param>
	public float GetDamageMultiplier(DamageType damageType){
		return this.resistances[(int)damageType].GetMultiplier();
	}
	
	/// <summary>
	/// Adds the base resistance.
	/// from class, stats, and equipment
	/// </summary>
	/// <param name='damageType'>
	/// Damage type.
	/// </param>
	/// <param name='amount'>
	/// Amount.
	/// </param>
	private void AddBaseResistance(DamageType damageType, float amount){
		this.resistances[(int) damageType].AddToBase(amount);
	}
	#endregion
	
	#region ailments
	/// <summary>
	/// Determines whether this instance has ailment the specified ailment.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance has ailment the specified ailment; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='ailment'>
	/// ailment to test
	/// </param>
	public bool HasAilment(Ailment ailment){
		return this.ailments[(int)ailment];
	}
	
	/// <summary>
	/// Determines whether this instance has ailment immunity the specified ailment.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance has ailment immunity the specified ailment; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='ailment'>
	/// ailment to test
	/// </param>
	public bool HasAilmentImmunity(Ailment ailment){
		return this.immunities[(int)ailment];
	}
	
	/// <summary>
	/// Adds the ailment immunity.
	/// </summary>
	/// <param name='ailment'>
	/// Ailment.
	/// </param>
	public void AddAilmentImmunity(Ailment ailment){
		this.immunities[(int)ailment] = true;
	}
	
	/// <summary>
	/// Removes the ailment immunity.
	/// </summary>
	/// <param name='ailment'>
	/// Ailment.
	/// </param>
	public void RemoveAilmentImmunity(Ailment ailment){
		this.immunities[(int)ailment] = false;
	} 
	
	/// <summary>
	/// Trys to give the Character Ailment
	/// </summary>
	/// <returns>
	/// true if the ailment was successfully given, or already had ailment
	/// false if immune
	/// </returns>
	/// <param name='ailment'>
	/// ailment to give
	/// </param>
	public bool GiveAilment(Ailment ailment){
		bool result = false;
		if(!this.immunities[(int)ailment]){
			this.ailments[(int)ailment] = true;
			return true;
		}
		return result;
	}
	
	/// <summary>
	/// Cures the given ailment.
	/// </summary>
	/// <param name='ailment'>
	/// Ailment to remove
	/// </param>
	public void CureAilment(Ailment ailment){
		this.ailments[(int)ailment] = false;
	}
	
	/// <summary>
	/// Cures all ailments.
	/// </summary>
	public void CureAllAilments(){
		foreach(int ailmentInt in Enum.GetValues(typeof(Ailment))){
			this.ailments[ailmentInt] = false;
		}
	}
	#endregion 
		
	
}
