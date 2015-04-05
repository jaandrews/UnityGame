using UnityEngine;
using System.Collections;

public abstract class UnitController : MonoBehaviour
{
	private bool _clear;

	protected void Start() {
		_clear = false;
	}

	protected void Update() {
		if (_clear && Input.GetMouseButtonDown(0))
			HandleOffClick();
	}

	void OnMouseOver() {
		_clear = false;
		if (Input.GetMouseButtonDown(0))
			HandleOnClick();
	}

	void OnMouseExit() {
		_clear = true;
	}

	public abstract void HandleOnClick();
	public abstract void HandleOffClick();
}

