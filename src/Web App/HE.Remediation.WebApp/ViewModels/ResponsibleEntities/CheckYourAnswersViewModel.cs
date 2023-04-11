﻿using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class CheckYourAnswersViewModel
    {
        public Guid ApplicationId { get; set; }
        public Guid ResponsibleEntityId { get; set; }
        public int? RepresentationTypeId { get; set; }
        public int? ResponsibleEntityTypeId { get; set; }
        public int? BuildingRelationshipId { get; set; }

        public int? RepresentativeResponsibleEntityTypeId { get; set; }
        public bool? RepresentativeUKBased { get; set; }
        public string RepresentativeIndividualOrCompany { get; set; }
        public string RepresentativeCompanyDetails { get; set; }
        public string RepresentativeDetails { get; set; }
        public string RepresentativeAddress { get; set; }

        public int ResponsibleEntityRelationId { get; set; }
        public string ResponsibleEntityRelation { get; set; }
        public int? ResponsibleEntityCompanyTypeId { get; set; }
        public string ResponsibleEntityCompanyType { get; set; }
        public int? ResponsibleEntityCompanySubTypeId { get; set; }
        public bool? ResponsibleEntityRegisteredInUK { get; set; }
        public string ResponsibleEntityCompanyDetails { get; set; }
        public string ResponsibleEntityDetails { get; set; }
        public string ResponsibleEntityCompanyAddress { get; set; }
        public string ResponsibleEntityPrimaryContact { get; set; }

        public string ResponsibleEntityAuthorisationEvidence { get; set; }
        public int? ResponsibleEntitySharedOwners { get; set; }
        public bool? ResponsibleEntityClaimingGrant { get; set; }
        public bool? ConfirmedNotViable { get; set; }
        public List<EvidenceFile> EvidenceFiles { get; set; }

        public Guid? FreeholderId { get; set; }
        public int? FreeholderResponsibleEntityTypeId { get; set; }
        public string FreeholderIndividualOrCompany { get; set; }
        public string FreeholderCompanyDetails { get; set; }
        public string FreeholderDetails { get; set; }
        public string FreeholderAddress { get; set; }

        public string ReturnUrl { get; set; }
    }
}
