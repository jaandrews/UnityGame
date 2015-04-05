using System.Collections.Generic;
using UnityEngine;

public class PatternEffect : Effect{
	public int target;
	public PatternDesign pattern;
	public int random;
	
	protected List<Hex> GetPatternHexes(List<Hex> targets){
		List<Hex> hexes = new List<Hex>();
		if(this.target >= targets.Count){
			Debug.Log("Target number needed out of range for PatternEffect " + this.name);
		}
		else{
			Hex targetHex = targets[this.target];
			Pattern pattern = AssetDataBase.Instance.GetPatternFromDesign(this.pattern);
			List<Coordinate> coordinates = pattern.GetCoordinates(targetHex.GetCoordinate());
			foreach(Coordinate coord in coordinates){
				hexes.Add(Map.Instance.GetHex(coord));
			}
		}
		return hexes;
	}
}
