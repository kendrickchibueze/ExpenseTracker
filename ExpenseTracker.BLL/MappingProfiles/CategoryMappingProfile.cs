using AutoMapper;
using ExpenseTracker.BLL.Models;
using ExpenseTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.BLL.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {

        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryVM>();

            CreateMap<CategoryVM, Category>();
        }
    }

   
}
