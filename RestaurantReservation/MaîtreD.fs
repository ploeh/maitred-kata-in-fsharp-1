module Ploeh.Kata.MaîtreD

open System

type Reservation = {
    Date : DateTime
    Name : string
    Email : string
    Quantity : int }

let canAccept capacity reservations { Quantity = q } =
    let reservedSeats =
        match Seq.tryHead reservations with
        | Some r -> r.Quantity
        | None -> 0
    reservedSeats + q <= capacity