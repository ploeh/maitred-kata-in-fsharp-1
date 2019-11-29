module Ploeh.Kata.MaîtreD

open System

type Reservation = {
    Date : DateTime
    Name : string
    Email : string
    Quantity : int }

let canAccept capacity reservations { Quantity = q } =
    let reservedSeats = Seq.sumBy (fun r -> r.Quantity) reservations
    reservedSeats + q <= capacity