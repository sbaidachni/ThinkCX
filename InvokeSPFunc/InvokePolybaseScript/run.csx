#r "System.Data"

using System.Data;
using System.Data.SqlClient;

public static void Run(Stream myBlob, string name, TraceWriter log)
{
    try
    {
        SqlConnection conn=new SqlConnection(System.Environment.GetEnvironmentVariable("sqlConn", EnvironmentVariableTarget.Process));
        SqlCommand comm=new SqlCommand("dbo.test_func",conn);
        comm.CommandType=CommandType.StoredProcedure;
        comm.Parameters.Add("@blobName",name);
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();
        log.Info("done");
    }
    catch(Exception ex)
    {
        log.Info(ex.Message);
    }
}