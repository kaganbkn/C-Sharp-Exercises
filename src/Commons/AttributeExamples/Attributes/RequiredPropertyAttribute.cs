using System;

namespace AttributeExamples
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    internal class RequiredPropertyAttribute : Attribute
    {
    }
}