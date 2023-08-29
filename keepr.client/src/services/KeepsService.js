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
async getMyKeeps(){
  const res = await api.get('/account/keeps')
  // logger.log('my Keeps', res.data)
  AppState.myKeeps = res.data.map(k=> new Keep(k))
}

async setActiveKeep(keepId){
  const res = await api.get(`api/keeps/${keepId}`)
  AppState.activeKeep = new Keep(res.data)
}
async createKeep(formData){
  const res = await api.post("api/keeps", formData)
  AppState.keeps.push(new Keep(res.data))
  AppState.myKeeps.push(new Keep(res.data))
  
  // logger.log(res.data)
}
async getProfileKeeps(profileId){
  const res = await api.get(`api/profiles/${profileId}/keeps`)
  AppState.profileKeeps = res.data.map(k=> new Keep(k))
}
  async getVaultKeepsByVaultId(vaultId){
  const res = await api.get(`api/vaults/${vaultId}/keeps`)
  AppState.activeVaultKeeps = res.data.map(vk=> new VaultKeep(vk))
  // logger.log(AppState.activeVaultKeeps)
}
  async removeKeep(keepId){
  const res = await api.delete(`api/keeps/${keepId}`)
  const myKeepIndex = AppState.myKeeps.findIndex(k => k.id == keepId)
  if (myKeepIndex == -1) {
    throw new Error("This is a bad id")
  }
  AppState.myKeeps.splice(myKeepIndex, 1)
  // logger.log('keep removed?', res.data)
  const keepIndex = AppState.keeps.findIndex(k => k.id == keepId)
  if (keepIndex == -1) {
    throw new Error("This is a bad id")
  }
  AppState.keeps.splice(keepIndex, 1)
}

}
export const keepsService = new KeepsService()