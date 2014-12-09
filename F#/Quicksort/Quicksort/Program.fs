
// NOTE: If warnings appear, you may need to retarget this project to .NET 4.0. Show the Solution
// Pad, right-click on the project node, choose 'Options --> Build --> General' and change the target
// framework to .NET 4.0 or .NET 4.5.

module Quicksort.Main

open System

type ContinuationBuilder() =
    member this.Bind (m, f) = fun c -> m (fun a -> f a c)
    member this.Return x = fun k -> k x
let cont = ContinuationBuilder()

let qSort list=
 let rec loop list acc = 
  cont {
    match list with 
    | []     -> return acc
    | [x]    -> return x::acc
    | x::xs  -> 
      let l,r = List.partition ((>) x) xs
      let! rs = loop r acc
      let! s = loop l (x::rs)
      return s
      }
 loop list [] (fun x -> x)
    

[<EntryPoint>]
let main args = 
    printf "%A \n" (qSort [1;7;4;9;10;22;88])
    0

