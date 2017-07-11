namespace App.Entity.Common
{
    using System;
    using App.Common;
    using App.Common.Data;

    public class Parameter : BaseContent
    {
        public Guid ParentId { get; set; }
        public ParameterParentType ParentType { get; set; }
        public bool IsRequired { get; set; }
        public bool IsEncoded { get; set; }
        public string Value { get; set; }
        public ParameterValueType ValueType { get; set; }
        public Parameter() : base()
        {
            this.IsEncoded = false;
            this.ValueType = ParameterValueType.String;
            this.IsRequired = false;
        }

        public void UpdateFrom(Parameter param)
        {
            this.Name = param.Name;
            this.Key = param.Key;
            this.Description = param.Description;
            this.IsRequired = param.IsRequired;
            this.IsEncoded = param.IsEncoded;
            this.Value = param.Value;
            this.ValueType = param.ValueType;
        }

        public void CreateFrom(Parameter param)
        {
            this.Name = param.Name;
            this.Key = param.Key;
            this.ParentId = param.ParentId;
            this.ParentType = param.ParentType;
            this.Description = param.Description;
            this.IsRequired = param.IsRequired;
            this.IsEncoded = param.IsEncoded;
            this.Value = param.Value;
            this.ValueType = param.ValueType;
        }
    }
}
