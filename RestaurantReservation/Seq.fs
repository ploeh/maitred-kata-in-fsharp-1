module Ploeh.Kata.Seq

let deleteFirstBy pred (xs : _ seq) = seq {
    let mutable found = false
    use e = xs.GetEnumerator ()
    while e.MoveNext () do
        if found
        then yield e.Current
        else if pred e.Current
            then found <- true
            else yield e.Current
    }

let deleteFirstsBy pred = Seq.fold (fun xs x -> deleteFirstBy (pred x) xs)