using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace DEV_CSharp
{
    static class Common
    {
        /// <summary>
        /// Đọc nội dung theo số dòng
        /// </summary>
        /// <param name="filePath">Đường dẫn</param>
        /// <param name="lineNumber">Số dòng cần đọc nội dung</param>
        /// <returns></returns>
        public static string ReadTextFile(string filePath, int lineNumber)
        {
            string result = string.Empty;
            try
            {
                result = File.ReadLines(filePath).Skip(lineNumber - 1).Take(1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error " + ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// Đọc nội dung theo 1 khoảng dòng
        /// </summary>
        /// <param name="filePath">Đường dẫn</param>
        /// <param name="lineStart">Dòng bắt đầu đọc file</param>
        /// <param name="lineEnd">Dòng kết thúc đọc file</param>
        /// <returns>List text</returns>
        public static List<String> ReadTextFile(string filePath, int lineStart, int lineEnd)
        {
            List<String> lst = new List<string>();
            try
            {
                lst = File.ReadLines(filePath).Skip(lineStart - 1).Take(lineEnd - lineStart + 1).ToList();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show("Error " + ex.ToString());
            }
            return lst;
        }
        /// <summary>
        /// Đếm số dòng trong text file
        /// </summary>
        /// <param name="filePath">Đường dẫn file</param>
        /// <returns>Số dòng</returns>
        public static long counterlineTextfile(string filePath)
        {
            var lineCount = 0;
            try
            {

                using (var reader = File.OpenText(filePath))
                {
                    while (reader.ReadLine() != null)
                    {
                        lineCount++;
                    }
                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show("Error " + ex.ToString());
            }

            return lineCount;
            //return File.ReadLines(filePath).Count();
        }
        /// <summary>
        /// Gửi list email
        /// </summary>
        /// <param name="lstEmail">Danh sách email cần gửi</param>
        /// <param name="content">Nội dung cần gửi</param>
        /// <returns>True nếu mail dc gửi, False nếu mail không được gửi</returns>
        public static bool senListmail(List<string> lstEmail, StringBuilder content)
        {
            try
            {
                SmtpClient Smtp_Server = new SmtpClient();
                MailMessage e_mail = new MailMessage();
                Smtp_Server.UseDefaultCredentials = false;
                Smtp_Server.Credentials = new System.Net.NetworkCredential("neverdie000000@gmail.com", "abc@#123");
                Smtp_Server.Port = 587;
                Smtp_Server.EnableSsl = true;
                Smtp_Server.Host = "smtp.gmail.com";
                foreach (var item in lstEmail)
                {
                    e_mail.From = new MailAddress("neverdie000000@gmail.com");
                    MailAddress em = new MailAddress(item);
                    e_mail.To.Add(em);
                    e_mail.Subject = "Alarm";
                    e_mail.IsBodyHtml = false;
                    e_mail.Body = content.ToString();
                    Smtp_Server.Send(e_mail);
                    return true;
                }
            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("Silent");
            }
            return false;
        }
        /// <summary>
        /// Gửi mail
        /// </summary>
        /// <param name="_subject">Tiêu đề email</param>
        /// <param name="_body">Nội dung email</param>
        /// <param name="_from">Mail người gửi</param>
        /// <param name="_to">Mail người nhận</param>
        /// <param name="_cc">Mail CC</param>
        /// <param name="_bcc">Mail BCC</param>
        public static void SendMail(string _subject, string _body, MailAddress _from, MailAddress _to, List<MailAddress> _cc, List<MailAddress> _bcc = null)
        {
            try
            {
                SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
                mailClient.UseDefaultCredentials = false;
                mailClient.EnableSsl = true;
                mailClient.Credentials = new System.Net.NetworkCredential("neverdie000000@gmail.com", "abc@#123");
                MailMessage msgMail;
                msgMail = new MailMessage();
                msgMail.From = _from;
                msgMail.To.Add(_to);
                _cc.ForEach(r =>
                {
                    msgMail.CC.Add(r);
                });
                if (_bcc != null)
                {
                    _bcc.ForEach(r => { msgMail.Bcc.Add(r); });
                }
                msgMail.Subject = _subject;
                msgMail.Body = _body;
                msgMail.IsBodyHtml = true;
                mailClient.Send(msgMail);
                msgMail.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Đọc file text chuyển ra list
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> readText2List(string filePath)
        {
            List<string> lst = new List<string>();
            //StreamReader sr;
            try
            {
                //sr = File.OpenText(filePath);
                //while (!sr.EndOfStream)
                //{
                //    lst.Add(sr.ReadLine());
                //}
                lst = File.ReadLines(filePath).ToList();
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(@"Error");
            }

            return lst;
        }
        /// <summary>
        /// Kiểm tra xem có phải email không
        /// </summary>
        /// <param name="email">Mail check</param>
        /// <returns>True nếu là email, false nếu không phải</returns>
        public static bool isEmail(string email)
        {
            Regex regex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            return regex.IsMatch(email);
        }
        /// <summary>
        /// Tìm tên đầy đủ của phần mềm theo 1 cụm từ
        /// </summary>
        /// <param name="NameSoft">Cụm từ cần tìm</param>
        /// <returns>Tên đầy đủ của phần mềm</returns>
        public static string FindApplication(string NameSoft)
        {
            string astring = null;
            foreach (Process p in Process.GetProcesses())
            {
                string h = p.MainWindowTitle.ToString(); //lay tung title cua tung process
                if (h.Length > 0)
                {
                    if (h.Contains(NameSoft))
                    {
                        astring = h;
                    }
                }
                //p.Refresh();
            }
            return astring;
        }
        /// <summary>
        /// Kiểm tra ứng dụng thông qua tên
        /// </summary>
        /// <param name="nameSoft">Tên ứng dụng</param>
        /// <returns>True nếu ứng dụng đang bật, False nếu ngược lại</returns>
        public static bool IsRunning(string nameSoft)
        {
            return Process.GetProcesses().Where(p => p.MainWindowTitle.Contains(nameSoft)).FirstOrDefault() != default(Process);
        }
        /// <summary>
        /// Lấy nội dung giữa 2 string
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string getBetween(string input, string from, string to)
        {
            int iFrom = input.IndexOf(from) + from.Length;
            int iTo = input.LastIndexOf(to);
            return input.Substring(iFrom, iTo - iFrom);
        }
        /// <summary>
        /// Tìm kiếm Email trong file text
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Danh sách email</returns>
        public static List<string> findEmail(string filePath)
        {
            List<string> result = new List<string>();
            try
            {
                result = File.ReadAllLines(filePath).ToList().Where(r => isEmail(r)).ToList(); ;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error " + ex.ToString());
            }
            return result;
        }
        /// <summary>
        /// Tìm kiếm theo chuỗi từ khóa
        /// </summary>
        /// <param name="filePath">Đường dẫn</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static string Search(string filePath, string keyword)
        {
            List<string> lstKeyword = new List<string>() { "temp", "nhiet do" };
            string result = string.Empty;
            foreach (var item in lstKeyword)
            {
                result = File.ReadLines(filePath)
                    .SkipWhile(r => !r.Contains(item))
                    .Skip(1)
                    .SkipWhile(h => h.Equals(""))
                    .Take(1).FirstOrDefault();
                if (result != null)
                {
                    return result;
                }
            }
            return result;
        }
        /// <summary>
        /// Lấy danh sách string 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<string> EverythingBetween(this string source, string start, string end)
        {
            var results = new List<string>();

            string pattern = string.Format(
                "{0}({1}){2}",
                Regex.Escape(start),
                ".+?",
                 Regex.Escape(end));

            foreach (Match m in Regex.Matches(source, pattern))
            {
                results.Add(m.Groups[1].Value.Trim());
            }

            return results;
        }
    }
}
