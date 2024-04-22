using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeChallange1
{
    public partial class Calc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnmul_Click(object sender, EventArgs e)
        {
            double num1 = double.Parse(n1.Text);
            double num2 = double.Parse(n2.Text);
            double resultValue = num1 * num2;
            result.Text = $"The Result Is :<br/> {resultValue}";

        }

        protected void btndiv_Click(object sender, EventArgs e)
        {
            double num1 = double.Parse(n1.Text);
            double num2 = double.Parse(n2.Text);
            if (num2 == 0)
            {
                result.Text = "Division by zero is not allowed";
            }
            else
            {
                double resultValue = num1 / num2;
                result.Text = $"The Result Is : <br/>{resultValue}";
            }

        }
    }
}