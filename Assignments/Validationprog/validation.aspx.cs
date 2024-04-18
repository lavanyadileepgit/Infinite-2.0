using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Validationprog
{
    public partial class validation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            
            if (txtName.Text.Trim().Equals(txtFamilyName.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                Response.Write("<script>alert('Name must be different from Family Name');</script>");
                return;
            }

            
            if (txtAddress.Text.Trim().Length < 2)
            {
                Response.Write("<script>alert('Address must be at least 2 characters long');</script>");
                return;
            }

           
            if (txtCity.Text.Trim().Length < 2)
            {
                Response.Write("<script>alert('City must be at least 2 characters long');</script>");
                return;
            }

            
            int zipCode;
            if (!int.TryParse(txtZipCode.Text.Trim(), out zipCode) || txtZipCode.Text.Trim().Length != 5)
            {
                Response.Write("<script>alert('Zip Code must be a 5-digit number');</script>");
                return;
            }

           
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text.Trim(), @"^\d{2,3}-\d{7}$"))
            {
                Response.Write("<script>alert('Phone must be in the format XX-XXXXXXX or XXX-XXXXXXX');</script>");
                return;
            }

            if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                Response.Write("<script>alert('Invalid Email address');</script>");
                return;
            }

            details.InnerHtml = $@"
    <h3>Validation Sum</h3>
    <p><strong>Name:</strong> {txtName.Text}</p>
    <p><strong>Family Name:</strong> {txtFamilyName.Text}</p>
    <p><strong>Address:</strong> {txtAddress.Text}</p>
    <p><strong>City:</strong> {txtCity.Text}</p>
    <p><strong>Zip Code:</strong> {txtZipCode.Text}</p>
    <p><strong>Phone:</strong> {txtPhone.Text}</p>
    <p><strong>Email:</strong> {txtEmail.Text}</p>";
        }

        
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
