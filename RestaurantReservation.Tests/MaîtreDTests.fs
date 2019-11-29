module Ploeh.Kata.MaîtreDTests

open System
open Xunit
open Swensen.Unquote
open Ploeh.Kata.MaîtreD

let d1 = DateTime (2023, 9, 14)
let d2 = DateTime (2023, 9, 15)
let aReservation =
    {
        Date = d1
        Name = ""
        Email = ""
        Quantity = 1
    }

let reserve (q, d) = { aReservation with Quantity = q; Date = d }

type BoutiqueTestCases () as this =
    inherit TheoryData<int, (int * DateTime) list, (int * DateTime), bool> ()
    do this.Add (12,                        [], ( 1, d1),  true)
       this.Add (12,                        [], (13, d1), false)
       this.Add (12,                        [], (12, d1),  true)
       this.Add ( 4,                 [(2, d1)], ( 3, d1), false)
       this.Add (10,                 [(2, d1)], ( 3, d1),  true)
       this.Add (10, [(3, d1);(2, d1);(3, d1)], ( 3, d1), false)
       this.Add ( 4,                 [(2, d2)], ( 3, d1),  true)

[<Theory; ClassData(typeof<BoutiqueTestCases>)>]
let ``Boutique restaurant`` (capacity, rs, r, expected) =
    let reservations = List.map reserve rs
    let actual = canAccept capacity reservations (reserve r)
    expected =! actual