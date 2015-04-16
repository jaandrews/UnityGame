using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

namespace Actions {
	public static class ActionGenerator {
		public static Sequence Attack(GameObject source, GameObject target) {
			var attack = new Sequence();
			attack.Add(new DealDamageEffect(source, target));
			return attack;
		}

		public static MethodInfo GetAction(string name) {
			return typeof(ActionGenerator).GetMethod(name);
		}
	}
}

