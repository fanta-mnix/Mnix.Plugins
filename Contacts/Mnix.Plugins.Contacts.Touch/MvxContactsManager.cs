using System;
using MonoTouch.AddressBook;
using MonoTouch.Foundation;
using System.Threading.Tasks;
using System.Threading;

namespace Mnix.Plugins.Contacts.Touch
{
	//TODO localizar

	public class MvxContactsManager : IMvxContactsManager
	{
		#region IMvxContactsManager implementation

		public Task AddContact(Contact contact)
		{
			return Task.Run(delegate
			{
				NSError error = null;
				ABAddressBook addressBook = ABAddressBook.Create(out error);
				
				if(error != null)
				{
					if(error.Code == (int)ABAddressBookError.OperationNotPermittedByUserError)
					{
						throw new UnauthorizedAccessException("Accesso aos contatos negado.");
					}
					
					throw new NSErrorException(error);
				}
				
				switch(ABAddressBook.GetAuthorizationStatus())
				{
					case ABAuthorizationStatus.Authorized:
						_AddContact(addressBook, contact);
					break;
					
					case ABAuthorizationStatus.NotDetermined:
						ManualResetEvent semaphore = new ManualResetEvent(false);
						bool? set = null;

						addressBook.RequestAccess(delegate(bool arg1, NSError arg2) {
								set = arg1;
								error = arg2;
								semaphore.Set();
						});
						
						semaphore.WaitOne(10000);

						if(set == null)
						{
							throw new TimeoutException("Tempo de aceitação esgotado.");
						}
						
						if(error != null || !set.Value)
						{
							throw new NSErrorException(error);
						}
						else
						{
							_AddContact(addressBook, contact);
						}
					break;
					
					default:
						throw new UnauthorizedAccessException("Accesso aos contatos negado.");
				}
			});
		}

		#endregion
		
		private void _AddContact(ABAddressBook addressBook, Contact contact)
		{
			if(addressBook.GetPeopleWithName(contact.FirstName).Length > 0)
			{
				throw new ArgumentException("Contato já existe");
			}
		
	        ABPerson person = new ABPerson();	        
	        person.FirstName = contact.FirstName;
	        person.LastName = contact.LastName;
			
			if(!string.IsNullOrEmpty(contact.Phone))
			{
				ABMutableMultiValue<string> phones = new ABMutableStringMultiValue();
		        phones.Add(contact.Phone, ABPersonPhoneLabel.Mobile);
		        person.SetPhones(phones);
			}
	        
			if(!string.IsNullOrEmpty(contact.Email))
			{
				//TODO implementar
//				ABMutableMultiValue<string> emails = new ABMutableStringMultiValue();
//				emails.Add(contact.Phone, ABPersonAddressKey.)
			}
			
	        ABMutableDictionaryMultiValue addresses = new ABMutableDictionaryMultiValue();
	        NSMutableDictionary address = new NSMutableDictionary();
			
			if(!string.IsNullOrEmpty(contact.City))
			{
				address.Add(new NSString(ABPersonAddressKey.City), new NSString(contact.City));
			}
			
			if(!string.IsNullOrEmpty(contact.State))
			{
				address.Add(new NSString(ABPersonAddressKey.State), new NSString(contact.State));
			}
			
			if(!string.IsNullOrEmpty(contact.ZipCode))
			{
				address.Add(new NSString(ABPersonAddressKey.Zip), new NSString(contact.ZipCode));
			}
			
			if(!string.IsNullOrEmpty(contact.Street))
			{
				address.Add(new NSString(ABPersonAddressKey.Street), new NSString(contact.Street));
			}
			
			if(!string.IsNullOrEmpty(contact.Country))
			{
				address.Add(new NSString(ABPersonAddressKey.Country), new NSString(contact.Country));
			}
			
	        addresses.Add(address, new NSString("Home"));
	        person.SetAddresses(addresses);
	
	        addressBook.Add(person);
			addressBook.Save();
		}
	}
}

