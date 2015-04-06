using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CombatController : MonoBehaviour {
	private IEnumerable<PlayerController> _units;
	private IEnumerable<EnemyController> _enemies;
	private List<GameObject> _allies;
	private MenuController _actionMenu;
	public GameObject units;
	public GameObject enemies;
	public GameObject allies;
	// Use this for initialization
	void Start () {
		_units = units.GetComponentsInChildren<PlayerController>();
		_enemies = units.GetComponentsInChildren<EnemyController>();
	}
	
	// Update is called once per frame
	void Update () {
		_units = units.GetComponentsInChildren<PlayerController>();
		_enemies = units.GetComponentsInChildren<EnemyController>();
	}
}
