using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMC.Portal.Web.UI.ViewModels
{
    public class Menu
    {
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public string Content { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public string SelectedStyle { get; set; }  
        public string SelectedClass { get; set; }
        public string OnClick { get; set; }
        public long Order { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }


    public class MenuViewModel
    {
        public Guid ID { get; set; }
        public string Content { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public string SelectedStyle { get; set; }
        public string SelectedClass { get; set; }
        public string OnClick { get; set; }
        public IList<MenuViewModel> Children { get; set; }
    }
}
