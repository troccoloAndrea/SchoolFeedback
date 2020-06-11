using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFeedback.Models.TagHelpers
{
    public class RatingTagHelper : TagHelper
    {
        //proprietà automaticamente popolata grazie al model binding
        public decimal Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (Value >= i)
                {
                    output.Content.AppendHtml("<i class=\"fas fa-star rating\"></i>");
                }
                else if (Value > i - 1)
                {
                    output.Content.AppendHtml("<i class=\"fas fa-star-half-alt rating\"></i>");
                }
                else
                {
                    output.Content.AppendHtml("<i class=\"far fa-star rating\"></i>");
                }
            }
        }
    }
}
