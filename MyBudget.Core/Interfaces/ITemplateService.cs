using MyBudget.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudget.Core.Interfaces
{
    public interface ITemplateService
    {
        public TemplateModel GetTemplate(int templateID);
        
        public List<TemplateModel> GetTemplates(string userID);

        public void AddTemplate(TemplateModel templateModel);

        public Task<int> AddTemplateAsync(TemplateModel templateModel);

        public void UpdateTemplate(TemplateModel templateModel);

        public void DeleteTemplate(int id);
    }
}
