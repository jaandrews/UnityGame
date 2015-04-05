using UnityEngine;
using System.Collections;

/// <summary>
/// Placeable. Can be placed on a hex
/// </summary>
public interface Placeable{
}

/// <summary>
/// Feature.Only one Feature per Hex
/// </summary>
public interface Feature : Placeable{
}

/// <summary>
/// Can take damage and be dead
/// </summary>
public interface Damageable : Placeable{
	void TakeDamage(Damage damage);
	int hp{get;}
	int maxHp{get;}
	bool IsDead();
}

/// <summary>
/// Something that can take actions and has a turn
/// </summary>
public abstract class Actor{//, Feature, Damageable{
	//strategies??
	//TakeAction()
	//getSpeed
	//TurnOrder?
}

