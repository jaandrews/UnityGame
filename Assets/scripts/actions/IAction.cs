using UnityEngine;
using System.Collections;

public interface IAction {
	/// <summary>
	/// Check to see whether the action has all of the data it needs to run.
	/// </summary>
	bool IsActionable();
	/// <summary>
	/// Runs the action.
	/// </summary>
	void Execute();
}

