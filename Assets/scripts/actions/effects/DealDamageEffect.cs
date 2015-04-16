using UnityEngine;
using System.Collections;

public class DealDamageEffect : IAction {
	private int _minDamage;
	private int _maxDamage;
	public GameObject source;
	public GameObject target;

	/// <summary>
	/// Initializes a new instance of the <see cref="DealDamageEffect"/> class.
	/// </summary>
	/// <param name="minDamage">Minimum Damage the attack will do.</param>
	/// <param name="maxDamage">Maximum damage the attack will do.</param>
	/// <param name="target">The unit receiving damage.</param>
	public DealDamageEffect(GameObject source, GameObject target) {
		this.source = source;
		this.target = target;
	}

	public bool IsActionable() {
		return source != null && target != null;
	}

	public void Execute() {
		target.GetComponent<CharacterModel>().HP -= 2;
	}
}

