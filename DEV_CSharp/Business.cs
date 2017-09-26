using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DEV_CSharp
{
    class Business
    {
        ConectorFactory conn = null;
        public Business()
        {
            conn = new ConectorFactory();
        }
        public List<History> getAll()
        {
            DataTable dt = null;
            string sql = "select * from Historytb";
            dt = conn.getTable(sql);
            var lstHis = (from rw in dt.AsEnumerable()
                          select new History()
                          {
                              id = Convert.ToInt32(rw["id"].ToString()),
                              author = rw["author"].ToString(),
                              date = Convert.ToDateTime(rw["date"]),
                              type = rw["type"].ToString()
                          }).ToList();
            conn.CloseConect();
            return lstHis;
        }
        public void Save(History history)
        {
            string sql = string.Format("INSERT INTO Historytb VALUES ('{0}','{1}','{2}')", history.author, history.date, history.type);
            conn.OpenConnect();
            conn.ExcuteNonQuery(sql);
        }
        public List<History> getAllHistories()
        {
            SqlDataReader dr = null;
            List<History> lst = new List<History>();
            string sql = "Select * from Historytb";
            try
            {
                dr = conn.getData(sql);
                while (dr.Read())
                {
                    History obj = new History();
                    obj.id = dr.GetInt32(0);
                    obj.author = dr.GetString(1);
                    obj.date = dr.GetDateTime(2);
                    obj.type = dr.GetString(3);
                    lst.Add(obj);
                }
                conn.CloseConect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        public History getLast()
        {
            History history = new History();
            DataTable dt = new DataTable();
            string sql = "select Top 1 * From Historytb order by Id DESC";
            dt = conn.getTable(sql);
            var lastHis = (from rw in dt.AsEnumerable()
                           select new History()
                           {
                               id = Convert.ToInt32(rw["id"]),
                               author = rw["author"].ToString(),
                               date = Convert.ToDateTime(rw["date"]),
                               type = rw["type"].ToString()
                           }).Single();
            return lastHis;
        }
    }
}
