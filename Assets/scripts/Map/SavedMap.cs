using UnityEngine;
using System.Collections.Generic;
using System;

public class SavedMap: MonoBehaviour{
	public MapData mapdata;
}

[Serializable]
public class MapData{
	public HexData[] hexes;
}

[Serializable]
public class HexData{
	public Coordinate coordinate;
	public List<Stratum> strata;
}


