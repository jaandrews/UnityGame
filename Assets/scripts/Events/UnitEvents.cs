using UnityEngine;
using System.Collections;
using System;

namespace Events {
	public delegate void UnitSelectedEventHandler(object sender, UnitEventArgs e);
	public delegate void UnitDeselectedEventHandler(object sender, UnitEventArgs e);
	public delegate void UnitTargetedEventHandler(object sender, UnitEventArgs e);
	public static class UnitEvents {
		public static event UnitSelectedEventHandler UnitSelected;
		public static event UnitDeselectedEventHandler UnitDeselected;
		public static event UnitTargetedEventHandler UnitTargeted;

		public static void TriggerUnitSelected(GameObject unit) {
			if (UnitSelected != null)
				UnitSelected(null, new UnitEventArgs(unit));
		}

		public static void TriggerUnitDeselected(GameObject unit) {
			if (UnitDeselected != null)
				UnitDeselected(null, new UnitEventArgs(unit));
		}

		public static void TriggerUnitTargeted(GameObject unit) {
			if (UnitTargeted != null)
				UnitTargeted(null, new UnitEventArgs(unit));
		}
	}
}

