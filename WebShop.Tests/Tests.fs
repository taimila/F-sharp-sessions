namespace WebShop

module Tests =
  open FsUnit
  open NUnit.Framework
  open ShoppingCart

  let laptop = (Item.create "Laptop" 1200m).Value

  let payment = (Payment.createCashPayment 1200m).Value

  let shouldBeSuccess result =
    match result with
    | Success s -> s
    | Failure errors -> failwithf "Errors: %A" errors

  let shouldBeFailure result =
     match result with
    | Success s -> failwith "Expected failure, but was success"
    | Failure errors -> errors

  let andErrorMessageIs error errors =
    if errors |> List.contains error 
    then () 
    else failwithf "Expected error %A, but had %A" error errors


  let isPaid cart =
    match cart with
    | Paid _ -> true
    | _ -> false

  [<Test>]
  let ``Adding an item to cart increases count by one`` () =
    ShoppingCart.empty
    |> addItem laptop
    |> map getNumberOfItems
    |> shouldBeSuccess
    |> should equal 1

  [<Test>]
  let ``Removing an item from one item list makes cart empty`` () =
    ShoppingCart.empty
    |> addItem laptop
    |> bind (removeItem laptop)
    |> map getNumberOfItems
    |> shouldBeSuccess
    |> should equal 0

  [<Test>]
  let ``Get total returns total amount of the cart`` () =
    ShoppingCart.empty
    |> addItem laptop
    |> bind(addItem laptop)
    |> map getTotal
    |> shouldBeSuccess
    |> should equal (Money 2400m)

  [<Test>]
  let ``Total of empty cart is zero`` () =
    ShoppingCart.empty
    |> getTotal
    |> should equal (Money 0m)

  [<Test>]
  let ``Cart is paid after I paid it`` () =
    ShoppingCart.empty
    |> addItem laptop
    |> bind (payWith payment)
    |> map isPaid
    |> shouldBeSuccess
    |> should be True

  [<Test>]
  let ``Adding an item to paid cart will fail`` () =
    ShoppingCart.empty
    |> addItem laptop
    |> bind (payWith payment)
    |> bind (addItem laptop)
    |> shouldBeFailure
    |> andErrorMessageIs CantAddItemToPaidCart