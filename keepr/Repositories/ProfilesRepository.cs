using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Repositories;

public class ProfilesRepository
{
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Profile GetProfile(string profileId)
    {
      string sql = @" SELECT * FROM accounts WHERE id = @profileId
      ;";
      return _db.QueryFirstOrDefault<Profile>(sql, new {profileId});
    }
}
