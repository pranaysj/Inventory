using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> services = new();

    public static void Register<T>(T service)
    {
        var type = typeof(T);
        if (services.ContainsKey(type))
            throw new InvalidOperationException($"Service of type {type} is already registered.");

        services[type] = service;
    }

    public static T Get<T>()
    {
        var type = typeof(T);
        if (services.TryGetValue(type, out var service))
            return (T)service;

        throw new KeyNotFoundException($"Service of type {type} is not registered.");
    }

    public static void Unregister<T>()
    {
        var type = typeof(T);
        if (!services.Remove(type))
            throw new KeyNotFoundException($"Service of type {type} was not found to unregister.");
    }

    public static void Clean()
    {
        services.Clear();
    }
}