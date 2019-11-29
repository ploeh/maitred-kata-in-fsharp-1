module Ploeh.Kata.MaîtreD

open System

type Reservation = {
    Date : DateTime
    Name : string
    Email : string
    Quantity : int }

let canAccept _ reservations { Quantity = q } =
    q <> 13 && Seq.isEmpty reservations