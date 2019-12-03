module Ploeh.Kata.MaîtreDTests

open System
open Xunit
open Swensen.Unquote
open Ploeh.Kata.MaîtreD

let d1 = DateTime (2023,  9, 14)
let d2 = DateTime (2023,  9, 15)
let d3 = DateTime (2024,  6,  7)
let d4 = DateTime (2023, 10, 22, 18, 0, 0)
let aReservation =
    {
        Date = d1
        Name = ""
        Email = ""
        Quantity = 1
    }

type Int32 with
    member x.minutes = TimeSpan.FromMinutes (float x)
    member x.hours = TimeSpan.FromHours (float x)
    member x.days = TimeSpan.FromDays (float x)

type Double with
    member x.hours = TimeSpan.FromHours x

let table s = Discrete s

let reserve (q, d) = { aReservation with Quantity = q; Date = d }

type BoutiqueTestCases () as this =
    inherit TheoryData<int, (int * DateTime) list, (int * DateTime), bool> ()
    do this.Add (12,                        [], ( 1, d1            ),  true)
       this.Add (12,                        [], (13, d1            ), false)
       this.Add (12,                        [], (12, d1            ),  true)
       this.Add ( 4,                 [(2, d1)], ( 3, d1            ), false)
       this.Add (10,                 [(2, d1)], ( 3, d1            ),  true)
       this.Add (10, [(3, d1);(2, d1);(3, d1)], ( 3, d1            ), false)
       this.Add ( 4,                 [(2, d2)], ( 3, d1            ),  true)
       this.Add ( 4,                 [(2, d1)], ( 3, d1.AddHours 1.), false)

[<Theory; ClassData(typeof<BoutiqueTestCases>)>]
let ``Boutique restaurant`` (capacity, rs, r, expected) =
    let reservations = List.map reserve rs
    let actual = canAccept (1 .days) (Communal capacity) reservations (reserve r)
    expected =! actual

type HauteCuisineTestCases () as this =
    inherit TheoryData<int list, (int * DateTime) list, (int * DateTime), bool> ()
    do this.Add ([2;2;4;4],        [], (4, d3),  true)
       this.Add ([2;2;4;4],        [], (5, d3), false)
       this.Add (  [2;2;4], [(2, d3)], (4, d3),  true)
       this.Add (  [2;2;4], [(3, d3)], (4, d3), false)

[<Theory; ClassData(typeof<HauteCuisineTestCases>)>]
let ``Haute cuisine`` (tableSeats, rs, r, expected) =
    let tables = List.map table tableSeats
    let reservations = List.map reserve rs

    let actual = canAccept (1 .days) (Tables tables) reservations (reserve r)

    expected =! actual

type SecondSeatingsTestCases () as this =
    inherit TheoryData<TimeSpan, int list, (int * DateTime) list, (int * DateTime), bool> ()
    do this.Add (
        2 .hours,
        [2;2;4],
        [(4, d4)],
        (3, d4.Add (2 .hours)),
        true)
       this.Add (
        2.5 .hours,
        [2;4;4],
        [(2, d4);(1, d4.AddMinutes 15.);(2, d4.Subtract (15 .minutes))],
        (3, d4.AddHours 2.),
        false)
       this.Add (
        2.5 .hours,
        [2;4;4],
        [(2, d4);(2, d4.Subtract (15 .minutes))],
        (3, d4.AddHours 2.),
        true)
       this.Add (
        2.5 .hours,
        [2;4;4],
        [(2, d4);(1, d4.AddMinutes 15.);(2, d4.Subtract (15 .minutes))],
        (3, d4.AddHours 2.25),
        true)

[<Theory; ClassData(typeof<SecondSeatingsTestCases>)>]
let ``Second seatings`` (dur, tableSeats, rs, r, expected) =
    let tables = List.map table tableSeats
    let reservations = List.map reserve rs

    let actual = canAccept dur (Tables tables) reservations (reserve r)

    expected =! actual

type AlternativeTableConfigurationTestCases () as this =
    inherit TheoryData<Table list, int list, int, bool> ()
    do this.Add (
        [Discrete 4; Discrete 1; Discrete 2; Group [2;2;2]],
        [3;1;2],
        2,
        true)
       this.Add (
        [Discrete 4; Discrete 1; Discrete 2; Group [2;2;2]],
        [3;1;2],
        7,
        false)
       this.Add (
        [Discrete 4; Discrete 1; Discrete 2; Group [2;2;2]],
        [3;1;2;1],
        4,
        true)

[<Theory; ClassData(typeof<AlternativeTableConfigurationTestCases>)>]
let ``Alternative table configurations`` (tables, rs, r, expected) =
    let res i = reserve (i, d4)
    let reservations = List.map res rs

    let actual = canAccept (1 .days) (Tables tables) reservations (res r)

    expected =! actual