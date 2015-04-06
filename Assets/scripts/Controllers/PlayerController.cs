using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Events;

public class PlayerController : UnitController {
	private CharacterModel _model;
	public bool active;

	void Start() {
		base.Start();
		_model = GetComponent<CharacterModel>();
	}
	
	void Update() {
		base.Update();
	}
	
	public override void HandleOnClick() {
		active = true;
	}

	public override void HandleOffClick() {
		active = false;
	}
}
