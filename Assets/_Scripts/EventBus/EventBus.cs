using System;
using System.Collections.Generic;

public class EventBus
{
    Dictionary<Type, Delegate> _allListeners = new();

    public void CallEvent(Event @event)
    {
        Type type = @event.GetType();

        if (_allListeners.TryGetValue(type, out Delegate action))
        {
            action?.DynamicInvoke(@event);
        }
    }

    public void Subscribe<T>(Action<T> action) where T : Event
    {
        Type type = typeof(T);

        if (_allListeners.ContainsKey(type))
        {
            _allListeners[type] = Delegate.Combine(_allListeners[type], action);
        }
        else
        {
            _allListeners[type] = action;
        }
    }

    public void Unsubscribe<T>(Action<T> action)
    {
        Type type = typeof(T);

        if (_allListeners.ContainsKey(type))
        {
            _allListeners[type] = Delegate.Remove(_allListeners[type], action);
        }
    }
}