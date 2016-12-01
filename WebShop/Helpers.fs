namespace WebShop

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

