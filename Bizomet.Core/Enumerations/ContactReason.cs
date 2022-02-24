using System.ComponentModel.DataAnnotations;

namespace Bizomet.Core.Enums
{
	public enum ContactReason {
		[Display(ResourceType = typeof(Resources.General), Name = "CONTACT_REASON_GENERAL")]
		GENERAL = 0,

		[Display(ResourceType = typeof(Resources.General), Name = "CONTACT_REASON_TECHNICAL")]
		TECHNICAL = 1,

		[Display(ResourceType = typeof(Resources.General), Name = "CONTACT_REASON_SHARE_SUCCESS")]
		SHARE_SUCCESS = 2,

		[Display(ResourceType = typeof(Resources.General), Name = "CONTACT_REASON_FEEDBACK")]
		FEEDBACK = 3,

		[Display(ResourceType = typeof(Resources.General), Name = "CONTACT_REASON_REPORT_SPAM")]
		REPORT_SPAM = 4
	}
}
