using UnityEngine;
using System.Collections.Generic;

//Patterns
//PatternDesign = enum which is easy to assign and find
//PatternDefinition = how the coordinates are placed, READ ONLY
//Pattern = the instance that can be rotated and moved and changed, 



public enum PatternDesign{
	Single,
	FrontAndBack,
	FrontThree,
	FilledTriangle
}

//stores coordinates relative to 0,0;
[System.Serializable]
public class PatternDefinition: MonoBehaviour{
	public PatternDesign design;
	public List<Coordinate> coordinates;
	//assumes definition is pointing north
	
	//constructors
	public PatternDefinition(List<Coordinate> coords){
		this.coordinates = coords;
	}
	
}
	
public class Pattern{
	//better to store routes than coordinates because of hex grid coordinates shift on odd rows
	public List<Route> routes;
	public Direction facing;
	
	public Pattern(PatternDefinition definition){
		List<Coordinate> coordinates = definition.coordinates;
		this.routes = new List<Route>();
		Coordinate origin = new Coordinate(0,0);
		foreach(Coordinate coord in coordinates){
			this.routes.Add(Coordinate.ShortestRoute(origin, coord));
		}
		this.facing = Direction.North;
	}
	
	//returns the number of relative coordinates stored in the pattern
	public int NumCoordinates(){
		return this.routes.Count;
	}
	
	//the coordinates in pattern relative to given coordinate
	public List<Coordinate> GetCoordinates(Coordinate coordinate){
		List<Coordinate> temp = new List<Coordinate>();
		foreach(Route route in this.routes){
			Coordinate tempCoord = coordinate.GetRelativeCoordinate(route);
			temp.Add(tempCoord);
		}
		return temp;
	}
	
	//rotate the pattern around the pattern's origin 60 degrees per rotation
	//if north is 12 O'Clock 
	public void RotateClockwise(int numRotations){
		List<Route> newRoutes = new List<Route>();
		foreach(Route route in this.routes){
			List<Direction> newDirections = new List<Direction>();
			foreach(Direction d in route.GetDirections()){
				newDirections.Add((Direction)(((int)d + numRotations)%6));
			}
			newRoutes.Add(new Route(newDirections));
		}
		this.routes = newRoutes;
		this.facing = (Direction)(((int)this.facing +numRotations)%6);
	}
	
	//rotate clockwise once
	public void RotateClockwise(){
		this.RotateClockwise(1);
	}
	
	//rotate counter clockwise once
	public void RotateCounterClockwise(){
		this.RotateClockwise(5);
	}
	
	//rotate counter clockwise a given number of times
	public void RotateCounterClockwise(int numRotations){
		this.RotateClockwise(6-numRotations);
	}
	
}
