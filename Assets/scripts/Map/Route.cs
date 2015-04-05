using System.Collections.Generic;

[System.Serializable]
public class Route{
	private List<Direction> directions;
	
	public Route(List<Direction> directions){
		this.directions = directions;
	}
	
	public List<Direction> GetDirections(){
		return this.directions;
	}
	
	//number of directions in path
	public int Length(){
		return directions.Count;
	}
	
	// function to reverse the route inline
	public void Reverse(){
		List<Direction> revRoute = new List<Direction>();
		foreach(Direction dir in this.directions){
			Direction revDir = (Direction)(((int)dir + 3) % 6);
			revRoute.Insert(0, revDir);
		}
		this.directions = revRoute;
	}
}
