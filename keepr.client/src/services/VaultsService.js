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
  // logger.log(res.data)
  AppState.profileVaults = res.data.map(v=> new Vault(v))
}
async setActiveVault(vaultId){
  const res = await api.get(`api/vaults/${vaultId}`)

  AppState.activeVault = new Vault(res.data)
}
setMyActiveVault(vaultId){
  let foundVault = AppState.myVaults.find(v=>v.id ==vaultId)
  AppState.activeVault = new Vault(foundVault)
}
setActiveVaultKeep(vaultId){
  const foundVault = AppState.myVaults.find(v=> v.id == vaultId)
  AppState.activeVault = new Vault(foundVault)

}
async removeVault(vaultId){
  const res = await api.delete(`api/vaults/${vaultId}`)
  const vaultIndex = AppState.myVaults.findIndex(v => v.id == vaultId)
  if (vaultIndex == -1) {
    throw new Error("This is a bad id")
  }
  AppState.myVaults.splice(vaultIndex, 1)
  // logger.log('vault removed?', res.data)
}

}
export const vaultsService = new VaultsService()