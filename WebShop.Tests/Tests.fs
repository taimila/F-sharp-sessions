namespace WebShop

module Tests =
  open FsUnit
  open NUnit.Framework
  open ShoppingCart

  let laptop = (Item.create "Laptop" 1200m).Value

  let payment = (Payment.createCashPayment 1200m).Value

  [<Test>]
  let ``Adding an item to cart increases count by one`` () =
    ShoppingCart.empty
    |> addItem laptop
    |> getNumberOfItems
    |> should equal 1

  [<Test>]
  let ``Removing an item from one item list makes cart empty`` () =
    ShoppingCart.empty
    |> addItem laptop
    |> removeItem laptop
    |> getNumberOfItems
    |> should equal 0

  [<Test>]
  let ``Get total returns total amount of the cart`` () =
    ShoppingCart.empty
    |> addItem laptop
    |> addItem laptop
    |> getTotal
    |> should equal (Money 2400m)

  [<Test>]
  let ``Total of empty cart is zero`` () =
    ShoppingCart.empty
    |> getTotal
    |> should equal (Money 0m)

  let isPaid cart =
    match cart with
    | Paid (items, payment) -> true
    | _ -> false

  [<Test>]
  let ``Cart is paid after I paid it`` () =
    ShoppingCart.empty
    |> addItem laptop
    |> payWith payment
    |> isPaid
    |> should be True