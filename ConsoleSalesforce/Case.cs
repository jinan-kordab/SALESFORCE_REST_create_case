using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSalesforce
{
	class Case
	{
		public string Status;			//The status of the case, such as “New,” “Closed,” or “Escalated.” This field directly controls the IsClosed flag.Each predefined Status value implies an IsClosed flag value.For more information, see CaseStatus.
		public string Reason;			//The reason why the case was created, such as Instructions not clear, or User didn’t attend training.
		public string Subject;			//The subject of the case. Limit: 255 characters
		public string SuppliedName;		//The name that was entered when the case was created. This field can't be updated after the case has been created. Label is Name.
		public string Priority;			//The importance or urgency of the case, such as High, Medium, or Low.


		public string Type;				//The type of case, such as Feature Request or Question.
		public string Origin;			//The source of the case, such as Email, Phone, or Web. Label is Case Origin

		//THE FIELDS BELOW NEED SPECIAL VALUES

		//public string OwnerId;			//ID of the contact who owns the case.
		//public string ParentId;			//The ID of the parent case in the hierarchy. The label is Parent Case.
		//public string AccountId;		//ID of the account associated with this case	
		//public string ContactId;		//ID of the associated Contact

		public string Description;		//A text description of the case. Limit: 32 KB
		public bool IsEscalated;		//Indicates whether the case has been escalated (true) or not. A case's escalated state does not affect how you can use a case, or whether you can query, delete, or update it. You can set this flag via the API. Label is Escalated.
		public string SuppliedEmail;	//The email address that was entered when the case was created. This field can't be updated after the case has been created. Label is Email.If your organization has an active auto-response rule, SuppliedEmail is required when creating a case via the API. Auto-response rules use the email in the contact specified by ContactId. If no email address is in the contact record, the email specified here is used.
		public string SuppliedPhone;	//The phone number that was entered when the case was created. This field can't be updated after the case has been created. Label is Phone.
		public string SuppliedCompany;  //The company name that was entered when the case was created. This field can't be updated after the case has been created. Label is Company.

	}
}
