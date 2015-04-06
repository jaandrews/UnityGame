using UnityEngine;
using System.Collections;
namespace Events {
	public interface IEvent {
		void Trigger<T>(T data);
	}
}

