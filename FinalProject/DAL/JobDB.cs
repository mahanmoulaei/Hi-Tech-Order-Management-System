using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BLL;
using FinalProject.DAL;
using System.Data.SqlClient;

namespace FinalProject.DAL
{
    public static class JobDB
    {
        public static Job GetJob(int eId)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectJobID = new SqlCommand();

            cmdSelectJobID.CommandText = "SELECT * FROM Jobs " +
                                    "WHERE JobID = " + eId;
            cmdSelectJobID.Connection = connDB;
            SqlDataReader sqlReader = cmdSelectJobID.ExecuteReader();
            Job job = new Job();
            if (sqlReader.Read())
            {
                job.JobID = Convert.ToInt32(sqlReader["JobID"]);
                job.JobTitle = sqlReader["JobTitle"].ToString();
            }
            else
            {
                job = null;
            }

            return job;
        }
    }
}
