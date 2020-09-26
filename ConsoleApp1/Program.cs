using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var user = new User("Joao", "joaomarcos@joao.com");
            user.SetPassword("123456", "123456");

            user.Validate();

            using(IUserRepository userRepository = new UserRepository())
            {
                userRepository.Create(user);
            }
            
            using(IUserRepository userRepository = new UserRepository())
            {
                var usr = userRepository.Get(user.Email);
                Console.WriteLine(usr.Email);
            }

            Console.ReadKey();
        }
    }
}
