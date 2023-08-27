import { AppState } from "../AppState"
import { Profile } from "../models/Profile"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService{
async getProfile(profileId){
  const res = await api.get(`api/profiles/${profileId}`)
  logger.log(res.data)
  AppState.profile = new Profile(res.data)
}
}
export const profilesService = new ProfilesService()