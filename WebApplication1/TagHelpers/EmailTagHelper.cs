//using öffnet sich nicht automatisch...warum auch immer
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BethanysPieShop.TagHelpers
{
    public class EmailTagHelper: TagHelper //"email" wil be used by convention as a tag
    {
        public string? Address { get; set; }
        public string? Content { get; set; }
        //through process we get acces to the context, which gives info about the current executing context, as well as the output
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //tag name of the output will be a
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + Address);
            //what will go between the open and closing tags
            output.Content.SetContent(Content);
        }
    }
}
