import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService{
async createVaultKeep(vkData){
  const res = await api.post("api/vaultkeeps", vkData)
  logger.log(res.data)
}
}
export const vaultKeepsService = new VaultKeepsService()