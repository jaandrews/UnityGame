using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Actions {
	public class Sequence : IAction {
		private List<IAction> _actions;

		public Sequence() {
			_actions = new List<IAction>();
		}

		public void Add(IAction action) {
			_actions.Add(action);
		}

		public bool IsActionable() {
			foreach (var action in _actions) {
				if (!action.IsActionable())
					return false;
			}
			return true;
		}

		public void Execute() {
			if (IsActionable()) {
				foreach (var action in _actions) {
					action.Execute();
				}
			}
		}
	}
}

