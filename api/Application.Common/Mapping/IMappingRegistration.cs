namespace App.Common.Mapping
{
    using System;
    public interface IMappingRegistration
    {
        Type From { get; }
        Type To { get; }
    }
}
