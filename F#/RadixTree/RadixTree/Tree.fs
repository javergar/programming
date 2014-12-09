 namespace Algorithms.FSharp
    module internal Tree =
       open System
       open System.Diagnostics
    
    let mutable Text = ""

    let inline (!!) opt =
        match opt with
        | Some n -> n
        | _ -> raise (new ArgumentNullException("Null node encountered"))
     
    // Node class
    [<DebuggerDisplay("Label={Label}, Edges.Count={Edges.Count}")>]
    type internal Node (label : uint32)  = 
        let label = label
        let mutable edges : Map<char, Edge> = Map.empty
        let mutable (suffixPointer : Node option) = None

        member self.Label 
            with get() = label
        
        member self.Edges 
            with get () = edges
            and set(value) = edges <- value

        member self.SuffixPointer 
            with get() = suffixPointer
            and set(value) = suffixPointer <- value

        member self.findEdgeByChar (c : char) : Edge option =
            if edges.ContainsKey(c) then Some edges.[c] else None

        member self.findEdgeByChar (start : int) : Edge option =
            if start >= Text.Length then None else self.findEdgeByChar Text.[start]
        
        // pick the next route by either going to the suffix node
        // or examining the edges
        member self.findNextRoute (start: int) followSuffixNode : Node option *Edge option =
            match suffixPointer with
            | Some s when followSuffixNode = true -> (Some s, None)
            | _ ->
                let edge = self.findEdgeByChar(start)
                match edge with
                | None -> (None, None)
                | Some e ->
                    match (e.Route : string).Length with
                    | 1 ->(Some e.EndNode, Some e) 
                    | _ -> (None, Some e)
                
        // add a new node
        member self.addNode label start endRoute = 
            let newNode = new Node(label)
            let newEdge = new Edge(newNode, start, endRoute)
            self.Edges <- self.Edges |> Map.add newEdge.Route.[0] newEdge
 
    // Edge class
    and 
        [<DebuggerDisplay("Start={Start}, End={End}, Route={Route}")>]
        internal Edge(node : Node, startRoute : int, endRoute : int)  =
        let mutable endNode = node
        let mutable endRoute = endRoute
        let mutable start = startRoute
        do
           if endNode = Unchecked.defaultof<Node> then raise (System.ArgumentNullException("node cannot be null"))
           if uint32(startRoute) > uint32(endRoute) then raise (System.ArgumentOutOfRangeException("start > end"))

        member edge.Start
            with get() = start
            and set(value) = start <- value

        member edge.End
            with get() = endRoute
            and set(value) = endRoute <- value

        member edge.EndNode
            with get () = endNode
            and set(value) = endNode <- value

        member private edge.getLength =
            if edge.End < 0 then Text.Length - edge.Start else edge.End - edge.Start + 1

        member private edge.getSubstring =
            Text.Substring(edge.Start, edge.getLength)

        member edge.Route 
            with get() = edge.getSubstring

        member edge.split (endRoute : int) (currentNodeNumber : uint32) =
            let nextStart = endRoute + 1
            let oldNode = edge.EndNode
            
            let newEdge = new Edge(oldNode, nextStart, edge.End)
            let newNode = new Node(currentNodeNumber)

            do
                edge.End <- endRoute
                edge.EndNode <- newNode
                newNode.Edges <- newNode.Edges |> Map.add newEdge.Route.[0] newEdge
            newNode

        member edge.walk (suffix : string) index skip =
            let mutable stop = false
            let mutable j = skip
            let mutable i = index + j

            while (not stop) &&  j < edge.Route.Length && i < suffix.Length do
                if not (suffix.[i] = edge.Route.[j]) then stop <- true
                else
                    i <- i + 1
                    j <- j + 1
            j
                    
        member edge.walkTheEdge i (activeLength : int ref) (minDistance : int ref) (activeNode : Node ref) : Edge*int =
            let skipCharacters = !minDistance
            let index = i + !activeLength
            match skipCharacters with
                | skipCharacters when skipCharacters >= edge.Route.Length ->

                    let newEdge = !!(edge.EndNode.findEdgeByChar (i + edge.Route.Length))
                    activeLength := !activeLength + edge.Route.Length
                    minDistance := !minDistance - edge.Route.Length

                    activeNode := !activeNode
                    newEdge.walkTheEdge i activeLength minDistance activeNode                    

                | _ ->
                    let j = edge.walk Text index skipCharacters
                    (edge, j)    
            
