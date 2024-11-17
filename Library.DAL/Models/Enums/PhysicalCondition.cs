using System.ComponentModel;

namespace Library.DAL.Models.Enums;

public enum PhysicalCondition
{
    /// <summary>
    /// Новая
    /// </summary>
    [Description("Новая")]
    New,

    /// <summary>
    /// В хорошем состоянии
    /// </summary>
    [Description("В хорошем состоянии")]
    Good,

    /// <summary>
    /// Небольшие повреждения
    /// </summary>
    [Description("Небольшие повреждения")]
    MinorDamage,

    /// <summary>
    /// В плохом состоянии
    /// </summary>
    [Description("В плохом состоянии")]
    BadCondition
}