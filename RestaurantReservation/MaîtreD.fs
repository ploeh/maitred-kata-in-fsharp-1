module Ploeh.Kata.MaîtreD

open System

type Reservation = {
    Date : DateTime
    Name : string
    Email : string
    Quantity : int }

let canAccept _ _ { Quantity = q } = q = 1