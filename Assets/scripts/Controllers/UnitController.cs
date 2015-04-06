using UnityEngine;
using System.Collections;
using Events;

public abstract class UnitController : MonoBehaviour
{
	protected UnitEvents _unitEvents;
	private bool _clear;

	protected void Start() {
		_unitEvents = new UnitEvents();
		_clear = false;
	}

	protected void Update() {
		if (_clear && Input.GetMouseButtonDown(0)) {
			_unitEvents.TriggerUnitUnSelected(gameObject);
			HandleOffClick();
		}
	}

	void OnMouseOver() {
		_clear = false;
		if (Input.GetMouseButtonDown(0)) {
			_unitEvents.TriggerUnitSelected(gameObject);
			HandleOnClick();
		}
	}

	void OnMouseExit() {
		_clear = true;
	}

	public abstract void HandleOnClick();
	public abstract void HandleOffClick();
}

