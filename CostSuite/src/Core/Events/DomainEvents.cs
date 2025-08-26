using System;
using System.Collections.Generic;
using System.Linq;

namespace CostSuite.Core.Events;

/// <summary>
/// Very lightweight in-process event aggregator.
/// </summary>
public static class DomainEvents
{
    private static readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public static void Register<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (!_handlers.TryGetValue(type, out var list))
        {
            list = new List<Delegate>();
            _handlers[type] = list;
        }

        list.Add(handler);
    }

    public static void Raise<T>(T @event)
    {
        var type = typeof(T);
        if (_handlers.TryGetValue(type, out var list))
        {
            foreach (var handler in list.Cast<Action<T>>())
            {
                handler(@event);
            }
        }
    }

    public static void Clear() => _handlers.Clear();
}

