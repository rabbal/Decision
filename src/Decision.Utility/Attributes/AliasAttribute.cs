using System;

namespace Decision.Utility.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AliasAttribute : Attribute
    {
        public virtual string Name { get; set; }

        public AliasAttribute(string name)
        {
            Name = name;
        }
    }
}
