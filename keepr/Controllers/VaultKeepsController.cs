using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth;

    public VaultKeepsController(Auth0Provider auth, VaultKeepsService vaultKeepsService)
    {
        _auth = auth;
        _vaultKeepsService = vaultKeepsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vkData)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        vkData.CreatorId = userInfo.Id;
        VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vkData);
      return Ok(vaultKeep);
      }
       catch(Exception e) 
      {
          return BadRequest(e.Message);
      }
    }
    [HttpDelete("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<string>> RemoveVaultKeep(int vaultKeepId)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        string message = _vaultKeepsService.RemoveVaultKeep(vaultKeepId, userInfo.Id);
      return Ok(message);
      }
       catch(Exception e) 
      {
          return BadRequest(e.Message);
      }
    }
}
