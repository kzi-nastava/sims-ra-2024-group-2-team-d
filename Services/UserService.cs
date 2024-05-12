using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class UserService
    {
        public IUserRepository UserRepository { get; set; }

        public UserService(IUserRepository userRepository) 
        { 
            UserRepository = userRepository;
        }

        public User GetById(int id)
        {
            return UserRepository.GetById(id);
        }
    }
}
