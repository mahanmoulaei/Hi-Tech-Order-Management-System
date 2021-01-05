using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL;

namespace FinalProject.BLL
{
    public class Job
    {
        private int jobID;
        private string jobTitle;
        

        public Job(int jobID, string jobTitle)
        {
            this.jobID = jobID;
            this.jobTitle = jobTitle;
        }

        public Job()
        {
        }

        public int JobID { get => jobID; set => jobID = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
        
        public Job GetJob(int eId)
        {
            return JobDB.GetJob(eId);
        }
    }
}
