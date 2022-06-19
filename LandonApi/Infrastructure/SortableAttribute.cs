using System;

namespace LandonApi.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    public class SortableAttribute:Attribute
    {
    }
}
