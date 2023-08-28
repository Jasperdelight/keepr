using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepository;
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService, KeepsService keepsService)
    {
        _vaultKeepsRepository = vaultKeepsRepository;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
    }
    internal VaultKeep CreateVaultKeep(VaultKeep vkData, string userId)
    {
      Vault vault = _vaultsService.GetVaultById(vkData.VaultId, userId);
       if (vault.CreatorId !=userId)
      {
        throw new Exception("Not your Vault");
      }
      VaultKeep vaultKeepId = _vaultKeepsRepository.CreateVaultKeep(vkData);
     
      // if (vkData.vaultKeepId == vaultKeepId)
      // {
      //   throw new Exception("can not have more than one keep in your vault!");
      // }
      // VaultKeepViewModel vaultKeep = GetVaultKeepById(vaultKeepId, vkData.CreatorId);
    //  Keep keep = _keepsService.GetKeepById(vkData.KeepId, vaultKeep.Creator.Id);
    //  vaultKeep.Keeps =keep;

      return vaultKeepId;
    }

    internal List<VaultKeepViewModel> GetVaultKeepsByVaultId(int vaultId, string userId)
    {
      _vaultsService.GetVaultById(vaultId, userId);
      List<VaultKeepViewModel> vaultKeeps = _vaultKeepsRepository.GetVaultKeepsByVaultId(vaultId);
      return vaultKeeps;
    }

    internal VaultKeepViewModel GetVaultKeepById(int vaultKeepId, string userId)
    {
      VaultKeepViewModel vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
      if (vaultKeep == null)
      {
        throw new Exception("Bad vaultKeep Id");
      }
      return vaultKeep;
    }

    internal string RemoveVaultKeep(int vaultKeepId, string userId)
    {
      VaultKeepViewModel vaultKeep = GetVaultKeepById(vaultKeepId, userId);
      if (vaultKeep.CreatorId != userId)
      {
        throw new Exception("Not your VAULTKEEP");
      }
      _vaultKeepsRepository.RemoveVaultKeep(vaultKeepId);
      return "VaultKeep Removed!";
    }
}
