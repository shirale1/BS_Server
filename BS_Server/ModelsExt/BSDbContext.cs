using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

public partial class BSDbContext : DbContext
{
    public User? GetUser(string email, string pas)
    {
        return this.Users.Where(u => u.Email == email && u.Password == pas).FirstOrDefault(); 
    }

    public User? GetUser(string email)
    {
        return this.Users.Where(u => u.Email == email).FirstOrDefault();
    }

    public Parent? GetParent(int userId)
    {
        return this.Parents.Where(p => p.ParentId == userId)
            .Include(p => p.ParentNavigation).FirstOrDefault();
    }

    public Babysiter? GetBabySiter(int userId)
    {
        return this.Babysiters.Where(p => p.BabysiterId == userId)
            .Include(p => p.BabysiterNavigation).FirstOrDefault();
    }
}
