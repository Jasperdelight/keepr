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
      // vkData.Id = vaultKeepId;
      vkData.Id = vaultKeepId;
      return vkData;
    }

    internal VaultKeepViewModel GetVaultKeepById(int vaultKeepId)
    {
      string sql = @"
      SELECT
      acc.*,
      k.*,
      vk.id AS vaultKeepId
      FROM vaultKeeps vk
      JOIN accounts acc ON acc.id = vk.creatorId
      JOIN keeps k ON vk.keepId = k.id
      WHERE vk.id = @vaultKeepId
      ;";
      VaultKeepViewModel vaultKeep = _db.Query< Profile, VaultKeepViewModel, VaultKeepViewModel>(
        sql,
        ( profile, vaultKeep) =>
        {
          vaultKeep.CreatorId = profile.Id;
          // vaultKeep.Id = keep.Id;
          return vaultKeep;
        },
        new {vaultKeepId}
      ).FirstOrDefault();
      return vaultKeep;
    }

    internal List<VaultKeepViewModel> GetVaultKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT
      acc.*,
      k.*,
      vk.id AS vaultKeepId
      FROM vaultKeeps vk
      JOIN accounts acc ON acc.id = vk.creatorId
      JOIN keeps k ON vk.keepId = k.id
      WHERE vk.vaultId = @vaultId
      ;";
      List <VaultKeepViewModel> vaultKeeps = _db.Query<Profile, VaultKeepViewModel, VaultKeepViewModel>(
        sql,
        ( profile, vaultKeep) =>
        {
          vaultKeep.Creator = profile;
          //  vaultKeep.VaultKeepId= keep.Id;
          //  vaultKeep.Name = keep.Name;
          //  vaultKeep.Img = keep.Img;
          //  vaultKeep.CreatedAt = keep.CreatedAt;
          //  vaultKeep.Kept = keep.Kept;
          //  vaultKeep.Views = keep.Views;
          //  vaultKeep.Description = keep.Description;
          //  vaultKeep.KeepId = keep.Id;

          return vaultKeep;
        },
        new {vaultId}
      ).ToList();
      return vaultKeeps;
    }

    internal void RemoveVaultKeep(int vaultKeepId)
    {
      string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1 ;";
      _db.Execute(sql, new{vaultKeepId});
    }
}
