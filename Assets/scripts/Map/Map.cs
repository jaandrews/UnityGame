using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

//singleton use map.Instance, can override map as needed.
public class Map{
	//public Direction windDirection;
	//public float windMagnitude;
	private Dictionary<Coordinate, Hex> coordinate_hex;
	public Boundary boundary;
	private static Map mapInstance;
	
	public Map(Boundary boundary, List<Hex> hexes){
		this.boundary = boundary;
		this.coordinate_hex = new Dictionary<Coordinate, Hex>();
		foreach(Hex hex in hexes){
			if(this.boundary.IsInBounds(hex.GetCoordinate())){
				this.coordinate_hex[hex.GetCoordinate()] = hex;
			}
			else{
				Debug.Log("Map constructor: didn't add hex at coordinate " + hex.GetCoordinate().ToString() + " because it wasn't in bounds");
			}
		}
		foreach(Coordinate coord in this.boundary.GetCoordinates()){
			if(!this.coordinate_hex.ContainsKey(coord)){
				Hex newHex = new Hex(coord);
				this.coordinate_hex[coord] = newHex;
			}
		}
		this.Build();
		mapInstance = this;
	}
	
	public Map(Boundary boundary): this(boundary, new List<Hex>()){
	}
	
	public Map(SavedMap saved): this(new CustomBoundary(saved.mapdata.hexes.Select(h => h.coordinate).ToList<Coordinate>()), saved.mapdata.hexes.Select(h => new Hex(h)).ToList()){		
	}
	
	public void SaveMap(string name){
		GameObject gobj = new GameObject();
		SavedMap sm = gobj.AddComponent<SavedMap>();
		MapData md = this.GetMapData();
		sm.mapdata = md;
		var prefab = EditorUtility.CreateEmptyPrefab("Assets/Resources/maps/"+ name + ".prefab");
		EditorUtility.ReplacePrefab(gobj, prefab);
		GameObject.Destroy(gobj);
		
		//AssetDatabase.Refresh();			
	}
	
	
	private void Build(){
		List<Coordinate> coordinates = this.boundary.GetCoordinates();
		foreach(Coordinate coord in coordinates){
			Hex hex = this.GetHex(coord);
			Vector3 pos = new Vector3(0f,0f,0f);
			pos.z += coord.i * 2 * GlobalVariables.HEX_WIDTH;
			if(coord.j % 2 == 1){
				pos.z += GlobalVariables.HEX_WIDTH;
			}
			pos.x += coord.j * GlobalVariables.HEX_RADIUS * 1.5f;
			hex.SetPosition(pos);
		}
	}
	
	/// <summary>
	/// singleton get
	/// </summary>
	/// <value>
	/// The instance.
	/// </value>
	public static Map Instance{
		get{
			if(mapInstance == null) return null;
			return mapInstance;
		}
	}
	
	public MapData GetMapData(){
		MapData result = new MapData();
		result.hexes = this.GetHexes().Select(h => h.GetHexData()).ToArray();
		return result;
	}
	
	/// <summary>
	/// returns all the hexes inside the boundary of the map
	/// </summary>
	/// <returns>
	/// The hexes.
	/// </returns>
	public List<Hex> GetHexes(){
		Hex[] array = new Hex[coordinate_hex.Count];
		coordinate_hex.Values.CopyTo(array,0);
		List<Hex> list = new List<Hex >(array); 
		return list;
	}
	
	/// <summary>
	/// returns the hex at the given coordinate
	/// </summary>
	/// <returns>
	/// The hex.
	/// </returns>
	/// <param name='coord'>
	/// Coordinate.
	/// </param>
	public Hex GetHex(Coordinate coord){
		Hex result = null;
		if(this.boundary.IsInBounds(coord)){
			result = this.coordinate_hex[coord];
		}
		return result;
	}
	
	#region calculate movement
	public void CalculateMovement(Coordinate startCoord, int maxValue = int.MaxValue){
		Dictionary<Coordinate, MovementResult> movement = new Dictionary<Coordinate, MovementResult>();
		foreach(Coordinate coord in this.coordinate_hex.Keys){
			movement[coord] = new MovementResult(coord, maxValue);
		}
		movement[startCoord].distance = 0;
		List<MovementResult> done = CalculateMovementHelper(movement, new List<MovementResult>(), maxValue);
		foreach(MovementResult res in done){
			Vector3 top_position = this.coordinate_hex[res.thisCoord].GetTopPosition();
			top_position.y += 3;
			Utilities.CreateText(res.distance.ToString(), top_position); 
		}
	}
	
	private class MovementResult{
		public int distance;
		public Coordinate fromCoord;
		public Coordinate thisCoord;
		
		public MovementResult(Coordinate thisCoord, int maxValue = int.MaxValue){
			this.distance = int.MaxValue;
			this.fromCoord = null;
			this.thisCoord = thisCoord;
		}
	}
	
	private List<MovementResult> CalculateMovementHelper(Dictionary<Coordinate,MovementResult> iter, List<MovementResult> done, int maxValue){
		//find smallest
		MovementResult smallest = null;
		int smallestValue = maxValue;
		foreach(MovementResult mr in iter.Values){
			if(mr.distance < smallestValue){
				smallest = mr;
				smallestValue = mr.distance;
			}
		}
		if (iter.Count > 0 && smallestValue < maxValue){
			//move smallest to done list;
			iter.Remove(smallest.thisCoord);
			done.Add(smallest);
			//update neighbor list if still in iter list
			foreach(int dirInt in Enum.GetValues(typeof(Direction))){
				Direction dir = (Direction)dirInt;
				Coordinate neighborCoord = smallest.thisCoord.GetNeighbor(dir);
				if(this.boundary.IsInBounds(neighborCoord) && iter.ContainsKey(neighborCoord)){
					int newDistance = smallest.distance + this.coordinate_hex[neighborCoord].GetDistance(dir);
					if(newDistance < iter[neighborCoord].distance){
						iter[neighborCoord].distance = newDistance;
						iter[neighborCoord].fromCoord = smallest.thisCoord;
					}
				}
			}
			return CalculateMovementHelper(iter, done, maxValue);
		}
		else{
			return done;
		}
	}
	#endregion
	
	
}
