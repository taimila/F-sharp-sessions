namespace WebShop

[<AutoOpen>]
module ErrorHandling = 
 
  type Error = 
    | CantAddItemToPaidCart
    | CantRemoveItemFromEmptyCart
    | CantRemoveItemFromPaidCart
    | CantPayEmptyCart
    | CantPayAlreadyPaidCart
    | PaymentWasNotSameAsCartTotal
    | CartNotFound of System.Guid

  type Result<'a> =
    | Success of 'a
    | Failure of Error list

  let ok a = Success a
  let fail error = Failure [error]
  
  let bind f result =
    match result with
    | Success s -> f s
    | Failure errors -> Failure errors

  let map f result = 
    match result with
    | Success s -> Success (f s)
    | Failure errors -> Failure errors

  let (>>=) x f = bind f x
