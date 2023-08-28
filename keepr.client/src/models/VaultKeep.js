import { Vault } from "./Vault";

export class VaultKeep{
constructor(data){
  this.id = data.id
  this.keepId = data.keepId
  this.vaultId = data.vaultId
  this.creator = data.creator
  this.keeps = data.keeps
  this.updatedAt = data.updatedAt
  this.createdAt = data.createdAt
}
}