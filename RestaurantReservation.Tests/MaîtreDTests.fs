module Ploeh.Kata.MaîtreDTests

open System
open Xunit
open Swensen.Unquote
open Ploeh.Kata.MaîtreD

let aReservation =
    {
        Date = DateTime (2023, 9, 14)
        Name = ""
        Email = ""
        Quantity = 1
    }

type BoutiqueTestCases () as this =
    inherit TheoryData<int, int list, int, bool> ()
    do this.Add (12,      [],  1,  true)
       this.Add (12,      [], 13, false)
       this.Add (12,      [], 12,  true)
       this.Add ( 4,     [2],  3, false)
       this.Add (10,     [2],  3,  true)
       this.Add (10, [3;2;3],  3, false)

[<Theory; ClassData(typeof<BoutiqueTestCases>)>]
let ``Boutique restaurant`` capacity reservatedSeats quantity expected =
    let rs =
        List.map (fun s -> { aReservation with Quantity = s }) reservatedSeats
    let actual =
        canAccept capacity rs { aReservation with Quantity = quantity }
    expected =! actual