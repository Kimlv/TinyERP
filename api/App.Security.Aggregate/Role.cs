namespace App.Security.Aggregate
{
    using App.Common;
    using App.Common.Data;
    public class Role : BaseContent, IClonable<Role>
    {
        public string DomainKey { get; set; }
        /// <summary>
        /// Do not create new instance using this ctor
        /// </summary>
        public Role():base(){}

        public Role(Role role) : base(role)
        {
            this.DomainKey = role.DomainKey;
        }

        public Role(string name, string key, string description, string domainKey) : base(name, key, description)
        {
            this.DomainKey = domainKey;
        }

        public Role Clone()
        {
            return new Role(this);
        }
    }
}
