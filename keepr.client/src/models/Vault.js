export class Vault{
  constructor(data){
    this.creatorId = data.creatorId
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.isPrivate = data.isPrivate
    this.creator = data.creator
    this.keeps = data.keeps
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
  }
}
