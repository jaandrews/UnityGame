using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public enum ActionManagerState{
	Free,
	HexPicker
}

public class ActionManager : MonoBehaviour {
	//BM global vars
	private ActionManagerState currentState;
	private static ActionManager instance;
	private List<EffectTree> activeEffectTrees;
	//hex picker vars
	private List<Hex> hexPickerHexes;
	private HexPicker hexPicker;
	private int HexPickerTargetsLeft;
	
	//action vars
	private Action actionInQueue;
	
	
	
	public static ActionManager Instance{
		get{
			if(instance == null) return null;
			return instance;
		}
	}
	
	public void AddEffectTree(EffectTree effectTree){
		this.activeEffectTrees.Add(effectTree);
	}
	
	public void GetTargetsAndExecute(Action action){
		Debug.Log("action Manager, executing action");
		if(action.numTargets < 0){
			Debug.Log("action " + action.name + " has negative targets");
		}
		this.StartHexPicker(action.numTargets);
		this.actionInQueue = action;
	}
	
	//private Code
	public void Awake(){
		if (instance != null)
		{
			Destroy(instance);	
		}
		instance = this;
		
		this.currentState = ActionManagerState.Free;
		this.activeEffectTrees = new List<EffectTree>();
	}
	
	//set the current state of battle manager
	private void SetState(ActionManagerState state){
		//end current state
		switch(this.currentState){
		case ActionManagerState.Free:
			break;
		case ActionManagerState.HexPicker:
			this.EndHexPicker();
			break;
		}
		this.currentState = state;
	}
	
	//major update function
	public void Update(){
		//update all effectTrees
		List<EffectTree> nextActiveTrees = new List<EffectTree>();
		foreach(EffectTree tree in this.activeEffectTrees){
			bool finished = tree.Update(Time.deltaTime);
			if(!finished){
				nextActiveTrees.Add(tree);
			}
		}
		this.activeEffectTrees = nextActiveTrees;
		switch(this.currentState){
		case ActionManagerState.Free:
			break;
		case ActionManagerState.HexPicker:
			this.UpdateHexPicker();
			break;
		}	
	}
	
	//-----------hex picker code--------
	private void StartHexPicker(int numTargets){
		this.SetState(ActionManagerState.HexPicker);
		this.hexPickerHexes = new List<Hex>();
		this.HexPickerTargetsLeft = numTargets;
		GameObject gobj = new GameObject("HexPickerObject");
		HexPicker picker = gobj.AddComponent<HexPicker>();
		this.hexPicker = picker;
	}
	
	private void UpdateHexPicker(){
		if(this.HexPickerTargetsLeft > 0){
			if(Input.GetMouseButtonDown(0)){
				Hex hex = this.hexPicker.getHex();
				if(hex != null){
					this.hexPickerHexes.Add(hex);
					this.HexPickerTargetsLeft--;
				}
			}
		}
		else{
			if(this.actionInQueue != null){
				Debug.Log(this.hexPickerHexes);
				this.actionInQueue.Execute(this.hexPickerHexes);
			}
			this.SetState(ActionManagerState.Free);
		}
	}
	
	private void EndHexPicker(){
		//clear hex picker variables
		this.hexPickerHexes = new List<Hex>();
		this.HexPickerTargetsLeft = 0;
		this.hexPicker.Remove();
		//Destroy(this.hexPicker.gameObject);
		this.currentState = ActionManagerState.Free;
	}
	
	
	
}
