using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Data;
using System;
using System.Linq;

namespace SpaUserControl.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbDataContext _context = new DbDataContext();

        public UserRepository(DbDataContext context)
        {
            this._context = context;
        }

        public User Get(Guid Id)
        {
            return _context.Users.Where(x => x.Id == Id).FirstOrDefault();
        }
        public User Get(string email)
        {
            return _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }
        public void Create(User user)
        {
            _context.Users.Add(user);
            Commit();
        }
        public void Update(User user)
        {
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            Commit();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            Commit();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
