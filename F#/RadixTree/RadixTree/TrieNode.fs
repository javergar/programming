module RadixTree.TrieNode

let IsLetter(c)   =    c>='A' && c<='Z'
exception FileNotFound of string

[<AllowNullLiteral>]
type TrieNode() =
    let nextChar = Array.create 26 null
    let mutable isEndOfWord = false
    member this.IsEndOfWord = isEndOfWord
    member this.Item with get(c:char) = nextChar.[int c - int 'A']
                     and private set(c:char) v = nextChar.[int c - int 'A'] <- v
    member this.Add(word) = 
        assert(word |> Seq.forall IsLetter)
        this.Add(word,0)
    member private this.Add(word:string,i) =
        if i = word.Length then
            isEndOfWord <- true
        else 
            match this.[word.[i]] with
            | null -> let newNode = new TrieNode()
                      newNode.Add(word,i+1)
                      this.[word.[i]] <- newNode
            | node -> node.Add(word,i+1)
            
            