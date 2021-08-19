using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBulbTunes.EFDataService.Models;

namespace TheBulbTunes.EFDataService.Services
{
    public class UserServices
    {

        private readonly TheBulbTunesContext _context = new TheBulbTunesContext();
        private List<User> _users = new List<User> { };
        
        
        

        //Create a user
        public void Create(string firstName, string lastName, string emailAddress)
        {
            User newUser = new User()
            {
                UserId = new Guid(),
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();         
        }
        // Fetch all users
        public List<User> FetchAll()
        {
            return _context.Users.ToList();
        }
       
        //Fetch users with filter(FirstName, LastName, EmailAddress,)
        public List<User> FetchWithFilter(string firstNameFilter = null, string lastNameFilter = null, string emailAddressFilter = null)
        {
            // Retrieve all available songs unfiltered
            _users = FetchAll();

            // If any filter is specified, apply that filter by calling its search method

            if (firstNameFilter != null)
                _users = SearchByFirstName(firstNameFilter, _users);

            if (lastNameFilter != null)
                _users = SearchByLastName(lastNameFilter, _users);

            if (emailAddressFilter != null)
                _users = SearchByEmailAddress(emailAddressFilter, _users);

            return _users;
        }

        private List<User> SearchByFirstName(string searchValue, List<User> users)
        {
            return users.Where(s => s.FirstName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private List<User> SearchByLastName(string searchValue, List<User> users)
        {
            return users.Where(s => s.LastName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private List<User> SearchByEmailAddress(string searchValue, List<User> users)
        {
            return users.Where(s => s.EmailAddress.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        //Update song

        public void Update(Guid id, User userWithNewInfo)
        {
            // Check if a song with the supplied id exists
            User userToUpdate = FetchAll()
                .Where(s => s.UserId == id)
                .FirstOrDefault();

            if (userToUpdate == null)
            {
                Console.WriteLine($"Invalid operation! No match was found for the id you supplied.");
                return;
            }

            // A matching song was found. Perform the requested update.
            if (userWithNewInfo.FirstName != null) userToUpdate.FirstName = userWithNewInfo.FirstName;
            if (userWithNewInfo.LastName != null) userToUpdate.LastName = userWithNewInfo.LastName;
            if (userWithNewInfo.EmailAddress != null) userToUpdate.EmailAddress = userWithNewInfo.EmailAddress;
            
            _context.SaveChanges();
        }

        //Delete song

        public void Delete(Guid id)
        {
            // Check if a song with the supplied id exists
            User userToDelete = FetchAll()
                .Where(s => s.UserId == id)
                .FirstOrDefault();

            if (userToDelete == null)
            {
                Console.WriteLine($"Invalid operation! No match was found for the id you supplied.");
                return;
            }

            // A matching song was found. Perform the requested deletion.
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }
    }
}
