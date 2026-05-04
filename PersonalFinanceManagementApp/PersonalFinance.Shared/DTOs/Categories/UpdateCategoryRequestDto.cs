using PersonalFinance.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.DTOs.Categories
{
    public class UpdateCategoryRequestDto
    {
        public string Name { get; set; }

        public CategoryType Type { get; set; }

    }
}
