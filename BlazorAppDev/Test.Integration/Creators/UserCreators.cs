using BlazorAppDev.Server.Repositories.MyDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppDev.Test.Integration.Creators;

public class UserCreators
{
    private readonly MyDbContext _myDbContext;

    public UserCreators(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }
}
