using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : UnitController {
	private CharacterModel _model;
	public bool active;
	public bool dirty;

	void Start() {
		base.Start();
		active = false;
		dirty = false;
		_model = GetComponent<CharacterModel>();
	}
	
	void Update() {
		base.Update();
	}
	
	public override void HandleOnClick() {
		active = true;
		dirty = true;
	}

	public override void HandleOffClick() {
		active = false;
		dirty = true;
	}
}
