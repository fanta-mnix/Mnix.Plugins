using System;
using System.Threading.Tasks;

namespace Mnix.Plugins.Contacts
{
	public class Contact
	{
		public string FirstName { get; set; }		
		public string LastName { get; set; }		
		public string Phone { get; set; }		
		public string Email { get; set; }		
		public string Street { get; set; }		
		public string City { get; set; }		
		public string State { get; set; }		
		public string ZipCode { get; set; }		
		public string Country { get; set; }
	}

	public interface IMvxContactsManager
	{
		Task AddContact(Contact contact);
	}
}

