namespace WebShop

module ShoppingCart = 
 
  let empty = Empty

  let addItem item cart =
    match cart with
    | Empty -> Active [item]
    | Active items -> Active (item :: items)
    | Paid _ -> failwith "Bug!"

  let removeItem item cart = 
    match cart with
    | Empty -> failwith "Bug!"
    | Paid _ -> failwith "Bug!"
    | Active items ->
      let remaining = items |> List.filter (fun i -> i <> item)
      if remaining.Length = 0
      then Empty
      else Active remaining

  let getNumberOfItems cart = 
    match cart with
    | Empty -> 0
    | Active items -> items.Length
    | Paid (items,_) -> items.Length

  let getTotal cart =
    let sum = List.sumBy (fun i -> i.Price)
    match cart with
    | Empty -> Money 0m
    | Active items -> sum items
    | Paid (items, _) -> sum items

  let payWith payment cart =
    match cart with
    | Empty -> failwith "Bug!"
    | Paid _ -> failwith "Bug!"
    | Active items ->
      let cartAmount = getTotal cart
      let paymentAmount = Payment.getAmount payment
      if paymentAmount = cartAmount
      then Paid (items, payment)
      else failwith "Bug!"
