using System;
using UnityEngine;

namespace UtilsToolbox.PropertyAttributes
{
    public enum AutoFindMode
    {
        Self,
        Children,
        Parent
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class AutoFindAttribute : PropertyAttribute
    {
        public AutoFindMode Mode { get; }

        public AutoFindAttribute(AutoFindMode mode = AutoFindMode.Self)
        {
            Mode = mode;
        }
    }
}