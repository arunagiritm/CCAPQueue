using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCAPModel
{
    public class CCAPJob
    {
        public int id { get; set; }
        public string SolutionName { get; set; }
        public string SolutionPath { get; set; }
        public string ProjectId { get; set; }
        public string ArtifactId { get; set; }
        public string ProjectName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ProjectKey { get; set; }
        public string Language { get; set; }
        public string TestPattern { get; set; }
        public string Status { get; set; }
        public string Log { get; set; }
        public string JavaSource { get; set; }
        public string JavaBinary { get; set; }
        public string OptionalProperties { get; set; }
        
    }

    public class CCAPJobs
    {
        public List<CCAPJob> Jobs { get; set; }
        public DateTime ExecutionDate { get; set; }
        public string AnalysisType { get; set; }
        public string JobQLocation { get; set; }
        public string ProjectLang { get; set; }
        
    }

    public static class CCAPJobFolders
    {
        public static  string INQUEUE = "In Queue";
        public static string PROCESSED = "Processed";
        public static string COMPLETED = "Completed";
        public static string CANCELLED = "Cancelled";
        public static string FAILED = "Failed";
        public static string LOG = "Log";
        public static string JOBS = "Jobs";
    }

    

}
