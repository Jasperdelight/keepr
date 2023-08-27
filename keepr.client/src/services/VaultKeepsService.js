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
}
export const vaultKeepsService = new VaultKeepsService()