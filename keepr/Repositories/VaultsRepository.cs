using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal int CreateVault(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults(name, description, img, creatorId)
      VALUES(@Name, @Description, @Img, @CreatorId);
      SELECT LAST_INSERT_ID()
      ;";
      int vaultId = _db.ExecuteScalar<int>(sql, vaultData);
      return vaultId;
    }

    internal void EditVault(Vault originalVault)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description,
      img = @Img,
      isPrivate = @IsPrivate
      WHERE id = @Id
      ;";
      _db.Execute(sql, originalVault);
    }

    internal Vault GetVaultById(int vaultId)
    {
      string sql = @"
      SELECT
      v.*,
      acc.*
      FROM vaults v
      JOIN accounts acc ON acc.id = v.creatorId
      WHERE v.id = @vaultId
      ;";
      Vault vault = _db.Query<Vault, Profile, Vault>(
        sql,
        (vault, profile)=>
        {
          vault.Creator = profile;
          return vault;
        }, new {vaultId}
      ).FirstOrDefault();
      return vault;
    }

    internal void RemoveVault(int vaultId)
    {
      string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
      _db.Execute(sql, new {vaultId});
    }
}
