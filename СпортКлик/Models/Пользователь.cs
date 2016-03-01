using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace СпортКлик.Models
{
    public class Пользователь
    {
        public ПрофильПользователя Профиль { get; set; }
        public MembershipUser Членство { get; set; }
        public Guid Ид { get { return new Guid(Членство.ProviderUserKey.ToString()); } } 
        public Пользователь(string Имя)
        {
            Членство = Membership.GetUser(Имя);
            Профиль = new ПрофильПользователя(Имя);
        }
    }
}