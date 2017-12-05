using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace shoppingcart.Models
{
    public static  class HelperExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper helper,
            string imageUrl, string alt, string width, string height, string hidden)
        {
            if(  !String.IsNullOrEmpty(imageUrl)  )
            {
                TagBuilder imageTag = new TagBuilder("img");

                imageTag.MergeAttribute("src", imageUrl);
                imageTag.MergeAttribute("alt", alt);
                imageTag.MergeAttribute("width", width);
                imageTag.MergeAttribute("height", height);

                if(   !(hidden == null)  && hidden.Equals("hidden")  )
                {
                    imageTag.MergeAttribute("hidden", hidden);
                }
                return MvcHtmlString.Create(imageTag.ToString());
            }

            return null;
        }



        public static MvcHtmlString FileUpload(this HtmlHelper helper,
           string type, string id, string name, string value)
        {
            if (!String.IsNullOrEmpty(type))
            {
                TagBuilder fileUpload = new TagBuilder("input");

                fileUpload.MergeAttribute("type", type);
                fileUpload.MergeAttribute("id", id);
                fileUpload.MergeAttribute("name", name);
                fileUpload.MergeAttribute("value", value);

                return MvcHtmlString.Create(fileUpload.ToString());
            }

            return null;
        }


        
        public static MvcHtmlString FormSubmit(this HtmlHelper helper,
                    string type, string name, string value, string cssClass)
        {

            if (!String.IsNullOrEmpty(type))
            {
                TagBuilder submitTag = new TagBuilder("input");

                submitTag.MergeAttribute("type", type);
                submitTag.MergeAttribute("name", name);
                submitTag.MergeAttribute("value", value);
                submitTag.MergeAttribute("class", cssClass);

                return MvcHtmlString.Create(submitTag.ToString());
            }

            return null;
        }






        public static MvcHtmlString PageLinks(this HtmlHelper helper,
                    PagingModel pagingModel, Func<int, string> pageUrl)
        {
            StringBuilder sb = new StringBuilder();

            if (pagingModel != null)
            {
                for (int i=1; i<=pagingModel.TotalPages; i++)
                {
                    TagBuilder pageTag = new TagBuilder("a");
                    pageTag.MergeAttribute("href", pageUrl(i));
                    pageTag.InnerHtml = i.ToString();

                    pageTag.AddCssClass("btn btn-default");

                    sb.Append(pageTag.ToString());
                }
                return MvcHtmlString.Create(sb.ToString());
            }

            return null;
        }

    }
}