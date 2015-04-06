using UnityEngine;
using System.Collections;
using System;

namespace Events {
	public delegate void UnitSelectedEventHandler(object sender, UnitEventArgs e);
	public delegate void UnitUnSelectedEventHandler(object sender, UnitEventArgs e);
	public class UnitEvents {
		public static event UnitSelectedEventHandler UnitSelected;
		public static event UnitUnSelectedEventHandler UnitUnSelected;
		protected virtual void OnUnitSelected(UnitEventArgs e) {
			if (UnitSelected != null) {
				UnitSelected(this, e);
			}
		}

		protected virtual void OnUnitUnSelected(UnitEventArgs e) {
			if (UnitUnSelected != null) {
				UnitUnSelected(this, e);
			}
		}

		public void TriggerUnitSelected(GameObject unit) {
			OnUnitSelected(new UnitEventArgs(unit));
		}

		public void TriggerUnitUnSelected(GameObject unit) {
			OnUnitUnSelected(new UnitEventArgs(unit));
		}
	}
}

