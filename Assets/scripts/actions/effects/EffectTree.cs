using UnityEngine;
using System.Collections.Generic;

public class EffectTree{
	private List<Effect>[] effectList;
	private int index;
	private List<Effect> toUpdate;
	private List<Hex> targets;
	
	public EffectTree(List<Effect>[] effectList, List<Hex> targets){
		this.effectList = effectList;
		this.index = 0;
		this.toUpdate = this.effectList[this.index];
		this.targets = targets;
		foreach(Effect eff in this.toUpdate){
			eff.StartEffect(this.targets);
		}
	}
	
	public EffectTree(List<Effect> effects, List<Hex> targets): this(GetEffectList(effects),targets){}
	
	private static List<Effect>[] GetEffectList(List<Effect> effects){
		//find all the order # and sort them
		List<int> orders = new List<int>();
		foreach(Effect effect in effects){
			if(!orders.Contains(effect.order)){
				orders.Add(effect.order);
			}
		}
		orders.Sort();
		//get all effects and sort them by effect.order
		List<Effect>[] effectList = new List<Effect>[orders.Count];
		foreach(Effect effect in effects){
			int order = effect.order;
			int index = orders.IndexOf(order);
			if(effectList[index] == null){
				effectList[index] = new List<Effect>();
			}	
			effectList[index].Add(effect);
		}
		return effectList;
	}
	
	public bool Update(float deltaTime){
		List<Effect> nextUpdate = new List<Effect>();
		foreach(Effect eff in this.toUpdate){
			bool finished = eff.UpdateEffect(deltaTime);
			if(!finished){
				nextUpdate.Add(eff);
			}
			else{
				eff.EndEffect();
			}
		}
		if(nextUpdate.Count == 0){
			this.index++;
			if(this.index < effectList.Length){
				nextUpdate = effectList[this.index];
				foreach(Effect eff in nextUpdate){
					eff.StartEffect(this.targets);
				}
			}
			else{
				return true;//finished
			}
		}
		this.toUpdate = nextUpdate;
		return false;
	}
}
