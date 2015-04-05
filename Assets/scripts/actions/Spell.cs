using UnityEngine;
using System.Collections.Generic;

public class Spell : Action {
	public int ApCost;
	private Character caster;
	
	public bool CanCast(Character character){
		if(character.ap > this.ApCost){
			return true;
		}
		return false;
	}
	
	public void Cast(Character player){
		if(this.CanCast(player)){
			Debug.Log("Casted " + this.gameObject.name);
			this.caster = player;
			this.Execute();
		}
	}
	
	public override void Execute(List<Hex> targets){
		//targets.Insert(0,this.caster.GetHex());
		base.Execute(targets);
	}
}
