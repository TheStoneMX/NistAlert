using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BioTechSysNistAlert.Properties;
using BioTechSys.AnsiNist;
using BioTechSys.Mail.Pop3;
using System.Collections.ObjectModel;


namespace BioTechSysNistAlert.HelperCode
{
    class DBHelper
    {
        private string _strConnect = string.Empty;

        public DBHelper()
		{

		}
        public static Collection<dbEmail> GetEmailsFromDB()
        {
            Collection<dbEmail>  Emails = new Collection<dbEmail>();
            SqlConnection conn = null;
            SqlDataReader _DataReader;

            try
            {
                Settings settings = Settings.Default;
                conn = new SqlConnection(settings.ConnectionString);
                conn.Open();
                string strSQL = "SELECT * FROM SNSP_EMAILS";

                SqlCommand myCommand = new SqlCommand(strSQL, conn);
                _DataReader = myCommand.ExecuteReader();

                while(_DataReader.Read())
                {
                    Emails.Add(new dbEmail( _DataReader["DE"].ToString(), 
                                            _DataReader["PARA"].ToString(), 
                                            _DataReader["SUBJECT"].ToString(),
                                            _DataReader["DATE"].ToString(), 
                                           (byte[])_DataReader["Attachment"], 
                                            _DataReader["AttachmentName"].ToString(),
                                            _DataReader["TOT"].ToString(), 
                                            _DataReader["NCP"].ToString(),
                                            _DataReader["Status"].ToString(),
                                            Convert.ToInt64(_DataReader["EmailDI"].ToString())));

                }
                 _DataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("BioTechSys", ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }

            return Emails;
          
        }

        public static Int64 InsertEmailInfo(Pop3Client pop3, 
                                            string nameAttachment, byte[] buf, 
                                            string TOT, string NCP, string Status, string rawEmail )
        {
            Int64 maxID = -1;
            SqlConnection conn = null;
            SqlCommand insert = null;

            try
            {
                Settings settings = Settings.Default;

                string cmdstr = "INSERT INTO SNSP_EMAILS( DE, PARA,   Subject, Date, Attachment, AttachmentName, TOT, NCP, Status, rawEmail )" +
                                                "values ( @DE, @PARA, @Subject, @Date, @Attachment, @AttachmentName, @TOT, @NCP, @Status, @rawEmail )";

                conn = new SqlConnection(settings.ConnectionString);
                conn.Open();
                insert = new SqlCommand(cmdstr, conn);

                insert.Parameters.AddWithValue("@DE", pop3.From);
                insert.Parameters.AddWithValue("@PARA", pop3.To);
                insert.Parameters.AddWithValue("@Subject", pop3.Subject);
                insert.Parameters.AddWithValue("@Date", DateTime.Now);//Parse( pop3.Date));
                insert.Parameters.AddWithValue("@Attachment", buf);
                insert.Parameters.AddWithValue("@AttachmentName", nameAttachment);
                insert.Parameters.AddWithValue("@TOT", TOT);
                insert.Parameters.AddWithValue("@NCP", NCP);
                insert.Parameters.AddWithValue("@Status", Status );
                insert.Parameters.AddWithValue("@rawEmail", rawEmail);

                insert.ExecuteNonQuery();

                SqlCommand select = new SqlCommand("SELECT @@IDENTITY", conn);
                SqlCommand com = new SqlCommand();
                com.Connection = conn;
                com.CommandText = "SELECT @@Identity";
                Object x = com.ExecuteScalar();
                maxID = System.Convert.ToInt64(x);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BioTechSys" );
            }
            finally
            {
                conn.Close();
            }
            return maxID;
        }

        public static  void DeleteEmail(Int64 IndexKey)
        {
            SqlConnection conn = null;
            SqlCommand delete = null;
            Settings settings = Settings.Default;


            try
            {
                string cmdstr = string.Format("DELETE FROM SNSP_EMAILS WHERE (EmailDI = {0})", IndexKey);
                conn = new SqlConnection(settings.ConnectionString);
                conn.Open();
                delete = new SqlCommand(cmdstr, conn);

                delete.ExecuteScalar();

            }
            catch (Exception ex)
            {
                MessageBox.Show("BioTechSys", ex.Message);
            }
           
        }
    }
}
