using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _profilesRepository;

    public ProfilesService(ProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    internal Profile GetProfile(string profileId)
    {
      Profile profile = _profilesRepository.GetProfile(profileId);
      return profile;
    }
}
