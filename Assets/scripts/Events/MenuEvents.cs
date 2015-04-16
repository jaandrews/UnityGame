using UnityEngine;
using System.Collections;
namespace Events {
	public delegate void MenuTargetedEventHandler(object sender, MenuEventArgs e);
	public static class MenuEvents {
		public static event MenuTargetedEventHandler MenuTargeted;

		public static void TriggerMenuTargeted(bool state, GameObject menu) {
			if (MenuTargeted != null)
				MenuTargeted(null, new MenuEventArgs(state, menu));
		}
	}
}

