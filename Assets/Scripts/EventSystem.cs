using System;
using System.Collections.Generic;

public class EventSystem
{
	private Dictionary<string, Action> events = new Dictionary<string, Action>();

    public void On(string name, Action action)
    {
		if (events.ContainsKey(name))
			events[name] += action;
		else
			events.Add(name, action);
    }

    public void Unsubscribe(string eventName, Action action)
    {
		if (events.ContainsKey(eventName)) {
			events[eventName] -= action;
		}
    }

    public void Emit(string eventName)
    {
		if (events.ContainsKey(eventName)) {
			events[eventName]?.Invoke();
		}
    }
}
