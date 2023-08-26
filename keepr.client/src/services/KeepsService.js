import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
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

}
export const keepsService = new KeepsService()