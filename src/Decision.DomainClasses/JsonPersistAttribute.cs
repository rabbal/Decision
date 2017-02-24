using System;

namespace Decision.DomainClasses.Common
{

    // codehint: sm-add
    /// <summary>
    /// Marker attribute. Indicates that the settings should
    /// be persisted as a JSON string rather than splitted
    /// into single properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class JsonPersistAttribute : Attribute
    {
    }

}
