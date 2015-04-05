using UnityEngine;
using System.Collections.Generic;

public class Action: MonoBehaviour{
	public int numTargets;
	
	//return all the effects in this action
	public virtual List<Effect> GetEffects(){	
		List<Effect> effects = new List<Effect>(this.gameObject.GetComponents<Effect>());
		return effects;
	}
	
	//Return the number of effects in this Action.
	public virtual int GetNumEffects(){
        return this.gameObject.GetComponents<Effect>().Length;
	}
	
	public virtual void Execute(){
		ActionManager.Instance.GetTargetsAndExecute(this);
	}
	
	public virtual void Execute(List<Hex> targets){
		ActionManager.Instance.AddEffectTree(new EffectTree(this.GetEffects(), targets));
	}
	
	
	
}
