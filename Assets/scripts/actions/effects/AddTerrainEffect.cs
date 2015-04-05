using System.Collections.Generic;

public class AddTerrainEffect: PatternEffect{
	public TerrainType terrain;
	public float amount;
	public bool addToTop = true;
	public float unitsPerSecond=1;
	
	private List<Hex> hexes;
	private float amountRaised;
	
	public override void StartEffect(List<Hex> targets){
		this.hexes = this.GetPatternHexes(targets);
		this.amountRaised = 0;
		if(this.unitsPerSecond <= 0.0f){
			this.unitsPerSecond = .05f;
		}
	}
	
	public override bool UpdateEffect(float deltaTime){
		float amountToRaise = Utilities.ClampFloat(deltaTime * this.unitsPerSecond, 0, this.amount - this.amountRaised);
		foreach(Hex hex in this.hexes){
			if(addToTop){
				hex.AddTerrainToTop(this.terrain, amountToRaise);
			}
			else{
				hex.AddTerrainToBottom(this.terrain, amountToRaise);
			}
		}
		this.amountRaised += amountToRaise;
		return this.amountRaised == this.amount;
	}
	
	public override void EndEffect(){
		//do nothing, no cleanup required.
	}
}
