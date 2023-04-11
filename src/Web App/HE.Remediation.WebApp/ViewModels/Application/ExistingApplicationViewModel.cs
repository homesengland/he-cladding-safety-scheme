﻿using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ApplicationViewModel
    {
        public Guid ApplicationId { get; set; }

        public string ApplicationNumber { get; set; }

        public string UniqueBuildingName { get; set; }

        public DateTime DateCreated { get; set; }

        public EApplicationStage Stage { get; set; }

        public EApplicationStatus Status { get; set; }

        public bool OpenTasks { get; set; }
    }

    public class ExistingApplicationViewModel
    {
        public List<ApplicationViewModel> ApplicationList { get; set; }

        public int PageCount { get; set; }

        public int CurrentPage { get; set; }

        public bool UseEllipses { get; set; }

        public ExistingApplicationViewModel()
        {
            ApplicationList = new List<ApplicationViewModel>();
            PageCount = 1;
            CurrentPage = 1;
            UseEllipses = false;            
        }
    }


}
