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
        private List<User> _users = new List<User>{ };
  
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
            // Retrieve all available users unfiltered
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
            return users.Where(u => u.FirstName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }


        private List<User> SearchByLastName(string searchValue, List<User> users)
        {
            return users.Where(u => u.LastName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private List<User> SearchByEmailAddress(string searchValue, List<User> users)
        {
            return users.Where(u => u.EmailAddress.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        //Update user

        public void Update(Guid id, User userUpdateInfo)
        {
            // Check if a user with the supplied id exists
            User userToUpdate = FetchAll()
                .Where(u => u.UserId == id)
                .FirstOrDefault();

            if (userToUpdate == null)
            {
                Console.WriteLine($"Invalid operation! No match was found for the id you supplied.");
                return;
            }

            // A matching user was found. Perform the requested update.
            if (userUpdateInfo.FirstName != null) userToUpdate.FirstName = userUpdateInfo.FirstName;
            if (userUpdateInfo.LastName != null) userToUpdate.LastName = userUpdateInfo.LastName;
            if (userUpdateInfo.EmailAddress != null) userToUpdate.EmailAddress = userUpdateInfo.EmailAddress;
            
            _context.SaveChanges();
        }

        //Delete user

        public void UserInfoDelete(Guid id)
        {
            // Check if a user with the supplied id exists
            User userInfoToDelete = FetchAll()
                .Where(u => u.UserId == id)
                .FirstOrDefault();

            if (userInfoToDelete == null)
            {
                Console.WriteLine($"Invalid operation! No match was found for the id you supplied.");
                return;
            }

            // A matching  was found. Perform the requested deletion.
            _context.Users.Remove(userInfoToDelete);
            _context.SaveChanges();
        }
    }
}
