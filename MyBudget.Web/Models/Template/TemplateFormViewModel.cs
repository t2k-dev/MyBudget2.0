using Microsoft.AspNetCore.Mvc.Rendering;
using MyBudget.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.Web.Models.Template
{
    public class TemplateFormViewModel
    {
        public TemplateModel Template { get; set; }

        public IEnumerable<CategoryModel> Categories { get; set; }

        public SelectList Days { get; set; }

        public string DefaultCurrencySymbol { get; set; }
    }
}
