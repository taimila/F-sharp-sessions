namespace WebShop

module DataAccess =
  open System
  open System.Collections.Generic

  let storage = new Dictionary<Guid, ShoppingCart>()
  
  let createId () =
    Guid.NewGuid ()

  let getCart cartId =
    storage.[cartId]

  let saveCart cartId cart =
    storage.[cartId] <- cart
    cart