using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.Core.Enums
{
    public enum ECollaborationUserStatus
    {
        [Display(Name = "Awaiting Confirmation")]
        AwaitingAdminConfirm = 0,
        [Display(Name = "Invited")]
        Invited = 1,
        [Display(Name = "Accepted")]
        Accepted = 2,
        [Display(Name = "Rejected")]
        Rejected = 3,
        [Display(Name = "Removed")]
        Removed = 4
    }
}
