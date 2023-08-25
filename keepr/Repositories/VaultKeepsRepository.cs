using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vkData)
    {
      string sql = @"
      INSERT INTO vaultKeeps(creatorId, vaultId, keepId)
      VALUES(@CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID()
      ;";
      int vaultKeepId = _db.ExecuteScalar<int>(sql, vkData);
      vkData.Id = vaultKeepId;
      return vkData;
    }

    internal List<VaultKeep> GetVaultKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT
      vk.*,
      acc.*,
      k.*
      FROM vaultKeeps vk
      JOIN accounts acc ON acc.id = vk.creatorId
      JOIN keeps k ON k.id = vk.keepId
      WHERE vk.vaultId = @vaultId
      ;";
      List <VaultKeepViewModel> vaultKeeps = _db.Query<VaultKeep, Profile, Keep, VaultKeep>(
        sql,
        (vaultKeep, profile, keep) =>
        {
          vaultKeep.Creator = profile;
          vaultKeep.Keeps = keep;
          return vaultKeep;
        },
        new {vaultId}
      ).ToList();
      return vaultKeeps;
    }
}
