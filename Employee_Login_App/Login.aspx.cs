using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Employee_Login_App
{
    public partial class Login : System.Web.UI.Page
    {
        // Use the correct connection string name from your Web.config
        SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string retypePassword = txtRetypePassword.Text.Trim();

            // Check if passwords match
            if (password != retypePassword)
            {
                lblMessage.Text = "Passwords do not match!";
                txtPassword.BorderColor = System.Drawing.Color.Red;
                txtRetypePassword.BorderColor = System.Drawing.Color.Red;
                return;
            }

            // Check password complexity
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$";
            if (!Regex.IsMatch(password, pattern))
            {
                lblMessage.Text = "Password must contain uppercase, lowercase, number, and special character.";
                txtPassword.BorderColor = System.Drawing.Color.Red;
                return;
            }

            // Query database
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username=@u AND Password=@p", con);
            cmd.Parameters.AddWithValue("@u", email);
            cmd.Parameters.AddWithValue("@p", password);

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Session["Username"] = email;
                    Response.Redirect("Employee.aspx"); // Redirect after successful login
                }
                else
                {
                    lblMessage.Text = "Invalid credentials!";
                    txtPassword.BorderColor = System.Drawing.Color.Red;
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Database error: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}