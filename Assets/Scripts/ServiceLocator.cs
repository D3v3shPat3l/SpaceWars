using System;
using System.Collections.Generic;

public static class ServiceLocator{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();
    public static void Register<T>(T service){
        services[typeof(T)] = service;
    }

    public static T Get<T>(){
        if (services.ContainsKey(typeof(T)))
            return (T)services[typeof(T)];
        else
            throw new Exception($"Service of type {typeof(T)} not found.");
    }

    public static void Unregister<T>(){
        if (services.ContainsKey(typeof(T))){
            services.Remove(typeof(T));
        }
    }

    public static bool HasService<T>(){
    return services.ContainsKey(typeof(T));
    }
}
