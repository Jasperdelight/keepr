using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Models;

public class VaultKeep
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string CreatorId { get; set; }
  public int VaultId { get; set; }
  public int KeepId { get; set; }
  // public Keep Keeps { get; set; }
  // public Profile Creator { get; set; }

}
