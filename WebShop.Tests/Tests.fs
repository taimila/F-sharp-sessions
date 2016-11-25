namespace WebShop

module Tests =
  open FsUnit
  open NUnit.Framework
  open ShoppingCart

  let laptop = (Item.create "Laptop" 1200m).Value

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