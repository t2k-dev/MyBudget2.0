using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBudget.Core.Extensions;
using MyBudget.Core.Interfaces;
using MyBudget.Core.Models;
using MyBudget.Data;
using MyBudget.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.Core.Services
{
    public class TemplateService : ITemplateService
    {
        #region ctors & fields
        private ApplicationContext _context;
        private readonly IMapper _mapper;

        public TemplateService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public TemplateModel GetTemplate(int templateID)
        {
            var template = _context.Templates
                .Include(t => t.Currency)
                .SingleOrDefault(t => t.ID == templateID);

            return _mapper.Map<Template, TemplateModel>(template);
        }

        public List<TemplateModel> GetTemplates(string userID)
        {
            userID.CheckForNull(nameof(userID));

            var templates =_context.Templates
                .Where(t => t.UserID == userID)
                .Include(t => t.Category)
                .Include(t => t.Currency)
                .ToList();

            return _mapper.Map<List<Template>, List<TemplateModel>>(templates);
        }

        public void AddTemplate(TemplateModel templateModel)
        {
            templateModel.CheckForNull(nameof(templateModel));

            var template = _mapper.Map<TemplateModel, Template>(templateModel);

            _context.Templates.Add(template);
            _context.SaveChanges();

        }

        public async Task<int> AddTemplateAsync(TemplateModel templateModel)
        {
            templateModel.CheckForNull(nameof(templateModel));

            var template = _mapper.Map<TemplateModel, Template>(templateModel);

            _context.Templates.Add(template);
            await _context.SaveChangesAsync();

            return template.ID;
        }
        public void UpdateTemplate(TemplateModel templateModel)
        {
            templateModel.CheckForNull(nameof(templateModel));

            var template = _mapper.Map<TemplateModel, Template>(templateModel);

            var templateInDb = _context.Templates.Single(t => t.ID == template.ID);

            templateInDb.Name = template.Name;
            templateInDb.Amount = template.Amount;
            templateInDb.CategoryID = template.CategoryID;
            templateInDb.IsSpending = template.IsSpending;
            templateInDb.Day = template.Day;
            templateInDb.UserID = template.UserID;
            templateInDb.CurrencyID = template.CurrencyID;

            _context.SaveChanges();
        }

        public void DeleteTemplate(int id)
        {
            var template = _context.Templates.SingleOrDefault(t => t.ID == id);
            _context.Templates.Remove(template);
            _context.SaveChanges();

        }
    }
}
