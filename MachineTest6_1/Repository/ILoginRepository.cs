using MachineTest6_1.Model;

namespace MachineTest6_1.Repository
{
    public interface ILoginRepository
    {
        //Get  user details by using username and password

        public Task<UserLogin> ValidateUser(string username, string password);

        public Task<IEnumerable<UserRegistration>> GetAllRegistrations();
        public Task<UserRegistration> GetRegistrationById(int registrationId);
        public Task<UserRegistration> AddRegistration(UserRegistration registration);
        public Task<UserRegistration> UpdateRegistration(UserRegistration registration);

    }
}
