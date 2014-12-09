module RadixTree.Trie

open RadixTree.TrieNode

type Trie(path: string)=
    let trie = new TrieNode()
    let sorted_words = 
       let words = System.IO.File.ReadAllLines(path) 
       Array.sort words
    member this.Load() =
       try
        for w in sorted_words do
        trie.Add(w)       
       with
       | e -> raise e
    member private this.IsWord(s:string, t:TrieNode) =
        if t=null then false
        elif s.Length=0 then t.IsEndOfWord
        else this.IsWord(s.Substring(1), t.[ s.[0] ])
    member private this.WordsInPattern(s:string, t:TrieNode,acc:string) =
        if t=null then []
        elif s.Length=0 && t.IsEndOfWord then [acc]
        else
          this.WordsInPattern(s.Substring(1), t.[ s.[0] ],acc + s.Substring(0,1))  
    member this.FindPattern(s:string) =
        this.WordsInPattern(s,trie,"");
    member this.FindWord(s:string) =
        this.IsWord(s,trie);
    new() = Trie("dict.txt")