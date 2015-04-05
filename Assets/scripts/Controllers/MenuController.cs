using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	private GameObject _activeUnit;
	private bool _dirty;

	// Use this for initialization
	void Start () {
		if (_activeUnit == null)
			gameObject.SetActive(false);
		_dirty = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (_dirty);
		if (_dirty)
			gameObject.SetActive(_activeUnit != null);
	}

	public void SetActiveUnit(GameObject unit) {
		_activeUnit = unit;
		_dirty = true;
	}
}

