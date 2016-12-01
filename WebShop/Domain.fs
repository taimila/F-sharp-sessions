namespace WebShop

[<AutoOpen>]
module Domain =

  type Name = Name of string
  type CreditCardNumber = CreditCardNumber of string

  type Money = Money of decimal with
    static member (+) (x,y) = 
      let (Money a) = x
      let (Money b) = y
      Money (a + b) 
    static member Zero = Money 0m
     
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