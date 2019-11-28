module Ploeh.Kata.MaîtreD

type Reservation = { Quantity : int }

let canAccept _ _ { Quantity = q } = q = 1