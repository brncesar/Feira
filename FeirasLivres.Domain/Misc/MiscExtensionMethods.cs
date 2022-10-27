using System.ComponentModel;
using System.Reflection;

namespace FeirasLivres.Domain.Misc;

public static class MiscExtensionMethods
{
    #region Enum
    public static string ToDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute == null ? value.ToString() : attribute.Description;
    }
    #endregion

    #region String
    public static bool IsNotNullOrNotEmpty(this string? input)
    {
        return !string.IsNullOrEmpty(input);
    }

    public static bool IsNullOrEmpty(this string? input)
    {
        return string.IsNullOrEmpty(input);
    }

    public static string IfIsNullOrEmptyThen(this string? input, string newValue)
    {
        return string.IsNullOrEmpty(input)
            ? newValue
            : input;
    }
    #endregion

    #region Enumerable
    public static bool None<TSource>(this IEnumerable<TSource> source)
    {
        return !source.Any();
    }
    public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        return !source.Any(predicate);
    }
    #endregion

    #region Object properties map
    private static void TrySetSourcePropertyValueOnTargetObject<TTarget>(object source, PropertyInfo sourceProperty, TTarget targetObject, PropertyInfo targetProperty, bool ignoreNullValues = false)
    {
        var sourceType = source.GetType();
        var targetType = typeof(TTarget);

        if (ignoreNullValues && source.GetType().GetProperty(sourceProperty.Name).GetValue(source, null) is null) return;

        var isSourcePropertyNullable = sourceProperty.PropertyType.IsGenericType;
        var isTargetPropertyNullable = targetProperty.PropertyType.IsGenericType && targetProperty.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

        if (isSourcePropertyNullable == isTargetPropertyNullable)
        {
            if (sourceProperty != null && sourceProperty.PropertyType == targetProperty.PropertyType)
            {
                var sourceValue = sourceType.GetProperty(sourceProperty.Name).GetValue(source, null);
                targetType.GetProperty(targetProperty.Name).SetValue(targetObject, sourceValue, null);
            }
        }
        else if (isTargetPropertyNullable && sourceProperty.PropertyType.UnderlyingSystemType == targetProperty.PropertyType.GetGenericArguments()[0])
        {
            var sourceValue = sourceType.GetProperty(sourceProperty.Name).GetValue(source, null);
            targetType.GetProperty(targetProperty.Name).SetValue(targetObject, sourceValue, null);
        }
    }

    public static TTarget MapValuesTo<TTarget>(this object source, bool ignoreNullValues = false)
    {
        try
        {
            var sourceType = source.GetType();
            var targetType = typeof(TTarget);

            var arrSourceProperties = sourceType.GetProperties();
            var arrTargetProperties = targetType.GetProperties();

            var ass = Assembly.GetAssembly(targetType);
            var targetObject = (TTarget)ass.CreateInstance(targetType.FullName);

            if (arrSourceProperties.Length < arrTargetProperties.Length)
            {

                foreach (var sourceProperty in arrSourceProperties)
                {
                    var targetProperty = arrTargetProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);

                    if (targetProperty is null) continue;

                    TrySetSourcePropertyValueOnTargetObject<TTarget>(source, sourceProperty, targetObject, targetProperty, ignoreNullValues);
                }
            }
            else
            {
                foreach (var targetProperty in arrTargetProperties)
                {
                    var sourceProperty = arrSourceProperties.FirstOrDefault(sp => sp.Name == targetProperty.Name);

                    if (sourceProperty is null) continue;

                    TrySetSourcePropertyValueOnTargetObject<TTarget>(source, sourceProperty, targetObject, targetProperty, ignoreNullValues);
                }
            }

            return targetObject;
        }
        catch (Exception exc)
        {
            throw exc;
        }
    }

    public static void MapValuesTo<TTarget>(this object source, ref TTarget targetObject, bool ignoreNullValues = false)
    {
        try
        {
            var sourceType = source.GetType();
            var targetType = typeof(TTarget);

            var arrSourceProperties = sourceType.GetProperties();
            var arrTargetProperties = targetType.GetProperties();

            if (arrSourceProperties.Length < arrTargetProperties.Length)
            {
                foreach (var sourceProperty in arrSourceProperties)
                {
                    var targetProperty = arrTargetProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);

                    if (targetProperty is null) continue;

                    TrySetSourcePropertyValueOnTargetObject(source, sourceProperty, targetObject, targetProperty, ignoreNullValues);
                }
            }
            else
            {
                foreach (var targetProperty in arrTargetProperties)
                {
                    var sourceProperty = arrSourceProperties.FirstOrDefault(sp => sp.Name == targetProperty.Name);

                    if (sourceProperty is null) continue;

                    TrySetSourcePropertyValueOnTargetObject(source, sourceProperty, targetObject, targetProperty, ignoreNullValues);
                }
            }
        }
        catch (Exception exc)
        {
            throw exc;
        }
    }

    public static List<TTarget> MapValuesToNewList<TSource, TTarget>(this ICollection<TSource> sourceList)
    {
        try
        {
            var targetListType = Type.GetType(typeof(List<TTarget>).AssemblyQualifiedName);
            var targetList = (List<TTarget>)Activator.CreateInstance(targetListType);

            targetList.AddRange(from object sourceItem in sourceList select sourceItem.MapValuesTo<TTarget>());

            return targetList;
        }
        catch (Exception exc)
        {
            throw exc;
        }
    }
    #endregion
}