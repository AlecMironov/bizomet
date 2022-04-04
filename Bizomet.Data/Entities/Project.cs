using Bizomet.Core.Enums;

namespace Bizomet.Data.Entities
{
	public class Project : EntityBase
	{
		public string UserId { get; set; }

		public ApplicationUser User { get; set; }

		public DateTime RequestDate { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public InterviewCondition InterviewCondition { get; set; } = InterviewCondition.FREE_INTERVIEW;

		public string InterviewConditionComment { get; set; }

		public InterviewResult InterviewResult { get; set; }

		public string InterviewResultComment { get; set; }

		/// Would you like to be contacted by Media Assistant?
		/// Media assistant(anyone who has ability to create a professionally looking text 
		/// article for your website, newspaper, or a person who can help to create professionally edited video or audio podcast).
		/// Usually, you have to pay for this service separately.
		public bool WishContactedByMediaAssistant { get; set; } = false;

		public FinancialType MediaAssistantFinancialService { get; set; } = FinancialType.NOT_SET;

		/// Would you like to be contacted by Promoter?
		/// Media promoter (this category of service includes SMM specialists, people or organizations with an ability to promote your interview in mass media/social networks). 
		/// Depends on collaboration it could be a service you will have to pay for.
		public bool WishContactedByPromoter { get; set; } = false;

		public FinancialType PromoterFinancialService { get; set; } = FinancialType.NOT_SET;

		/// Would you like to be contacted by Producer?
		/// Producer (this option comes handy in case if you want someone to do everything on this platform for you.
		/// Consider producer as a general manager who will set up everything for in terms of your interviews).
		/// Usually paid separately.
		public bool WishContactedByProducer { get; set; } = false;

		public FinancialType ProducerFinancialService { get; set; } = FinancialType.NOT_SET;

		public string Location { get; set; }

		public bool RemoteLocation { get; set; }

		public DateTime DueDate { get; set; }

		public ICollection<ProjectAttachment> ProjectAttachments { get; set; }

		public bool IsPublished { get; set; }

		public bool IsArchived { get; set; }
	}
}