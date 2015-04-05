using System.Collections.Generic;
using UnityEngine;
using System;
//stratum of a hex

[Serializable]
public class Stratum {
	public float thickness; // the height of the stratum
	public TerrainDefinition terrainDefinition;// the contents of the stratum
	
	public Stratum(TerrainType terrainType, float thickness){
		this.thickness = thickness;
		this.terrainDefinition = AssetDataBase.Instance.GetTerrainDefinition(terrainType);
	}
	
	public Stratum(TerrainDefinition terrainDefinition, float thickness){
		this.thickness = thickness;
		this.terrainDefinition = terrainDefinition;
	}
}
