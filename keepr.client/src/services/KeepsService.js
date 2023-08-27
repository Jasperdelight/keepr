import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { VaultKeep } from "../models/VaultKeep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{
async getKeeps(){
  const res = await api.get("api/keeps")
  // logger.log(res.data)
  AppState.keeps = res.data.map(k=> new Keep(k))
}

async setActiveKeep(keepId){
  const res = await api.get(`api/keeps/${keepId}`)
  AppState.activeKeep = new Keep(res.data)
}
async createKeep(formData){
  const res = await api.post("api/keeps", formData)
  AppState.keeps.push(new Keep(res.data))
  logger.log(res.data)
}
async getProfileKeeps(profileId){
  const res = await api.get(`api/profiles/${profileId}/keeps`)
  AppState.profileKeeps = res.data.map(k=> new Keep(k))
}
  async getVaultKeepsByVaultId(vaultId){
  const res = await api.get(`api/vaults/${vaultId}/keeps`)
  AppState.activeVaultKeeps = res.data.map(vk=> new VaultKeep(vk))
  logger.log(AppState.activeVaultKeeps)
}

}
export const keepsService = new KeepsService()