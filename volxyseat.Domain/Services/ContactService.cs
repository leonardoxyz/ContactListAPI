using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using volxyseat.Domain.Core.Data;
using volxyseat.Domain.Models;

namespace volxyseat.Domain.Services
{
    public class ContactService
    {
        private readonly IRepository<Contact, Guid> _contactRepository;
        private readonly IUnityOfWork _unityOfWork;

        public ContactService(IRepository<Contact, Guid> contactRepository, IUnityOfWork unityOfWork)
        {
            _contactRepository = contactRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<Contact> GetContactByIdAsync(Guid id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            await _contactRepository.AddAsync(contact);
            await _unityOfWork.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> UpdateContactAsync(Guid id, Contact contact)
        {
            var existingContact = await _contactRepository.GetByIdAsync(id);
            if (existingContact == null)
            {
                return null; // Contato não encontrado
            }

            existingContact.Name = contact.Name;
            await _contactRepository.UpdateAsync(existingContact);
            await _unityOfWork.SaveChangesAsync();
            return existingContact;
        }

        public async Task<bool> DeleteContactAsync(Guid id)
        {
            try
            {
                var contact = await _contactRepository.GetByIdAsync(id);
                if (contact == null)
                {
                    return false;
                }

                await _contactRepository.DeleteAsync(id);
                await _unityOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
