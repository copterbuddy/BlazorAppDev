using BlazorAppDev.Server.Repositories.MyDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Integration.Creators;

public class UserCreator
{
    private readonly MyDbContext? _myDbContext;

    public UserCreator(MyDbContext _myDbContext)
    {
        _myDbContext = _myDbContext;
    }
}
