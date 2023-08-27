using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _keepsRepository;

    public KeepsService(KeepsRepository keepsRepository)
    {
        _keepsRepository = keepsRepository;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        int keepId = _keepsRepository.CreateKeep(keepData);
        Keep keep = GetKeepById(keepId, keepData.CreatorId);
        return keep;
    }

    internal Keep GetKeepByIdAndIncreaseVisits(int keepId, string userId)
    {
      Keep keep = GetKeepById(keepId, userId);
      keep.Views ++;
       _keepsRepository.UpdateKeeps(keep);
      return keep;
    }

    private Keep GetKeepById(int keepId, string userId)
    {
      Keep keep = _keepsRepository.GetKeepById(keepId);
      if(keep == null)
      {
        throw new Exception("Bad Id Keeper");
      }
      return keep;
    }

    internal List<Keep> GetKeeps()
    {
      List<Keep> keeps = _keepsRepository.GetKeeps();
      return keeps;
    }

    internal Keep EditKeep(Keep keepData, string userId)
    {
      Keep originalKeep = GetKeepById(keepData.Id, userId);
      if (originalKeep.CreatorId != userId)
      {
        throw new Exception("NOT YOUR KEEP");
      }
      originalKeep.Name = keepData.Name ?? originalKeep.Name;
      originalKeep.Description = keepData.Description ?? originalKeep.Description;
      originalKeep.Img = keepData.Img ?? originalKeep.Img;

      _keepsRepository.UpdateKeeps(originalKeep);
      return originalKeep;
    }

    internal string RemoveKeep(int keepId, string userId)
    {
      Keep keep = GetKeepById(keepId, userId);
      if (keep.CreatorId != userId)
      {
        throw new Exception("Not your keep to delete!");
      }
      _keepsRepository.RemoveKeep(keepId);
      return "Keep Removed!";
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      List<Keep> keeps = _keepsRepository.GetKeepsByProfileId(profileId);
      return keeps;
    }

    internal List<Keep> getAccountKeeps(string id)
    {
      List<Keep> keeps = _keepsRepository.getAccountKeeps(id);
      return keeps;
    }
}
