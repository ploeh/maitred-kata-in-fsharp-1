module Ploeh.Kata.MaîtreD

open System

type Table = { Seats : int }

type Reservation = {
    Date : DateTime
    Name : string
    Email : string
    Quantity : int }

type TableConfiguration = Communal of int | Tables of Table list

let private fits r t = r.Quantity <= t.Seats

let canAccept config reservations ({ Quantity = q; Date = d } as r) =
    let contemporaneousReservations =
        Seq.filter (fun r -> r.Date.Date = d.Date) reservations
    match config with
    | Communal capacity ->
        let reservedSeats =
            Seq.sumBy (fun r -> r.Quantity) contemporaneousReservations
        reservedSeats + q <= capacity
    | Tables tables ->
        let rs = Seq.sort contemporaneousReservations
        let remainingTables = Seq.deleteFirstsBy fits (Seq.sort tables) rs
        Seq.exists (fits r) remainingTables