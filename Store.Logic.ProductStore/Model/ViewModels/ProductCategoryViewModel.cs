using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Store.Logic.ProductStore.Models.ViewModels
{
    //[Bind(Exclude = "Id")]
    public class ProductCategoryViewModel
    {
        public int Id { get;  set; }
        [Display(Name = "Название")]
        public string Name { get;  set; }

        public string Description { get;  set; }

        public ProductCategoryViewModel ()
        {

        }
    }
}
