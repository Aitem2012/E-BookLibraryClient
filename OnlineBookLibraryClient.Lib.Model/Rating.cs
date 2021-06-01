using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBookLibraryClient.Lib.Model
{
    public class Rating
    {
        public Rating(int rate)
        {
            for (int i = 1; i <= 5; i++)
            {
                if(rate>i && rate < (i + 1))
                {
                    //use half shaded star
                }
                else if (rate >= i)
                {
                    //use shaded star
                }
                else
                {
                    //use unshaded star
                }
            }
        }
        

        /* public static string Ratings(this HtmlHelper helper, PostModel post)
         {
             StringBuilder sb = new StringBuilder();
             sb.AppendFormat("<span class='rating' rating='{0}' post='{1}' title='Click to cast vote'>", post.Rating, post.ID);

             string formatStr = "<img src='/Content/images/{0}' alt='star' width='5' height='12' class='star' value='{1}' />";
             for (Double i = .5; i <= 5.0; i = i + .5)
             {
                 if (i <= post.Rating)
                 {
                     sb.AppendFormat(formatStr, (i * 2) % 2 == 1 ? "star-left-on.gif" : "star-right-on.gif", i);
                 }
                 else
                 {
                     sb.AppendFormat(formatStr, (i * 2) % 2 == 1 ? "star-left-off.gif" : "star-right-off.gif", i);
                 }
             }
             sb.AppendFormat(" <span>Currently rated {0} by {1} people</span>", post.Rating, post.Raters);
             sb.Append("</ span > ");
     return sb.ToString();
         }*/
    }
}
