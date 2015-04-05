using UnityEngine;
using System.Collections.Generic;

public class HexPicker: MonoBehaviour{
	private List<GameObject> geometery;
	private List<Hex> hexes;
	private Hex hitHex;
	private Dictionary<GameObject,Hex> collider_hex;
	private Pattern pattern;
	
	void Awake () {
		this.pattern = AssetDataBase.Instance.GetPatternFromDesign(PatternDesign.Single);
		this.SetPattern(this.pattern);
		Map map = Map.Instance;
		this.collider_hex = new Dictionary<GameObject, Hex>();
		List<Hex> allHexes = map.GetHexes();
		foreach(Hex hex in allHexes){
			GameObject collider = hex.collider;
			this.collider_hex[collider] = hex;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Ray mouseRay = Utilities.mouseRay();
		RaycastHit hit;
		this.hexes = new List<Hex>();
		this.hitHex = null;
		if(Physics.Raycast( mouseRay, out hit, Mathf.Infinity, 1 << GlobalVariables.HEX_COLLISION_LAYER)){
			Hex hex = collider_hex[hit.collider.gameObject];
			//move the geo
			Coordinate hitCoord = hex.GetCoordinate();
			this.hitHex = Map.Instance.GetHex(hitCoord);
			List<Coordinate> coords = this.pattern.GetCoordinates(hitCoord);
			for(int i = 0; i < coords.Count; i++){
				Map map = Map.Instance;
				if(map.boundary.IsInBounds(coords[i])){
					Hex h = map.GetHex(coords[i]);
					this.hexes.Add(h);
					this.geometery[i].transform.position = h.GetTopPosition();		
					this.geometery[i].SetActive(true);
				}
				else{
					this.geometery[i].SetActive(false);
				}
			}
		}
		else{
			foreach(GameObject geo in this.geometery){
				geo.SetActive(false);
			}
		}
	}
	
	//return all the hexes that are valid in the pattern
	public List<Hex> GetHexes(){
		return this.hexes;
	}
	
	//the hex that was hit
	public Hex getHex(){ 
		return this.hitHex;
	}
	
	public Direction getHexDirection(){
		return this.pattern.facing;
	}
	
	//set the pattern for picking
	public void SetPattern(Pattern pattern){
		this.pattern = pattern;
		this.geometery = new List<GameObject>();
		for(int i = 0; i < this.pattern.NumCoordinates();i++){
			GameObject sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			this.geometery.Add(sph);
			sph.transform.localScale = new Vector3(.3f, .3f, .3f);
		}
	}
	
	//rotate the pattern clockwise
	public void RotateClockwise(){
		this.pattern.RotateClockwise();
	}
	
	//rotate the pattern counter clockwise
	public void RotateCounterClockwise(){
		this.pattern.RotateCounterClockwise();
	}
	
	//remove this object and picker object
	public void Remove(){
		foreach(GameObject go in this.geometery){
			Destroy(go);
		}
		Destroy(this.gameObject);
	}
	
}
