using AutoMapper;
using ExpenseTracker.BLL.Models;
using ExpenseTracker.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = ExpenseTracker.DAL.Entities.Transaction;

namespace ExpenseTracker.BLL.MappingProfiles
{
    public class TransactionMappingProfile:Profile
    {

        public TransactionMappingProfile()
        {
            CreateMap<Category, CategoryVM>();
            CreateMap<Transaction, TransactionVM>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.CategoryTitleWithIcon, opt => opt.MapFrom(src => src.Category == null ? "" : src.Category.Icon + " " + src.Category.Title))
                .ForMember(dest => dest.FormattedAmount, opt => opt.MapFrom(src => ((src.Category == null || src.Category.Type == "Expense") ? "- " : "+ ") + src.Amount.ToString("C0")));



            CreateMap<TransactionVM, Transaction>() // new mapping configuration
                      .ForMember(dest => dest.Category, opt => opt.Ignore()) // ignore mapping for category
                      .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                      .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                      .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                      .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

         


        }


    }

   
}
