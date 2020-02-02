using System;
using System.Collections.Generic;

public static class GameEvents
{
	private static Dictionary<string, Action> events = new Dictionary<string, Action>();

	public static void Init() {
		events = new Dictionary<string, Action>();
	}

	public static void On(string name, Action action) {
		if (events.ContainsKey(name))
			events[name] += action;
		else
			events.Add(name, action);
	}

	public static void Unsubscribe(string eventName, Action action) {
		if (events.ContainsKey(eventName)) {
			events[eventName] -= action;
		}
	}

	public static void Emit(string eventName) {
		if (events.ContainsKey(eventName)) {
			events[eventName]?.Invoke();
		}
	}
}
