import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class VaultKeepsService{
async createVaultKeep(vkData){
  const res = await api.post("api/vaultkeeps", vkData)
  // logger.log(res.data)
  Pop.toast(`Saved to vault!`)
  
}
async removeVaultKeep(keepId){
  const res = await api.delete(`api/vaultkeeps/${keepId}`)
  const vkIndex = AppState.activeVaultKeeps.findIndex(vk => vk.id == keepId)
  if (vkIndex == -1) {
    throw new Error("Bad ID ")
  }
  AppState.activeVaultKeeps.splice(vkIndex, 1)
}
}
export const vaultKeepsService = new VaultKeepsService()