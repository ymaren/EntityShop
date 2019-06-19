using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using System.Web.Mvc;

namespace MyShop.Helpers
{
    public static class  MyHelpers
    {
        public static MvcHtmlString CreateSubMenu(this HtmlHelper html, IEnumerable<Credential> subMenuList, int? parentId)
        {
            var subitems = subMenuList.Where(d => d.ParentCredentialId == parentId).OrderBy(i => i.Order);
            if (subitems.Any())
            {
                TagBuilder ul = new TagBuilder("ul");
                foreach(var itemSub in subitems)
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder link = new TagBuilder("a");
                    link.MergeAttribute("href", itemSub.Url);
                    link.InnerHtml = itemSub.FullNameCredential;
                    li.InnerHtml = link.ToString();
                    ul.InnerHtml += li.ToString();
                }
                return new MvcHtmlString(ul.ToString());
            }
         return null;
        }

        

    }
}
