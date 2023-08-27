using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth;
  private readonly ProfilesService _profilesService;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;

    public ProfilesController(Auth0Provider auth, AccountService accountService, ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
    {
        _auth = auth;
        _accountService = accountService;
        _profilesService = profilesService;
        _keepsService = keepsService;
        _vaultsService = vaultsService;
    }

    [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfile(string profileId)
  {
    try
    {
      Profile profile = _profilesService.GetProfile(profileId);
    return Ok(profile);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
  {
    try
    {
      List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId);
    return Ok(keeps);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _vaultsService.GetVaultsByProfileId(profileId, userInfo?.Id);
    return Ok(vaults);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }



}
