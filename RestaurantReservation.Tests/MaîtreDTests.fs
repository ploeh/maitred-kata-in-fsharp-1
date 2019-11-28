module Ploeh.Kata.MaîtreDTests

open System
open Xunit
open Swensen.Unquote
open Ploeh.Kata.MaîtreD

let aReservation =
    {
        Date = DateTime (2019, 11, 29, 12, 0, 0)
        Name = ""
        Email = ""
        Quantity = 1
    }

[<Theory>]
[<InlineData( 1,  true)>]
[<InlineData(13, false)>]
let ``Boutique restaurant`` quantity expected =
    expected =! canAccept 12 [] { aReservation with Quantity = quantity }