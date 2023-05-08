using OnmpApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmpApp.Data;

public static partial class Database
{
    public static async Task<bool> UserEmailExists(string email)
    {
        var cnt = await db.Table<User>().Where(x => x.Email == email).CountAsync();
        if (cnt > 0)
            return true;

        return false;
    }

    public static async Task<bool> UserCreate(string email)
    {
        if (await UserEmailExists(email))
            return false;

        _ = await db.InsertAsync(new User(email));
        return true;
    }

}
