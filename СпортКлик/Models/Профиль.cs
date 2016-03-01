using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace СпортКлик.Models
{
    public class ПрофильПользователя
    {
        private ProfileBase profile = null;

        public ПрофильПользователя(string username)
        {
            profile = ProfileBase.Create(username);
        }
        [Display(Name = "Имя")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+", ErrorMessage = "Только буквы")]
        public string Имя
        {
            get
            {
                if (profile == null) return string.Empty;
                else return profile["Имя"] as string;
            }
            set { if (profile != null) { profile["Имя"] = value; } }
        }
        [Display(Name = "Отчество")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+", ErrorMessage = "Только буквы")]
        public string Отчество
        {
            get
            {
                if (profile == null) return string.Empty;
                else return profile["Отчество"] as string;
            }
            set { if (profile != null) { profile["Отчество"] = value; } }
        }
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+", ErrorMessage = "Только буквы")]
        public string Фамилия
        {
            get
            {
                if (profile == null) return string.Empty;
                else return profile["Фамилия"] as string;
            }
            set { if (profile != null) { profile["Фамилия"] = value; } }
        }
        [Display(Name = "Телефон")]
        [RegularExpression(@"^[0-9()-]+", ErrorMessage = "Только цифры и '-', '(',')'")]
        public string Телефон
        {
            get
            {
                if (profile == null) return string.Empty;
                else return profile["Телефон"] as string;
            }
            set { if (profile != null) { profile["Телефон"] = value; } }
        }
        public void Save() { if (profile != null)profile.Save(); }
    }

}