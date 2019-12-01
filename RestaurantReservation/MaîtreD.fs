module Ploeh.Kata.MaîtreD

open System

type Table = { Seats : int }

type Reservation = {
    Date : DateTime
    Name : string
    Email : string
    Quantity : int }

let canAccept tables reservations { Quantity = q; Date = d } =
    let capacity = tables |> Seq.map (fun t -> t.Seats) |> Seq.max
    let relevantReservations =
        Seq.filter (fun r -> r.Date.Date = d.Date) reservations
    let reservedSeats = Seq.sumBy (fun r -> r.Quantity) relevantReservations
    reservedSeats + q <= capacity