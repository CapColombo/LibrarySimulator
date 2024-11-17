using System.ComponentModel;
using System.Reflection;

namespace Library.Common.Extensions;

public static class EnumExtension
{
    public static T? GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
    {
        MemberInfo[] memInfo = enumVal.GetType().GetMember(enumVal.ToString());

        object[] attributes = memInfo[0].GetCustomAttributes(typeof(T), false);

        return attributes.Any() ? (T)attributes[0] : null;
    }

    public static string? GetDescription(this Enum enumVal)
    {
        return enumVal?.GetAttributeOfType<DescriptionAttribute>()?.Description;
    }

    public static bool IsCorrect<T>(this Enum value) where T : Enum
    {
        return value != null && Enum.IsDefined(typeof(T), value);
    }

    public static int ToInt<T>(this T enumValue) where T : Enum
    {
        return Convert.ToInt32(enumValue);
    }

    public static List<T> GetEnumList<T>()
    {
        var array = (T[])Enum.GetValues(typeof(T));
        var list = new List<T>(array);
        return list;
    }
}
