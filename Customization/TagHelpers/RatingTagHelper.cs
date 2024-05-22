using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebAppCourse.Customization.TagHelpers
{
    public class RatingTaghelper : TagHelper
    {
        public double Value { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //double value = (double)context.AllAttributes["value"].Value;
            for(int i = 1;i<=5;i++) 
            {
                if(Value >= i) 
                {
                   output.Content.AppendHtml("<i class=\"fas fa-star\"></i>");
                }
                else if (Value > i - 1)
                {
                    output.Content.AppendHtml("<i class=\"fas fa-star-half-alt\"></i>");
                }
                else 
                {
                    output.Content.AppendHtml("<i class=\"far fa-star\"></i>");
                }
            }
        }
    }
}