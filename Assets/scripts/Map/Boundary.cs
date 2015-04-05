using System.Collections.Generic;
using System;

[Serializable]
public abstract class Boundary{
	
	//returns True if the given coordinate in inside the boundary
	public abstract bool IsInBounds(Coordinate coordinate);
	
	//returns all the coordinates that are considered in bounds
	public abstract List<Coordinate> GetCoordinates();
}

[Serializable]
public class SquareBoundary: Boundary{
	public int numRows;
	public int numColumns;
	
	public SquareBoundary(int rows, int columns){
		this.numRows = rows;
		this.numColumns = columns;
	}
	
	public override bool IsInBounds (Coordinate coordinate)
	{
		return (coordinate.i >= 0 && coordinate.i < numRows)
			&& (coordinate.j >= 0 && coordinate.j < numColumns);	
	}
	
	
	public override List<Coordinate> GetCoordinates()
	{
		List<Coordinate> coords = new List<Coordinate>();
		for(int r = 0; r < this.numRows; r++){
			for(int c = 0; c < this.numColumns; c++){
			coords.Add(new Coordinate(r, c));	
			}
		}
		return coords;
	}
}

public class CustomBoundary: Boundary{
	public List<Coordinate> coordinates;
	
	public CustomBoundary(List<Coordinate> coordinates){
		this.coordinates = coordinates;
	}
	
	public override bool IsInBounds (Coordinate coordinate)
	{
		bool result = false;
		foreach (Coordinate coord in this.coordinates){
			if(coord == coordinate){
				result = true;
			}
		}
		return result;
	}
	
	public override List<Coordinate> GetCoordinates ()
	{
		return this.coordinates;
	}
}