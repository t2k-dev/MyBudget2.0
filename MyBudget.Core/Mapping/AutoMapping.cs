using AutoMapper;
using MyBudget.Core.Models;
using MyBudget.Domain;

namespace MyBudget.Core.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();

            CreateMap<Goal, GoalModel>();
            CreateMap<GoalModel, Goal>();

            CreateMap<Currency, CurrencyModel>();
            CreateMap<CurrencyModel, Currency>();

            CreateMap<Transaction, TransactionModel>();
            CreateMap<TransactionModel, Transaction>();

            CreateMap<Template, TemplateModel>();
            CreateMap<TemplateModel, Template>();
        }
    }
}
