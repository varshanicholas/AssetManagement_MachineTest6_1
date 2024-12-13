using MachineTest6_1.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MachineTest6_1.Repository
{
    public class LoginRepository : ILoginRepository
    {


        //virtual DBcontext

        private readonly AssetsContext _context;

        //Dependency Injection

        public LoginRepository(AssetsContext context)
        {
            _context = context;
        }

        #region Validate user

        public async Task<UserLogin> ValidateUser(string username, string password)
        {
            try
            {
                if (_context != null)
                {
                    UserLogin? dbUser = await _context.UserLogins.FirstOrDefaultAsync(
                           u => u.Username == username && u.Password == password
                           );
                    if (dbUser != null)
                    {
                        return dbUser;
                    }
                }

                //return an empty list if _context is null.

                return null;
            }

            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion

        //Generate JWT token

        // GET: Get all user registrations
        public async Task<IEnumerable<UserRegistration>> GetAllRegistrations()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.UserRegistrations


                        .ToListAsync();
                }

                // Return an empty list if context is null
                return new List<UserRegistration>();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        // GET: Get a user registration by ID
        public async Task<UserRegistration> GetRegistrationById(int registrationId)
        {
            try
            {
                if (_context != null)
                {
                    //find the employee by id 

                    var registration = await _context.UserRegistrations
                        .FirstOrDefaultAsync(e => e.RegistrationId == registrationId);
                    return registration;
                }

                return null;

            }

            catch (Exception ex)
            {
                return null;
            }
        }

        // POST: Insert a new user registration
        public async Task<UserRegistration> AddRegistration(UserRegistration registration)
        { 

            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration), "User data is null");
            }

            try
            {

                await _context.UserRegistrations.AddAsync(registration);
                await _context.SaveChangesAsync();


                var registeredUser = await _context.UserRegistrations
                    .FirstOrDefaultAsync(u => u.RegistrationId == registration.RegistrationId);

                return registeredUser;
            }
            catch (DbUpdateException ex)
            {

                throw new InvalidOperationException("An error occurred while registering the user.", ex);
            }
            catch (Exception ex)
            {

                throw new Exception("An unexpected error occurred.", ex);
            }

        }


        // PUT: Update an existing user registration
        public async Task<UserRegistration> UpdateRegistration(UserRegistration registration)
        {

            try
            {
                if (registration == null)
                {
                    throw new ArgumentNullException(nameof(registration), "registration data is null");
                }

                // Ensure the context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                var result = await _context.UserRegistrations.FromSqlRaw(
                    "EXEC UpdateUserRegistration @RegistrationId, @FirstName, @LastName, @Age, @Gender, @Address, @PhoneNumber",
                    new SqlParameter("@RegistrationId", registration.RegistrationId),
                    new SqlParameter("@FirstName", registration.FirstName ?? (object)DBNull.Value),
                    new SqlParameter("@LastName", registration.LastName ?? (object)DBNull.Value),
                    new SqlParameter("@Age", registration.Age),
                    new SqlParameter("@Gender", registration.Gender ?? (object)DBNull.Value),
                    new SqlParameter("@Address", registration.Address ?? (object)DBNull.Value),
                    new SqlParameter("@PhoneNumber", registration.PhoneNumber ?? (object)DBNull.Value)
                ).ToListAsync();

                // Return the updated asset or null if not found
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return null;
            }

        }
    }

}
