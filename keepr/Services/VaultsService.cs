using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _vaultsRepository;

    public VaultsService(VaultsRepository vaultsRepository)
    {
        _vaultsRepository = vaultsRepository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
      int vaultId = _vaultsRepository.CreateVault(vaultData);
      Vault vault = GetVaultById(vaultId, vaultData.CreatorId);
      return vault;
    }

    internal Vault EditVault(Vault vData, string userId)
    {
      Vault originalVault = GetVaultById(vData.Id, userId);
      if (originalVault.CreatorId != userId)
      {
        throw new Exception("NOT YOUR VAULT");
      }
      originalVault.Name = vData.Name ?? originalVault.Name;
      originalVault.Description = vData.Description ?? originalVault.Description;
      originalVault.Img = vData.Img ?? originalVault.Img;
      originalVault.IsPrivate = vData.IsPrivate ?? originalVault.IsPrivate;
      _vaultsRepository.EditVault(originalVault);
      return originalVault;
    }

    internal List<Vault> GetAccountVaults(string userId)
    {
      List<Vault> vaults = _vaultsRepository.GetAccountVaults(userId);
      return vaults;
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
      Vault vault = _vaultsRepository.GetVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception("Bad Vault Id");
    }
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception("Bad vault Id 😌");
    }
      return vault;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId, string userId)
    {
      List<Vault> vaults = _vaultsRepository.GetVaultsByProfileId(profileId);
      vaults = vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == userId);
      return vaults;
    }

    internal string RemoveVault(int vaultId, string userId)
    {
      Vault vault = GetVaultById(vaultId, userId);
      if (vault.CreatorId != userId)
      {
        throw new Exception("NOT YOUR VAULT TO DELETE");
      }
      _vaultsRepository.RemoveVault(vaultId);
      return "Vault Removed!";
    }
}
