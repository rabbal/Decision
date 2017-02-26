namespace Decision.ViewModels.Home
{
   public  class BenchmarkViewModel
   {
       public string UsersCount { get; set; } = "۰";
       public string  NonApprovedApplicantsCount  { get; set; } = "۰";
        public string LastBackupDate { get; set; } = "../../..";
        public string JugesCount { get; set; } = "۰";
        public string ApprovedApplicantsCount { get; set; } = "۰";
        public string ArticlesCount { get; set; } = "۰";
        public string ArticleEvaluationsCount { get; set; } = "۰";
        public string DateOfLastTask { get; set; } = "۰۰/۰۰/۰۰";
        public string StatusOfLastTask { get; set; } = "-";
        public string RemainingDateToNextTask { get; set; } = "۰";
        public string SystemErrorsCount { get; set; } = "۰";
    }
}
