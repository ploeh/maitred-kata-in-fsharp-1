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

type BoutiqueTestCases () as this =
    inherit TheoryData<int, bool> ()
    do this.Add ( 1,  true)
       this.Add (13, false)
       this.Add (12,  true)

[<Theory; ClassData(typeof<BoutiqueTestCases>)>]
let ``Boutique restaurant`` quantity expected =
    expected =! canAccept 12 [] { aReservation with Quantity = quantity }