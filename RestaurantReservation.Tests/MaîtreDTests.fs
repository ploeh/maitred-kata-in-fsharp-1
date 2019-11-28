module Ploeh.Kata.MaîtreDTests

open Xunit
open Swensen.Unquote
open Ploeh.Kata.MaîtreD

[<Fact>]
let ``Boutique restaurant`` () =
    test <@ canAccept 12 [] { Quantity = 1 } @>