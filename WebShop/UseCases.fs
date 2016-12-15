namespace WebShop

module UseCases = 
  open ShoppingCart

  let createCart createId saveCart =
    let id = createId ()
    empty
    |> saveCart id 
    |> ignore
    id

  let addItem getCart saveCart cartId item =
    getCart cartId
    >>= addItem item
    >>= saveCart cartId
  
  let removeItem getCart saveCart cartId item =
    getCart cartId
    >>= removeItem item
    >>= saveCart cartId
     
  let payCart getCart saveCart cartId payment =
    getCart cartId
    >>= payWith payment
    >>= saveCart cartId
    
  let getTotalOf getCart cartId =
    getCart cartId
    |> map getTotal

  let create () =
    createCart DataAccess.createId DataAccess.saveCart 

  let add =
    addItem DataAccess.getCart DataAccess.saveCart

  let remove =
    removeItem DataAccess.getCart DataAccess.saveCart

  let pay =
    payCart DataAccess.getCart DataAccess.saveCart

  let total =
    getTotalOf DataAccess.getCart