//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace N32MODEL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ou_UserInfo
    {
        public Ou_UserInfo()
        {
            this.Ou_UserRole = new HashSet<Ou_UserRole>();
            this.Ou_UserVipPermission = new HashSet<Ou_UserVipPermission>();
        }
    
        public int uId { get; set; }
        public int uDepId { get; set; }
        public string uLoginName { get; set; }
        public string uPwd { get; set; }
        public bool uGender { get; set; }
        public string uPost { get; set; }
        public string uRemark { get; set; }
        public bool uIsDel { get; set; }
        public System.DateTime uAddTime { get; set; }
    
        public virtual Ou_Department Ou_Department { get; set; }
        public virtual ICollection<Ou_UserRole> Ou_UserRole { get; set; }
        public virtual ICollection<Ou_UserVipPermission> Ou_UserVipPermission { get; set; }
    }
}
