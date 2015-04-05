using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class TestDeleteMe : MonoBehaviour {
	public PatternDesign design;
	private Pattern pattern;
	public Spell spell;
	
	// Use this for initialization
	void Start () {
		this.pattern = AssetDataBase.Instance.GetPatternFromDesign(this.design);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("k")){
			//Boundary bounds = new SquareBoundary(16,16);
			List<Coordinate> clist = new List<Coordinate>();
			clist.Add(new Coordinate(1,5));
			clist.Add(new Coordinate(3,6));
			clist.Add(new Coordinate(3,7));
			clist.Add(new Coordinate(2,5));
			clist.Add(new Coordinate(1,3));
			clist.Add(new Coordinate(1,4));
			clist.Add(new Coordinate(5,9));
			Boundary bounds = new CustomBoundary(clist);
			
			Map map = new Map(bounds);
			GameObject gobj = new GameObject("ActionManagerObj");
			ActionManager bm = gobj.AddComponent<ActionManager>();
			map.CalculateMovement(new Coordinate(1,5),2);
		}
		if(Input.GetKeyDown("j")){
			Debug.Log("Cast");
			this.spell.Cast(new Character());
		}
		
		if(Input.GetKeyDown("h")){
			Utilities.CreateText("35!", new Vector3(0f,0f,0f));
		}
		
		if(Input.GetKeyDown("p")){
			SavedMap sm = AssetDataBase.Instance.GetSavedMap("firstTest");
			Map map = new Map(sm);
		}
		
		if(Input.GetKeyDown("l")){
			Map.Instance.SaveMap("firstTest");
		}
		
		
		
		
	}
}
