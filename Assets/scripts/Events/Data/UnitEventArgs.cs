using UnityEngine;
using System;
namespace Events {
	public class UnitEventArgs : EventArgs {
		public GameObject unit { get; private set; }
		public UnitEventArgs(GameObject unit) {
			this.unit = unit;
		}
	}
}

