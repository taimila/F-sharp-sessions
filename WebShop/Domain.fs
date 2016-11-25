namespace WebShop

[<AutoOpen>]
module Domain =

  type Name = Name of string
  type CreditCardNumber = CreditCardNumber of string
  type Money = Money of decimal

  type Item = {
    Name: Name
    Price: Money
  }

  type Payment =
    | Cash of Money
    | CreditCard of Money * CreditCardNumber

  type ShoppingCart =
    | Empty
    | Active of Item list
    | Paid of Item list * Payment

[<AutoOpen>]
module Helpers =
  let (|IsPositive|_|) num =
    if num <= 0m
    then None
    else Some num

  let (|IsNotEmpty|_|) (str:string) =
    if str.Length = 0
    then None
    else Some str

module Item =
  let create name price =
    match name, price with
    | IsNotEmpty name, IsPositive price -> Some { Name = Name name; Price = Money price }
    | _ -> None
  
module ShoppingCart = 
 
  let empty = Empty

  // Item -> ShoppingCart -> ShoppingaCart
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