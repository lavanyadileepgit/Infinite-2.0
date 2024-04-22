using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeChallange1
{
    public partial class Message : System.Web.UI.Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {

            }

            protected void Button1_Click(object sender, EventArgs e)
            {
                
                int clickCount = int.Parse(HiddenField1.Value) + 1;
                HiddenField1.Value = clickCount.ToString();
                Response.Write($"Button clicked {clickCount} times. <br/> Have a nice day....");
            }
        }
    }

