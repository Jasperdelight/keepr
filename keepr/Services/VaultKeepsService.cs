using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepository;
  private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
    {
        _vaultKeepsRepository = vaultKeepsRepository;
        _vaultsService = vaultsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vkData)
    {
      VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vkData);
      return vaultKeep;
    }

    internal List<VaultKeep> GetVaultKeepsByVaultId(int vaultId, string userId)
    {
      _vaultsService.GetVaultById(vaultId, userId);
      List<VaultKeep> vaultKeeps = _vaultKeepsRepository.GetVaultKeepsByVaultId(vaultId);
      return vaultKeeps;
    }
}
