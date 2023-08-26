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
    internal VaultKeep CreateVaultKeep(VaultKeep vkData)
    {
      int vaultKeepId = _vaultKeepsRepository.CreateVaultKeep(vkData);
      VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId, vkData.CreatorId);
     Keep keep = _keepsService.GetKeepByIdAndIncreaseVisits(vkData.KeepId, vaultKeep.Creator.Id);
     vaultKeep.Keeps =keep;

      return vaultKeep;
    }

    internal List<VaultKeep> GetVaultKeepsByVaultId(int vaultId, string userId)
    {
      _vaultsService.GetVaultById(vaultId, userId);
      List<VaultKeep> vaultKeeps = _vaultKeepsRepository.GetVaultKeepsByVaultId(vaultId);
      return vaultKeeps;
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId, string userId)
    {
      VaultKeep vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
      if (vaultKeep == null)
      {
        throw new Exception("Bad vaultKeep Id");
      }
      return vaultKeep;
    }

    internal string RemoveVaultKeep(int vaultKeepId, string userId)
    {
      VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId, userId);
      if (vaultKeep.CreatorId != userId)
      {
        throw new Exception("Not your VAULTKEEP");
      }
      _vaultKeepsRepository.RemoveVaultKeep(vaultKeepId);
      return "VaultKeep Removed!";
    }
}
