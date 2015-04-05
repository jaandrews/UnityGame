using UnityEngine;
using System.Collections.Generic;

public class Plan {
	public TargetDesignation targetDesignation;
	public TargetCondition targetCondition;
}

public class SelfPlan: Plan{

}

public class FriendPlan: Plan{
	public CharacterTargetDesignationEnum designation;
	public CharacterTargetConditionEnum condition;
	
	public FriendPlan(CharacterTargetDesignationEnum ctd, CharacterTargetConditionEnum ctc){
		this.designation = ctd;
		this.condition = ctc;
	}
		
}

public class EnemyPlan: Plan{

}

public class TilePlan: Plan{
}