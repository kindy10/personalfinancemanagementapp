using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinance.Shared.DTOs.Enums;

namespace PersonalFinance.Shared.DTOs.Categories
{
    public  class CreateCategoryRequestDto
    {
        public string Name { get; set; }

        public CategoryType  Type { get; set; }
    }
}
