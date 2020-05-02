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

            CreateMap<Currency, CurrencyModel>();
            CreateMap<CurrencyModel, Currency>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<Goal, GoalModel>();
            CreateMap<GoalModel, Goal>();

            CreateMap<Transaction, TransactionModel>();
            CreateMap<TransactionModel, Transaction>();

            CreateMap<Template, TemplateModel>();
            CreateMap<TemplateModel, Template>();
        }
    }
}
