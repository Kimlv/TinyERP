namespace App.Security.Aggregate
{
    using App.Common.Aggregate;
    using App.Security.Event;
    using App.Security.Event.User;
    using Common;
    using Common.Helpers;
    using System;
    using System.Collections.Generic;

    public class User : BaseAggregateRoot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Email { get; set; }
        public UserAccountStatus Status { get; set; }
        public string LoginToken { get; set; }
        public DateTime TokenExpiredAfter { get; set; }
        public DateTime LastLogin { get; set; }
        public Roles Roles { get; set; }
        /// <summary>
        /// Do not create new instance using this ctor
        /// </summary>
        public User() : base()
        {
            this.Roles = new Roles();
            this.TokenExpiredAfter = DateTime.UtcNow;
            this.LastLogin = DateTime.UtcNow;
            this.Status = UserAccountStatus.Active;
            this.AddEvent(new OnUserCreated()
            {
                UserId = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                UserName = this.UserName
            });
        }

        public void GenerateLoginToken()
        {
            AuthenticationToken token = TokenHelper.CreateNewAuthenticationToken(this.LoginToken);
            this.LoginToken = token.Value;
            this.TokenExpiredAfter = token.ExpiredAfter;
        }

        public void AddRole(Role role)
        {
            this.Roles.Add(role.Clone());
            this.AddEvent(new OnUserRoleAdded()
            {
                UserId = this.Id,
                RoleId = role.Id
            });
        }
    }
}