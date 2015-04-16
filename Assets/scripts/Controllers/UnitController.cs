using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Events;

public abstract class UnitController : MonoBehaviour
{
	private bool _clear;

	protected void Start() {
		_clear = false;
	}

	protected void Update() {
		if (_clear && !EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0)) {
			UnitEvents.TriggerUnitDeselected(gameObject);
			HandleOffClick();
		}
		if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(1))
			UnitEvents.TriggerUnitTargeted(gameObject);
	}

	void OnMouseOver() {
		if (!EventSystem.current.IsPointerOverGameObject()) {
			_clear = false;
			if (Input.GetMouseButtonDown(0)) {
				UnitEvents.TriggerUnitSelected(gameObject);
				HandleOnClick();
			}
		}
	}

	void OnMouseExit() {
		_clear = true;
	}

	public abstract void HandleOnClick();
	public abstract void HandleOffClick();
}

