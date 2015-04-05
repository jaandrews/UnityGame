using UnityEngine;
using System.Collections.Generic;

public class Effect : MonoBehaviour {
	public int order;
	
	public virtual void StartEffect(List<Hex> targets){
	}
	
	public virtual bool UpdateEffect(float deltaTime){//return True when Done
		return true;
	}
	
	public virtual void EndEffect(){
		
	}
}
