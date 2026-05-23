using System.Reflection;

namespace Kozlova_Albina;

// 1
public static class TypeFactory
{
    public static object CreateAndFill(Type type, Dictionary<string, object> values)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(values);

       
        object instance = Activator.CreateInstance(type)
            ?? throw new InvalidOperationException($"Не получилось создать экземпляр типа {type.FullName}.");

        foreach (KeyValuePair<string, object> pair in values)
        {
            PropertyInfo? property = type.GetProperty(
                pair.Key,
                BindingFlags.Public | BindingFlags.Instance);

            
            if (property is null || !property.CanWrite)
                continue;

            object? value = pair.Value;

           
            if (value is not null && !property.PropertyType.IsInstanceOfType(value))
            {
                Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                value = Convert.ChangeType(value, targetType);
            }

            property.SetValue(instance, value);
        }

        return instance;
    }
}
