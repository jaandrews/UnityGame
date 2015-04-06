using UnityEngine;
using System.Collections;
using Events;

public class MenuController : MonoBehaviour {
	private GameObject _currentUnit;
	// Use this for initialization
	void Start () {
		UnitEvents.UnitSelected += new UnitSelectedEventHandler(UpdateOptions);
		UnitEvents.UnitUnSelected += new UnitUnSelectedEventHandler(ClearOptions);
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void UpdateOptions(object sender, UnitEventArgs e) {
		_currentUnit = e.unit;
		gameObject.SetActive(true);
	}

	private void ClearOptions(object sender, UnitEventArgs e) {
		if (e.unit != null && _currentUnit.gameObject == e.unit.gameObject) {
			_currentUnit = null;
			gameObject.SetActive(false);
		}
	}
}

