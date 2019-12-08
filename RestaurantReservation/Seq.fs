module Ploeh.Kata.Seq

let consume quantity =
    let go (acc, xs) x =
        if quantity <= acc
        then (acc, Seq.append xs (Seq.singleton x))
        else (acc + x, xs)
    Seq.fold go (0, Seq.empty) >> snd