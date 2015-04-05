using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Coordinate{
	public int i;
	public int j;
	
	public Coordinate(int i, int j){
		this.i = i;
		this.j = j;
	}
	
	public override bool Equals(object obj){
		return ((Coordinate)obj).i == this.i && ((Coordinate)obj).j == this.j;
	}
	
	public static bool operator ==(Coordinate a,  Coordinate b){
		if (System.Object.ReferenceEquals(a, b)){
	        return true;
	    }
	    if (((object)a == null) || ((object)b == null)){
	        return false;
	    }
		return a.Equals(b);
	}
	
	public static bool operator !=(Coordinate a,  Coordinate b){
		if (System.Object.ReferenceEquals(a, b)){
	        return true;
	    }
	    if (((object)a == null) || ((object)b == null)){
	        return false;
	    }
		return !(a==b);
	}
	
	
	public override string ToString(){
		return "[Coordinate] " + this.i + " " + this.j;  
	}
	
	public override int GetHashCode(){
		return this.ToString().GetHashCode();
	}
	
	//gets the coordinate connected to this coordinate in given direction
	public Coordinate GetNeighbor(Direction dir){
		Coordinate other = new Coordinate(this.i, this.j);
		bool on_lower_row = this.j%2 == 0;
		switch(dir){
			case Direction.North:
				other.i++;
				break;
			case Direction.Northeast:
				if(on_lower_row){
					other.j++;
				}
				else{
					other.i++;
					other.j++;
				}
				break;
			case Direction.Southeast:
				if(on_lower_row){
					other.i--;
					other.j++;
				}
				else{
					other.j++;
				}
				break;
			case Direction.South:
				other.i--;
				break;
			case Direction.Southwest:
				if(on_lower_row){
					other.i--;
					other.j--;
				}
				else{
					other.j--;
				}
				break;
			case Direction.Northwest:
				if(on_lower_row){
					other.j--;
				}
				else{
					other.i++;
					other.j--;
				}
				break;
		}
		return other;
	}
	
	public Coordinate[] GetNeighbors(){
		Coordinate[] result = new Coordinate[Enum.GetValues(typeof(Direction)).Length];
		foreach(int dirInt in Enum.GetValues(typeof(Direction))){
			result[dirInt] = this.GetNeighbor((Direction)dirInt);
		}
		return result;
	}
	
	
	//helper function for ShortestRoute
	private static List<Direction> ShortestRouteHelper(Coordinate startCoord, Coordinate endCoord,
			List<Direction> result, Dictionary<Direction, Coordinate> dir_coord_even,
			Dictionary<Direction, Coordinate> dir_coord_odd){
		if(endCoord != startCoord){	
			int displ_i = endCoord.i - startCoord.i;
			int displ_j = endCoord.j - startCoord.j;
			Direction resultDir = Direction.North;
			int shortestDist = int.MaxValue;
			Dictionary<Direction,Coordinate> toUse = (startCoord.j % 2 == 0) ? dir_coord_even : dir_coord_odd; 
			foreach(KeyValuePair<Direction, Coordinate> kv in toUse){
				int vect_i = displ_i - kv.Value.i;
				int vect_j = displ_j - kv.Value.j;
				int distSqrd = vect_i*vect_i + vect_j*vect_j;
				if(distSqrd < shortestDist){
					shortestDist = distSqrd;
					resultDir = kv.Key;
				}
			}
			result.Add(resultDir);
			Coordinate newStartCoord = new Coordinate(startCoord.i + toUse[resultDir].i,
				startCoord.j + toUse[resultDir].j);
			return ShortestRouteHelper(newStartCoord, endCoord, result, dir_coord_even, dir_coord_odd);
		}
		return result;
	}
	
	//returns the shortestRoute between the start and end coordinate
	public static Route ShortestRoute(Coordinate startCoord, Coordinate endCoord){
		List<Direction> result = new List<Direction>();
		Dictionary<Direction,Coordinate> dir_coord_even = new Dictionary<Direction, Coordinate>();
		dir_coord_even[Direction.North] = new Coordinate(1,0);
		dir_coord_even[Direction.Northeast] = new Coordinate(0,1);
		dir_coord_even[Direction.Southeast] = new Coordinate(-1,1);
		dir_coord_even[Direction.South] = new Coordinate(-1,0);
		dir_coord_even[Direction.Southwest] = new Coordinate(-1,-1);
		dir_coord_even[Direction.Northwest] = new Coordinate(0,-1);
		Dictionary<Direction,Coordinate> dir_coord_odd = new Dictionary<Direction, Coordinate>();
		dir_coord_odd[Direction.North] = new Coordinate(1,0);
		dir_coord_odd[Direction.Northeast] = new Coordinate(1,1);
		dir_coord_odd[Direction.Southeast] = new Coordinate(0,1);
		dir_coord_odd[Direction.South] = new Coordinate(-1,0);
		dir_coord_odd[Direction.Southwest] = new Coordinate(0,-1);
		dir_coord_odd[Direction.Northwest] = new Coordinate(1,-1);
		return new Route(ShortestRouteHelper(startCoord, endCoord, result, dir_coord_even, dir_coord_odd));
	}
	
	//return the coordinate from this if route was followed
	public Coordinate GetRelativeCoordinate(Route route){
		if(route.Length() > 0){
			List<Direction> dirs = route.GetDirections();
			Direction d = dirs[0];
			Coordinate neighbor = this.GetNeighbor(d);
			Route remaining = new Route(dirs.GetRange(1, dirs.Count - 1));
			return neighbor.GetRelativeCoordinate(remaining);
		}
		else{
			return this;
		}
	}
}
