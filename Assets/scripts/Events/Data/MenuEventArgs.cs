using UnityEngine;
using System.Collections;
using System;
namespace Events {
	public class MenuEventArgs : EventArgs {
		public bool active { get; private set; }
		public GameObject menu { get; private set; }
		public MenuEventArgs(bool active, GameObject menu) {
			this.active = active;
			this.menu = menu;
		}
	}
}

