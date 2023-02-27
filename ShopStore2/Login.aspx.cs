using ShopStore2.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopStore2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) { return; }
            CheckQuesryString();
        }

        private void CheckQuesryString()
        {
            lblMessageActivationLinkSent.Visible = Request.QueryString["ActivationLinkSent"] != null;
            if (string.IsNullOrEmpty(Request.QueryString["ActivationCode"])) { return; }
            string activationCode = Request.QueryString["ActivationCode"];
            Guid guid;
            Guid.TryParse(activationCode, out guid);
            SqlTool sqlTool = new SqlTool();
            int rowAffected = sqlTool.RunTextNonQuery("delete tbl_activation where col_activation_code = @col_activation_code",
                new List<SqlParameter>() { new SqlParameter("@col_activation_code", guid) });
            if (rowAffected > 0)
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Your account has been activated. You can log in now";
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Invalid Activation Code";
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ValidateUser();
        }

        private void ValidateUser()
        {
            SqlTool sqlTool = new SqlTool();
            Encryption enc = new Encryption();
            List<SqlParameter> lParam = new List<SqlParameter>()
            {
                new SqlParameter("@col_username", txtUsername.Text.Trim()),
                new SqlParameter("@col_password", enc.EncryptIt(txtPassword.Text.Trim()))
            };
            using (SqlDataReader reader = sqlTool.RunProcReader("usp_validate_user", lParam))
            {
                if (reader.Read())
                {
                    int userid = Convert.ToInt32(reader["userid"]);
                    string rolename = Convert.ToString(reader["rolename"]);
                    switch (userid)
                    {
                        case -1:
                            {
                                lblMessage.ForeColor = Color.Red;
                                lblMessage.Text = "Username and/or password is incorrect";
                                break;
                            }
                        case -2:
                            {
                                lblMessage.ForeColor = Color.Red;
                                lblMessage.Text = "Account has not been activated";
                                break;
                            }
                        default:
                            {
                                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, txtUsername.Text.Trim(),
                                DateTime.Now, DateTime.Now.AddMinutes(2880), chkRememberMe.Checked, rolename, FormsAuthentication.FormsCookiePath);
                                string hash = FormsAuthentication.Encrypt(ticket);
                                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                                if (ticket.IsPersistent)
                                {
                                    cookie.Expires = ticket.Expiration;
                                }
                                Response.Cookies.Add(cookie);
                                Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text.Trim(), chkRememberMe.Checked));
                                break;
                            }
                    }
                }
            }

        }
    }
}