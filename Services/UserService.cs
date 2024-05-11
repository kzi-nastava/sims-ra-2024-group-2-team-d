using BookingApp.Domain.Model;
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
        public UserRepository UserRepository { get; set; }

        public UserService() 
        { 
            UserRepository = new UserRepository();
        }

        public User GetById(int id)
        {
            return UserRepository.GetById(id);
        }
    }
}
