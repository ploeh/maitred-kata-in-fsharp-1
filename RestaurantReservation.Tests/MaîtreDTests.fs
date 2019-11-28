module Ploeh.Kata.MaîtreDTests

open Xunit
open Swensen.Unquote
open Ploeh.Kata.MaîtreD

[<Theory>]
[<InlineData( 1,  true)>]
[<InlineData(13, false)>]
let ``Boutique restaurant`` quantity expected =
    expected =! canAccept 12 [] { Quantity = quantity }