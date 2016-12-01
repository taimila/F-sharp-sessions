namespace WebShop

module Item =
  let create name price =
    match name, price with
    | IsNotEmpty name, IsPositive price -> Some { Name = Name name; Price = Money price }
    | _ -> None
  