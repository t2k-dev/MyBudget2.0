using MyBudget.Core.Models;
using System.Collections.Generic;

namespace MyBudget.Core.Interfaces
{
    public interface ITemplateService
    {
        public TemplateModel GetTemplate(int templateID);
        
        public List<TemplateModel> GetTemplates(string userID);

        public void AddTemplate(TemplateModel templateModel);

        public void UpdateTransaction(TemplateModel templateModel);

        public void DeleteTemplate(int templateID);
    }
}
