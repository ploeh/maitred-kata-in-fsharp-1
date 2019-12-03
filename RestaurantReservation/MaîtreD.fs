module Ploeh.Kata.MaîtreD

open System

type Table = Discrete of int | Group of int list

type Reservation = {
    Date : DateTime
    Name : string
    Email : string
    Quantity : int }

type TableConfiguration = Communal of int | Tables of Table list

let private fits r = function
    | Discrete seats -> r.Quantity <= seats
    | Group tables -> r.Quantity <= List.sum tables

let private isContemporaneous
    (seatingDur : TimeSpan)
    (candidate : Reservation)
    (existing : Reservation) =
    let aSeatingBefore = candidate.Date.Subtract seatingDur
    let aSeatingAfter = candidate.Date.Add seatingDur
    aSeatingBefore < existing.Date && existing.Date < aSeatingAfter

let private allot r = function
    | Discrete seats ->
        if r.Quantity <= seats
        then None
        else Some (Discrete seats)
    | Group tables -> Some (Group tables)

let private allocate (tables : Table seq) r = seq {
    let mutable found = false
    use e = tables.GetEnumerator ()
    while e.MoveNext () do
        if found
        then yield e.Current
        else
            match allot r e.Current with
            | None ->
                found <- true
            | Some t -> yield t
    }

let canAccept (seatingDur : TimeSpan) config reservations r =
    let contemporaneousReservations =
        Seq.filter (isContemporaneous seatingDur r) reservations
    match config with
    | Communal capacity ->
        let reservedSeats =
            Seq.sumBy (fun r -> r.Quantity) contemporaneousReservations
        reservedSeats + r.Quantity <= capacity
    | Tables tables ->
        let rs = Seq.sort contemporaneousReservations
        let remainingTables = Seq.fold allocate (Seq.sort tables) rs
        Seq.exists (fits r) remainingTables