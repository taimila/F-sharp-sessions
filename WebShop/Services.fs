namespace WebShop

module Services = 
  open Suave
  open System
  open Suave.Successful
  open Suave.Filters
  open Suave.Operators
  open Suave.RequestErrors
  open ErrorHandling

  let resultToResponse result =
    match result with
    | Success x -> x |> sprintf "%A" |> OK
    | Failure errors -> errors |> sprintf "%A" |> BAD_REQUEST

  let toString x = 
    x.ToString()

  (* To keep this exsample small we assume Guid.parse and Item creation always go ok. *)

  let create () =
    UseCases.create ()
    |> toString
    |> OK

  let getTotalService cartId =
    Guid.Parse(cartId)
    |> UseCases.total
    |> resultToResponse

  let addItem (cartId, itemName, (itemPrice:string)) =
    let price = Convert.ToDecimal(itemPrice)
    let guid = Guid.Parse cartId
    let item = (Item.create itemName price).Value
    let result = UseCases.add guid item
    resultToResponse result

  let routes = 
    choose [
      GET  >=> pathScan "/total/%s"     getTotalService
      POST >=> pathScan "/add/%s/%s/%s" addItem
      POST >=> path     "/create"       >=> request (fun _ -> create ())
    ]

  let startApi () =
    startWebServer defaultConfig routes