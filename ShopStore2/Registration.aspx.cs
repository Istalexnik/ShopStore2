using ShopStore2.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopStore2
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            SqlTool sqlTool = new SqlTool();
            Encryption enc = new Encryption();
            List<SqlParameter> lParam = new List<SqlParameter>()
            {
                new SqlParameter("@col_username", txtUsername.Text.Trim()),
                new SqlParameter("@col_password", enc.EncryptIt(txtPassword.Text.Trim())),
                new SqlParameter("@col_email", txtEmail.Text.Trim())
            };
            int userid = Convert.ToInt32(sqlTool.RunProcScalar("usp_register_user", lParam));
            switch (userid)
            {
                case -1:
                    {
                        lblMessage.Text = "Username already exists. \nPlease choose a different username";
                        break;
                    }
                case -2:
                    {
                        lblMessage.Text = "This email address has already been used";
                        break;
                    }
                default:
                    {
                        string activationCode = CreateActivationCode(userid);
                        SendActivationEmail(activationCode);
                        Response.Redirect("Login.aspx?ActivationLinkSent=true");
                        break;
                    }
            }
        }

        private string CreateActivationCode(int userid)
        {
            SqlTool sqlTool = new SqlTool();
            string activationCode = Guid.NewGuid().ToString();
            List<SqlParameter> lParam = new List<SqlParameter>()
            {
                new SqlParameter("@col_userid", userid),
                new SqlParameter("@col_activation_code", activationCode)
            };
            sqlTool.RunTextNonQuery("insert into tbl_activation(col_userid, col_activation_code) values(@col_userid, @col_activation_code)", lParam);
            return activationCode;
        }

        private void SendActivationEmail(string activationCode)
        {
            string fileName = Server.MapPath("~/Templates/Notice_Registration.html");
            string body = File.ReadAllText(fileName);
            body = body.Replace("##USERNAME##", txtUsername.Text.Trim());
            body = body.Replace("##URL##", Request.Url.AbsoluteUri.Replace("Registration.aspx", Convert.ToString("Login.aspx?ActivationCode=") + activationCode));
            Emailing.SendEmail(txtEmail.Text, "Account Activation", body);
        }


    }
}