using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Employee_Login_App
{
    public partial class Employee : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvEmployees.DataSource = dt;
                gvEmployees.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string contact = txtContact.Text.Trim();

            // Validate password complexity
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$";
            if (!Regex.IsMatch(password, pattern))
            {
                lblMessage.Text = "Password must contain uppercase, lowercase, number, and special character.";
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Users (Username, Password, Name, Address, Contact) VALUES (@u,@p,@n,@a,@c)", con);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@a", address);
                cmd.Parameters.AddWithValue("@c", contact);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                lblMessage.Text = "Employee added successfully!";
                ClearInputs();
                BindGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void ClearInputs()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtContact.Text = "";
        }

        // GridView Events: Edit, Update, Cancel, Delete
        protected void gvEmployees_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvEmployees_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvEmployees.EditIndex = -1;
            BindGrid();
        }

        protected void gvEmployees_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);
            string username = ((System.Web.UI.WebControls.TextBox)gvEmployees.Rows[e.RowIndex].Cells[1].Controls[0]).Text.Trim();
            string name = ((System.Web.UI.WebControls.TextBox)gvEmployees.Rows[e.RowIndex].Cells[2].Controls[0]).Text.Trim();
            string address = ((System.Web.UI.WebControls.TextBox)gvEmployees.Rows[e.RowIndex].Cells[3].Controls[0]).Text.Trim();
            string contact = ((System.Web.UI.WebControls.TextBox)gvEmployees.Rows[e.RowIndex].Cells[4].Controls[0]).Text.Trim();

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Users SET Username=@u, Name=@n, Address=@a, Contact=@c WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@a", address);
                cmd.Parameters.AddWithValue("@c", contact);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                gvEmployees.EditIndex = -1;
                BindGrid();
                lblMessage.Text = "Employee updated successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        protected void gvEmployees_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                BindGrid();
                lblMessage.Text = "Employee deleted successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }
    }
}