using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Events;
using Actions;
using System;
using System.Reflection;

public class ActionMenuController : MenuController {
	private GameObject _currentUnit;
	private MethodInfo _currentAction;
	private IEnumerable<ActionModel> _actions;
	private Transform _buttons;
	private UnityEngine.Object _buttonPrefab;
	// Use this for initialization
	void Start () {
		UnitEvents.UnitSelected += new UnitSelectedEventHandler(UpdateOptions);
		UnitEvents.UnitDeselected += new UnitDeselectedEventHandler(ClearOptions);
		UnitEvents.UnitTargeted += new UnitTargetedEventHandler(ExecuteAction);
		_actions = new List<ActionModel> {
			new ActionModel {
				Name = "Attack"
			},
			new ActionModel {
				Name = "Mug"
			},
			new ActionModel {
				Name = "Fireball"
			}
		};
		_buttons = transform.FindChild("Container/Buttons");
		_buttonPrefab = Resources.Load("prefabs/general/button");
		gameObject.SetActive(false);
	}

	public void SelectAction(GameObject button) {
		_currentAction = ActionGenerator.GetAction(button.name);
	}

	private void UpdateOptions(object sender, UnitEventArgs e) {
		_currentUnit = e.unit;
		foreach (var action in _actions) {
			GameObject button = (GameObject)Instantiate(_buttonPrefab);
			button.name = action.Name;
			button.transform.GetChild(0).GetComponent<Text>().text = action.Name;
			button.transform.SetParent(_buttons);
			button.transform.localPosition = Vector3.zero;
			button.GetComponent<Button>().onClick.AddListener(() => SelectAction(button));
		}
		gameObject.SetActive(true);
	}

	private void ClearOptions(object sender, UnitEventArgs e) {
		if (e == null || (_currentUnit != null && _currentUnit.gameObject == e.unit.gameObject)) {
			_currentUnit = null;
			gameObject.SetActive(false);
			RemoveButtons();
		}
	}

	private void RemoveButtons() {
		for (int i=_buttons.childCount-1; i>=0; i--)
			Destroy(_buttons.GetChild(i));
	}

	private void ExecuteAction(object sender, UnitEventArgs e) {
		var action = (Sequence)_currentAction.Invoke(null, new object[] {
			_currentUnit,
			e.unit
		});
		action.Execute();
	}
}

