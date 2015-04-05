using System.Collections.Generic;

public class RemoveTerrainEffect: PatternEffect{
	public float amount;
	public bool removeFromTop = true;
	public float unitsPerSecond =1;
	
	private List<Hex> hexes;
	private float amountRemoved;
	
	public override void StartEffect(List<Hex> targets){
		this.hexes = this.GetPatternHexes(targets);
		this.amountRemoved = 0;
		if(this.unitsPerSecond <= 0.0f){
			this.unitsPerSecond = .05f;
		}
	}
	
	public override bool UpdateEffect(float deltaTime){
		float amountToRemove = Utilities.ClampFloat(deltaTime * this.unitsPerSecond, 0, this.amount - this.amountRemoved);
		bool has_terrain = false;
		foreach(Hex hex in this.hexes){
			if(removeFromTop){
				 hex.RemoveTerrainFromTop(amountToRemove);
			}
			else{
				hex.RemoveTerrainFromBottom(amountToRemove);
			}
			if(hex.GetHeight() > 0.0f){
				has_terrain = true;
			}
		}
		this.amountRemoved += amountToRemove;
		bool finished = this.amountRemoved == this.amount;
		if(!finished){
			if(!has_terrain){
				finished = true;
			}
		}
		return finished;
	}
	
	public override void EndEffect(){
		//do nothing, no cleanup required.
	}
}

