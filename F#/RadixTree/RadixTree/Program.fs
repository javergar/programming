module RadixTree.Main

open System
open RadixTree.Trie


[<EntryPoint>]
let main args = 
    let trie = new Trie();
    trie.Load();
    printfn "%A \n" (trie.FindPattern("CAT"));
    0