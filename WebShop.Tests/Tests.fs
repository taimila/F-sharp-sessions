module Tests

open FsUnit
open NUnit.Framework

[<Test>]
let placeholderTest () =
  1 |> should equal 1