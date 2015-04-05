using System.Collections.Generic;
using UnityEngine;

public class Hex{
	private List<Stratum> strata;
	public GameObject collider;
	private float height;
	private Coordinate coord;
	private GameObject masterGroup;//contains all objects for the hex, translate position only
	private GameObject renderGroup;//groups the hex renders together
	
	public Hex(Coordinate coord, List<Stratum> strata){
		//get strata
		this.coord = coord;
		this.strata = strata;
		//get height
		this.height = 0.0f;
		foreach(Stratum stratum in this.strata){
			this.height += stratum.thickness;
		}
		//make collider mesh
		Object gobj = GameObject.Instantiate(AssetDataBase.Instance.GetHexCollider());
		this.collider = (GameObject)gobj;
		
		//set initial properties of collider mesh
		this.collider.name = "hex_collider_" + coord.i+ "_" + coord.j ;
		Renderer[] renderers = this.collider.GetComponents<Renderer>();
		foreach(Renderer renderer in renderers) {
	        renderer.enabled = false;
	    }
		
		//create master transform
		string tempName = "Hex_"+ this.coord.i + "_" + this.coord.j; 
		this.masterGroup = new GameObject(tempName);
		
		//create render transform
		this.renderGroup = new GameObject(tempName + "_render");
		
		//basic parenting
		this.renderGroup.transform.parent = this.masterGroup.transform;
		this.collider.transform.parent = this.masterGroup.transform;
		
		//put hex collider in hex_collider layer
		this.collider.layer = GlobalVariables.HEX_COLLISION_LAYER;
		
		//update meshes
		this.Update();
	}
	
	public Hex(Coordinate coord):
		this(coord, 
		new List<Stratum>(new Stratum[]{ new Stratum(TerrainType.Snow, 1.0f)})){        
	}
	
	public Hex(HexData hd): this(hd.coordinate, hd.strata){
	}
	
	//refreshes the render of the hex
	public void Update(){
		//Debug.Log("hex update");
		//add geometery for each hex
		
		//remove all existing render terrain
		foreach(Transform child in this.renderGroup.transform){
			GameObject.Destroy(child.gameObject);
		}
		
		//side of the terrains
		float reachedHeight = 0.0f;//the height of this mesh I have added
		foreach(Stratum stratum in this.strata){
			TerrainDefinition def = stratum.terrainDefinition;
			float thickness = stratum.thickness;
			float added = 0.0f;//the amount of this stratum I have added
			GameObject sideMesh = def.terrainSideMesh;
			while(added < thickness){
				float toAdd = Utilities.ClampFloat(thickness - added, 0, 1);
				GameObject tempSideMesh = (GameObject)GameObject.Instantiate(sideMesh);
				tempSideMesh.transform.parent = this.renderGroup.transform;
				tempSideMesh.transform.localPosition = new Vector3(0,reachedHeight,0);
				tempSideMesh.transform.Rotate(new Vector3(0,60*(int)((Random.value*36f)%6),0));
				if(toAdd < 1.0){//make mesh appear smaller
					Renderer rend = (Renderer)(tempSideMesh.GetComponent<Renderer>());
					Material orig_clipping = AssetDataBase.Instance.GetClippingMaterial();
					Material[] mats = new Material[rend.materials.Length];
					for(int i = 0; i < rend.materials.Length; i++){
						Material clone_clipping = (Material)Material.Instantiate(orig_clipping);
						clone_clipping.CopyPropertiesFromMaterial(rend.materials[i]);   
						clone_clipping.SetFloat("_CutHeight", toAdd);
						mats[i] = clone_clipping;
						
					}
					rend.materials = mats;
					//tempSideMesh.transform.localScale = new Vector3(1,toAdd,1);
				}
				added += toAdd;
				reachedHeight += toAdd;
			}
		}
		//terrain tops
		TerrainDefinition topDef = this.strata[this.strata.Count-1].terrainDefinition;
		GameObject topMesh = (GameObject)GameObject.Instantiate(topDef.terrainTopMesh);
		topMesh.transform.parent = this.renderGroup.transform;
		topMesh.transform.localPosition = new Vector3(0, this.height,0);

		//make collider mesh height same as strat height
		this.collider.transform.localScale = new Vector3(1,this.height,1);
	}
	
	//get the total height of the object
	public float GetHeight(){
		return this.height;
	}
	
	//set the position of the hex
	public void SetPosition(Vector3 pos){
		this.masterGroup.transform.position = pos;
	}
	
	//returns the top most stratum
	public Stratum GetTopStratum(){
		return this.strata[this.strata.Count-1];
	}
	
	//Adds terrain to the top of the raising the height of the hex 
	public void AddTerrainToTop(TerrainType type, float amount){
		if(amount < 0){
			throw new System.Exception("Hex.AddTerrainToTop, Value given must be positive");
		}
		Stratum top = this.GetTopStratum();
		if(top.terrainDefinition.terrainType == type){
			top.thickness += amount;
		}
		else{
			this.strata.Add(new Stratum(type, amount));
		}
		this.height += amount;
		this.Update();
	}
	
	//adds terrain to the bottom raising the hex
	public void AddTerrainToBottom(TerrainType type, float amount){
		if(amount < 0){
			throw new System.Exception("Hex.AddTerrainToTop, Value given must be positive");
		}
		Stratum bottom = this.strata[0];
		if(bottom.terrainDefinition.terrainType == type){
			bottom.thickness += amount;
		}
		else{
			this.strata.Insert(0,new Stratum(type, amount));
		}
		this.height += amount;
		this.Update();
	}
	
	//removes terrain from the top lowering the height of the hex
	public void RemoveTerrainFromTop(float amountToRemove){
		float amountRemoved = 0.0f;
		while(amountToRemove > 0.0f){
			Stratum top = this.GetTopStratum();
			if(amountToRemove < top.thickness){
				amountRemoved += amountToRemove;
				top.thickness -= amountToRemove;
				amountToRemove = 0.0f;
			}
			else{
				//remove all but leave strata at zero thickness;
				if(this.strata.Count == 1){
					top.thickness = 0.0f;
					amountRemoved = this.height;
					amountToRemove = 0.0f;
				}
				//remove this strata and go other available strata
				else{
					this.strata.RemoveAt(this.strata.Count -1);
					amountRemoved += top.thickness;
					amountToRemove -= top.thickness;
				}
			}
		}
		this.height -= amountRemoved;
		this.Update();
	}
	
	//removes the bottom of the lowering the height of the hex
	public void RemoveTerrainFromBottom(float amountToRemove){
		float amountRemoved = 0.0f;
		while(amountToRemove > 0.0f){
			Stratum bottom = this.strata[0];
			if(amountToRemove < bottom.thickness){
				amountRemoved += amountToRemove;
				bottom.thickness -= amountToRemove;
				amountToRemove = 0.0f;
				
			}
			else{
				//remove all but leave strata at zero thickness;
				if(this.strata.Count == 1){
					bottom.thickness = 0.0f;
					amountRemoved = this.height;
					amountToRemove = 0.0f;
				}
				//remove this strata and go other available strata
				else{
					this.strata.RemoveAt(0);
					amountRemoved += bottom.thickness;
					amountToRemove -= bottom.thickness;
				}
			}
		}
		this.height -= amountRemoved;
		this.Update();
	}
	
	//return the world space position of the top center position
	public Vector3 GetTopPosition(){
		Vector3 top = this.collider.transform.position;
		top.y += this.height;
		return top;
	}
	
	//return the world space position of the bottom center position
	public Vector3 GetBottomPosition(){
		return this.masterGroup.transform.position;
	}
	
	public override string ToString ()
	{
		return string.Format ("[Hex] at coordinate" + this.coord);
	}
	
	public Coordinate GetCoordinate(){
		return this.coord;
	}
	
	public HexData GetHexData(){
		HexData result = new HexData();
		result.coordinate = this.coord;
		result.strata = this.strata;
		return result;
	}
	
	public int GetDistance(Direction dir){
		return 1;
	}
}
	