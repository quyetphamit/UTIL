using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DEV_CSharp
{
    class ConectorFactory
    {
        private string strConn = @"Data Source=UMC-C531;Initial Catalog=ScheduleDB;User ID=sa;Password=123456;Integrated Security=False;Connect Timeout=15;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlCommand cmd;
        private SqlConnection conn;
        private SqlDataReader dr;
        private SqlDataAdapter da;
        private DataTable dt;
        //private DataSet ds;
        public ConectorFactory()
        {
            conn = new SqlConnection(strConn);
        }
        /// <summary>
        /// Mở kết nối
        /// </summary>
        public void OpenConnect()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// Đóng kết nối
        /// </summary>
        public void CloseConect()
        {

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Lấy giá trị từ các bảng
        /// </summary>
        /// <param name="tableName">Tên bảng cần truy vấn</param>
        /// <returns>Data Reader</returns>
        public SqlDataReader getData(string sql)
        {
            try
            {
                //string sql = "select * from " + tableName;
                OpenConnect();
                cmd = new SqlCommand(sql);
                cmd.Connection = conn;
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dr;
        }
        /// <summary>
        /// Lấy giá trị từ dữ liệu
        /// </summary>
        /// <param name="sql">Query</param>
        /// <returns>Datatable</returns>
        public DataTable getTable(string sql)
        {
            try
            {
                dt = new DataTable();
                using (cmd = new SqlCommand(sql, conn))
                {
                    using (da = new SqlDataAdapter(cmd))
                    {
                        OpenConnect();
                        da.Fill(dt);
                    }
                }
                CloseConect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public void ExcuteNonQuery(string sql)
        {
            try
            {
                OpenConnect();
                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                CloseConect();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
