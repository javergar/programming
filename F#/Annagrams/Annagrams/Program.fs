open System.Collections.Generic
open System

type Solver() =
    let dict = new Dictionary<char list,string list>()
    
    member this.Dict with get() = dict

    member this.insertDict (str:string) = 
        let list = ref ([]:string list)
        let mutable key = str |> Seq.sort |> Seq.toList
        if dict.TryGetValue(key,list) then
         dict.[key] <- (str :: !list)
        else
            dict.Add(key,[str]) |> ignore
      
    member this.sorted_words path = 
       let words = System.IO.File.ReadAllLines(path) 
       let swords =Array.sort words
       for word in swords do
         this.insertDict word |> ignore

    member this.find_annagrams word =
        let list = ref ([]:string list)
        let key = word |> Seq.sort |> Seq.toList
        if dict.TryGetValue(key,list) then
            printf "%A \n" list
        else
            printf "Not found \n"
    member this.rotate lst =
        List.tail lst @ [List.head lst]

    member this.getRotations lst =
        let rec getAll lst i = if i = 0 then [] else lst :: (getAll (this.rotate lst) (i - 1))
        getAll lst (List.length lst)

    member this.getPerms n lst = 
        match n, lst with
        | 0, _ -> seq [[]]
        | _, [] -> seq []
        | k, _ -> lst |> this.getRotations 
                      |> Seq.collect (fun r -> Seq.map ((@) [List.head r]) (this.getPerms (k - 1) (List.tail r)))


[<EntryPoint>]
let main args = 
    let o = new Object()
    if( o :? String ) then
    {
      Printf "%A \n" x;
    }
    let dict = new Solver()
    dict.sorted_words "/home/javergar/dict.txt";
    let query1 = query {
       for words  in dict.Dict.Values do
       where (words.Length > 1)
       select words
       }
       
    query1 |> Seq.iter (fun x -> printf "%A \n" x)
    
    //printfn "%A \n" (getPerms 4 (List.ofSeq "heandnedgotfree"));
    0

