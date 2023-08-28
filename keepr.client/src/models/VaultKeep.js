import { Vault } from "./Vault";

export class VaultKeep{
constructor(data){
  this.id = data.id
  this.keepId = data.keepId
  this.vaultId = data.vaultId
  this.creator = data.creator
  this.updatedAt = data.updatedAt
  this.createdAt = data.createdAt
  this.name = data.name
  this.description = data.description
  this.vaultKeepId = data.vaultKeepId
  this.img = data.img
}
}