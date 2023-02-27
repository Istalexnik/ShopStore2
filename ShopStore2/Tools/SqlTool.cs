using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopStore2.Tools
{
    public class SqlTool
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;


        public object RunProcScalar(string proc, List<SqlParameter> lParam = null)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (lParam != null)
                    {
                        cmd.Parameters.AddRange(lParam.ToArray());
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public SqlDataReader RunProcReader(string proc, List<SqlParameter> lParam = null)
        {
            SqlConnection con = new SqlConnection(constr);
            using (SqlCommand cmd = new SqlCommand(proc, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (lParam != null)
                {
                    cmd.Parameters.AddRange(lParam.ToArray());
                }
                con.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public int RunProcNonQuery(string proc, List<SqlParameter> lParam = null)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(lParam != null)
                    {
                        cmd.Parameters.AddRange(lParam.ToArray());
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public int RunTextNonQuery(string text, List<SqlParameter> lParam = null)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(text, con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (lParam != null)
                    {
                        cmd.Parameters.AddRange(lParam.ToArray());
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}