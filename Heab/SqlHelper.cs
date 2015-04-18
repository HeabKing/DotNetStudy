// 2015-04-02 20:03:03
// 2015-04-10 20:19:34   ���ֶ�connStr�ĳ���public ��readonly
// 2015-04-11 13:12:15  ���TestConn()���� �����TODO:SqlHelperInit.SetConfigConnStr
// 2015-04-12 14:42:43  ȥ����SqlHelperInit, ʹ�þ�̬���캯�������쳣���ֶ�ʵ���˾�̬�����쳣��ʼ��.
//                      ʵ�ֹ���:   1. Ĭ�ϴ������ļ��ж�ȡ���ݿ������ַ���; 
//                                  2. ����ֱ�Ӹ�ֵConStr����Ĭ�����ݿ������ַ���;
//                                  3. ��������, �쳣InvalidateOperationException

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Heab.SQL
{
    /// <summary>
    /// ���ݿ��SQL����
    /// ʹ������: 
    ///     1. ConnStrΪ�����ļ��б����е�connstr�����ַ���
    ///     2. ConnStrΪnull, ��ʱû�������ļ�, �����ֶ�����ConnStr
    ///     3. InvalidOperationException�쳣, ˵����ʱ����������������
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// Ĭ��: �����ļ��л�ȡ�����ַ���connstr, ��������ļ���û������, �쳣ΪNullReferenceException, Ȼ���쳣ΪTypeInitializationException, �ڹ��캯�����Ѿ�����, ����Ϊnull
        /// ����: ֱ�Ӹ�ֵ(null˵��û������connstr, ����Ϊ��Ĭ������)
        /// </summary>
        public static string ConnStr;

        static SqlHelper()
        {
            try
            {
                ConnStr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
            }
            // �����ʱ�����ļ���û������connstr���ݿ������ַ���
            catch (System.NullReferenceException)
            {
                ConnStr = null;
            }
        }

        /// <summary>
        /// �������ݿ�����, ���ɹ��ᱨ���쳣
        /// </summary>
        public static void TestConn()
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
            }
        }

        /// <summary>
        /// ִ�зǲ�ѯ���
        /// ������Ӱ�������
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    // ��һ��
                    //foreach (SqlParameter param in parameters)
                    //{
                    //    cmd.Parameters.Add(param);
                    //}
                    // �ڶ���
                    cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // SQL��������һ��һ��һ�е�����
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        // ִ�в�ѯ����Ƚ��ٵ�SQL
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }
        #region ������ݿ���C#������null��ת��
        /// <summary>
        /// ���������null, �򷵻����ݿ��е�"null"(��: DBNull.Value), ���򷵻ض�����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object ToDbValue(object obj)
        {
            if (obj == null)
            {
                return DBNull.Value;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// ȡ�����ݿ��е�����, �����SQL�е�Null��ת����C#�е�Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object FromDbValue(object obj)
        {
            if (obj == DBNull.Value)
            {
                return null;
            }
            else
            {
                return obj;
            }
        }
        #endregion
    }
}
