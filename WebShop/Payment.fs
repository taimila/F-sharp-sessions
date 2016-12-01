namespace WebShop

module Payment =
  
  let createCashPayment amount =
    match amount with
    | IsPositive amount -> amount |> Money |> Cash |> Some
    | _ -> None

  let getAmount payment =
    match payment with
    | Cash amount -> amount
    | CreditCard (amount, _) -> amount
