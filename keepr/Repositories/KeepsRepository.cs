using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal int CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps(name, description, img, creatorId)
        VALUES(@Name, @Description, @Img, @CreatorId);
        SELECT LAST_INSERT_ID() 
        ;";
        int keeperId = _db.ExecuteScalar<int>(sql, keepData);
        // keepData.Id = keeperId;
        return keeperId;
    }

    internal Keep GetKeepById(int keepId)
    {
      string sql = @"
    SELECT
    k.*,
    acc.*
    FROM keeps k
    JOIN accounts acc ON acc.id = k.creatorId
    WHERE k.id = @keepId
      ;";
      Keep keep = _db.Query<Keep, Profile, Keep>(
      sql,
      (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new {keepId}
    ).FirstOrDefault();
    return keep;
    }

    internal List<Keep> GetKeeps()
    {
        string sql = @"
    SELECT
    k.*,
    COUNT(vk.id) AS kept,
    acc.*
    FROM keeps k
    LEFT JOIN vaultKeeps vk ON vk.keepId = k.id
    JOIN accounts acc ON acc.id = k.creatorId
    GROUP BY k.id
    ;";
    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(
      sql,
      (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }
    ).ToList();
    return keeps;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      string sql = @"
      SELECT
      k.*,
      acc.*
      from keeps k
      JOIN accounts acc ON acc.id = k.creatorId
      WHERE k.creatorId = @profileId
      ;";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(
        sql,
        (keep, profile) =>
        {
          keep.Creator = profile;
          return keep;
        }, new {profileId}
      ).ToList();
      return keeps;
    }

    internal void RemoveKeep(int keepId)
    {
      string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
      _db.Execute(sql, new {keepId});
    }

    internal void UpdateKeeps(Keep keep)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description,
      img = @Img,
      views = @Views
      WHERE id = @Id
      ;";
      _db.Execute(sql, keep);
    }
}
