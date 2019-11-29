module Ploeh.Kata.MaîtreD

open System

type Reservation = {
    Date : DateTime
    Name : string
    Email : string
    Quantity : int }

let canAccept capacity reservations { Quantity = q; Date = d } =
    let relevantReservations = Seq.filter (fun r -> r.Date = d) reservations
    let reservedSeats = Seq.sumBy (fun r -> r.Quantity) relevantReservations
    reservedSeats + q <= capacity