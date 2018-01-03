//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SP_Sklad.SkladData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public Users()
        {
            this.Kagent = new HashSet<Kagent>();
            this.OperLog = new HashSet<OperLog>();
            this.PrintLog = new HashSet<PrintLog>();
            this.UserAccess = new HashSet<UserAccess>();
            this.UserSettings = new HashSet<UserSettings>();
            this.UserAccessCashDesks = new HashSet<UserAccessCashDesks>();
            this.UserAccessWh = new HashSet<UserAccessWh>();
        }
    
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string FullName { get; set; }
        public string SysName { get; set; }
        public Nullable<int> ShowBalance { get; set; }
        public Nullable<int> ShowPrice { get; set; }
        public Nullable<int> EnableEditDate { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<bool> IsOnline { get; set; }
        public string ReportFormat { get; set; }
        public Nullable<System.Guid> GroupId { get; set; }
        public Nullable<bool> InternalEditor { get; set; }
        public bool IsWorking { get; set; }
    
        public virtual ICollection<Kagent> Kagent { get; set; }
        public virtual ICollection<OperLog> OperLog { get; set; }
        public virtual ICollection<PrintLog> PrintLog { get; set; }
        public virtual ICollection<UserAccess> UserAccess { get; set; }
        public virtual UsersGroup UsersGroup { get; set; }
        public virtual ICollection<UserSettings> UserSettings { get; set; }
        public virtual ICollection<UserAccessCashDesks> UserAccessCashDesks { get; set; }
        public virtual ICollection<UserAccessWh> UserAccessWh { get; set; }
    }
}
