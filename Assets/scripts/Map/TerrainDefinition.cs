using UnityEngine;
using System.Collections;

public enum TerrainType {
	Dirt,
	Snow}//can't use class name Terrain used by unity

public class TerrainDefinition : MonoBehaviour {
	
	public TerrainType terrainType;//the Terrain Type this defines
	public GameObject terrainSideMesh;
	public GameObject terrainTopMesh;
	
	public bool isDestuctable = true;
	public bool isFlamable = false;
	public bool isSupportsLife = false;
	
	public int enterMovementCost;
	public int exitMovementCost;
	public float ascendMultiplier;
	public float descendMultiplier;
	
	public override string ToString(){
		return "[TerrainInfo] " + this.terrainType;
	}
}
