using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly Auth0Provider _auth;
  private readonly VaultsService _vaultsService;
  private readonly VaultKeepsService _vaultKeepsService;

    public VaultsController(VaultsService vaultsService, Auth0Provider auth, VaultKeepsService vaultKeepsService)
    {
        _vaultsService = vaultsService;
        _auth = auth;
        _vaultKeepsService = vaultKeepsService;
    }

    [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vaultsService.CreateVault(vaultData);
    return Ok(vault);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}")]
  public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.GetVaultById(vaultId, userInfo?.Id);
    return Ok(vault);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }

  [HttpPut("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vData, int vaultId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vData.Id = vaultId;
      Vault vault = _vaultsService.EditVault(vData, userInfo.Id);
    return Ok(vault);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }

  [HttpDelete("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<string>> RemoveVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultsService.RemoveVault(vaultId, userInfo.Id);
    return Ok(message);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}/keeps")]
  public async Task<ActionResult<List<VaultKeep>>> GetVaultKeepsByVaultId(int vaultId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<VaultKeep> vaultKeeps = _vaultKeepsService.GetVaultKeepsByVaultId(vaultId, userInfo?.Id);
    return Ok(vaultKeeps);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }

}
