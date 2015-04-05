using System.Collections.Generic;
using UnityEngine;

//singleton class which holds information about the assets
public class AssetDataBase {
	//terrian assets
	private Dictionary<TerrainType, TerrainDefinition> terrainType_terrainDefinition;
	private Dictionary<PatternDesign, PatternDefinition> patternDesign_patternDefinition;
	private Dictionary<string, SavedMap> savedMaps;
	private GameObject hexCollider;
	private Material clippingMaterial;
	
	//singleton instance stored
	private static AssetDataBase instance;
	
	//singleton constructor
	private AssetDataBase(){
		this.LoadResources();
	}
	
	//singletone .Instance command
	public static AssetDataBase Instance{
		get{
			if (instance == null){
				instance = new AssetDataBase();
			}
			return instance;
		}
	}
	
	//LOADING CODE
	
	//loads all resources
	private void LoadResources(){
		this.LoadTerrainInfo();
		this.LoadHexCollider();
		this.LoadClippingMaterial();
		this.LoadPatternDefinitions();
		this.LoadSavedMaps();
	}
	
	//TERRAIN Code
	
	//loads all terrain info resources
	private void LoadTerrainInfo(){
		terrainType_terrainDefinition = new Dictionary<TerrainType, TerrainDefinition>();
		
		Object[] terrainDefinitions = Resources.LoadAll("terrain_types", typeof(GameObject));
		foreach (Object obj in terrainDefinitions) {
			GameObject gobj = (GameObject)obj;
			TerrainDefinition terrainDefinition = gobj.GetComponent<TerrainDefinition>();
			
			if (terrainDefinition == null) {
				Debug.Log("ERROR: "+gobj.name+" has not TerrainDefinition component attached, skipping.");
				continue;
			}
			
			// terrain info
			TerrainType type = terrainDefinition.terrainType;
			
			if (terrainType_terrainDefinition.ContainsKey(type)) {
				Debug.Log("WARNING: "+type+" already defined.  Overwriting existing definition.");	
			}
			
			terrainType_terrainDefinition[type] = terrainDefinition;
		}
	}
	
	//HEX code
	
	private void LoadHexCollider(){
		Object collider = Resources.Load("hex_collider", typeof(GameObject));
		
		if( collider == null){
			Debug.Log("ERROR: no hex_collider found in resource directory, collider not loaded, skipping.");
			return;
		}
		
		GameObject mesh_collider = (GameObject)collider;
		this.hexCollider = mesh_collider;

	}
	
	//clipping materials
	private void LoadClippingMaterial(){
		Object material = Resources.Load("clip", typeof(Material));
		
		if(material == null){
			Debug.Log("ERROR: no clipping material found in resource directory");
			return;
		}
		
		Material mat = (Material)material;
		this.clippingMaterial = mat;
	}
	
	//Pattern Code
	
	private void LoadPatternDefinitions(){
		this.patternDesign_patternDefinition = new Dictionary<PatternDesign, PatternDefinition> ();
		
		Object[] PatternDefinitions = Resources.LoadAll("patterns", typeof(GameObject));
		foreach (Object obj in PatternDefinitions){
			GameObject gobj = (GameObject)obj;
			PatternDefinition def = gobj.GetComponent<PatternDefinition>();
			
			if (def == null) {
				Debug.Log("ERROR: "+gobj.name+" has not PatternDefinition component attached, skipping.");
				continue;
			}
			
			// Pattern definition
			PatternDesign design = def.design;
			
			if (this.patternDesign_patternDefinition.ContainsKey(design)) {
				Debug.Log("WARNING: "+design+" already defined.  Overwriting existing definition.");	
			}
			
			this.patternDesign_patternDefinition[design] = def;
		}
	}
	
	private void LoadSavedMaps(){
		this.savedMaps = new Dictionary<string, SavedMap>();
		
		Object[] resourceMaps = Resources.LoadAll("maps", typeof(GameObject));
		foreach (Object obj in resourceMaps){
			GameObject gobj = (GameObject)obj;
			SavedMap map = gobj.GetComponent<SavedMap>();
			
			if (map == null){
				Debug.Log("ERROR: "+gobj.name+" has not SavedMap component attached, skipping.");
				continue;
			}
			
			string mapName = gobj.name;
			
			if (this.savedMaps.ContainsKey(mapName)) {
				Debug.Log("WARNING: "+mapName+" already defined.  Overwriting existing map.");	
			}
			
			this.savedMaps[mapName] = map;
		}
	}
	
	
	///GETTERS
	
	//returns the PatternDefinition for the given Pattern
	public SavedMap GetSavedMap(string mapName){
		if (!this.savedMaps.ContainsKey(mapName)) return null;
		return this.savedMaps[mapName];
	}
	
	//get the matching terrain Info for the type
	public TerrainDefinition GetTerrainDefinition(TerrainType type){
		if (!this.terrainType_terrainDefinition.ContainsKey(type)) return null;
		return this.terrainType_terrainDefinition[type];
		
	}
	
	//returns the hex collider prefab
	public GameObject GetHexCollider(){
		return this.hexCollider;
	}
	
	//returns the PatternDefinition for the given Pattern
	public Pattern GetPatternFromDesign(PatternDesign design){
		if (!this.patternDesign_patternDefinition.ContainsKey(design)) return null;
		PatternDefinition def = this.patternDesign_patternDefinition[design];
		return new Pattern(def);
	}
	
	public Material GetClippingMaterial(){
		return this.clippingMaterial;
	}
	
}
