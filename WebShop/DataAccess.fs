namespace WebShop

module DataAccess =
  open System
  open System.Collections.Generic

  let storage = new Dictionary<Guid, ShoppingCart>()
  
  let createId () =
    Guid.NewGuid ()

  let getCart cartId =
    if storage.ContainsKey(cartId)
    then storage.[cartId] |> ok
    else CartNotFound cartId |> fail 

  let saveCart cartId cart =
    storage.[cartId] <- cart
    cart |> ok