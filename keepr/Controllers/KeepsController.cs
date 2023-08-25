using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
    private readonly Auth0Provider _auth;
    private readonly KeepsService _keepsService;

    public KeepsController(KeepsService keepsService, Auth0Provider auth)
    {
        _keepsService = keepsService;
        _auth = auth;
    }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody]Keep keepData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep keep = _keepsService.CreateKeep(keepData);
    return Ok(keep);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetKeeps()
  {
    try
    {
      List<Keep> keeps = _keepsService.GetKeeps();
    return Ok(keeps);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }

  [HttpGet("{keepId}")]
  public async Task<ActionResult<Keep>> GetKeepByIdAndIncreaseVisits(int keepId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Keep keep = _keepsService.GetKeepByIdAndIncreaseVisits(keepId, userInfo?.Id);
    return Ok(keep);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }
  [HttpPut("{keepId}")]
  [Authorize]
  public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep keepData, int keepId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      keepData.Id = keepId;
      Keep keep = _keepsService.EditKeep(keepData, userInfo.Id);
    return Ok(keep);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }

  [HttpDelete("{keepId}")]
  [Authorize]
  public async Task<ActionResult<string>> RemoveKeep(int keepId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _keepsService.RemoveKeep(keepId, userInfo.Id);
    return Ok(message);
    }
     catch(Exception e) 
    {
        return BadRequest(e.Message);
    }
  }
}


