using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MozaeekCore.Enum
{
    public enum CoreRole
    {
        [Display(Name = "کاربین ادمین")]
        Admin = 1,
        [Display(Name = "کاربین چارچوب")]
        BasiInfo = 2,
        [Display(Name = "کاربین عملیات")]
        Operation = 3
    }
}
