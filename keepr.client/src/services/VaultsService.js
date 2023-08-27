import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService{
async createVault(formData){
  const res = await api.post('api/vaults', formData)
  // logger.log(res.data)
  AppState.myVaults.push(new Vault(res.data))
}
async getMyVaults(){
  const res = await api.get('/account/vaults')
  AppState.myVaults = res.data.map(v=>new Vault(v))
}
async getProfileVaults(profileId){
  const res = await api.get(`api/profiles/${profileId}/vaults`)
  AppState.profileVaults = res.data.map(v=> new Vault(v))
}

}
export const vaultsService = new VaultsService()