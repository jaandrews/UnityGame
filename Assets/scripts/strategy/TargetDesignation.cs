using UnityEngine;
using System.Collections;
using System;

public class TargetDesignation{

}

public class TargetCondition{
}

public class CharacterTargetDesignation{
	public Func<string, bool>[] ctd;
	
	public CharacterTargetDesignation(){
		this.ctd = new Func<string, bool>[Enum.GetNames(typeof(CharacterTargetDesignationEnum)).Length];
		this.ctd[(int)CharacterTargetDesignationEnum.any] = any;
	}
	
	public bool any(string str){
		return str.Contains("Jason");
	}
	
}

public enum CharacterTargetDesignationEnum{
	any,
	high_priority,
	closest,
	closest_and_visible,
	flying,
	highest_elevation,
	lower_elevation,
	healer,
	strongest,
	highest_level
}

public class CharacterTargetCondition: TargetCondition{
	
}

public enum CharacterTargetConditionEnum{
	none,
	is_dead,
	is_alive,
	health_less_than_50_percent,
	health_is_critical,
	is_poisoned,
	mana_less_than_50_percent,
	is_caster
};

