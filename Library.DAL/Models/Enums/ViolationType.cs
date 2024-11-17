using System.ComponentModel;

namespace Library.DAL.Models.Enums;

public enum ViolationType
{
    /// <summary>
    /// Поврежденная книга
    /// </summary>
    [Description("Поврежденная книга")]
    DamagedBook,

    /// <summary>
    /// Истекший срок аренды
    /// </summary>
    [Description("Истекший срок аренды")]
    ExpiredRent
}