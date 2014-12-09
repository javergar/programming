namespace Algorithms.FSharp
open System
open System.Diagnostics
open Tree

// delegate for visiting a node
type VisitNode = delegate of int -> unit
    
// delegate for visiting an edge
type VisitEdge = delegate of int * int * int * int -> unit

[<DebuggerDisplay("activeLength={activeLength.Value}, minDistance={minDistance.Value}, currentNodeNumber={currentNodeNumber.Value}")>]
type SuffixTree(text : string) =
        let activeLength = ref 0
        let lastBranchIndex = ref 0
        let currentNodeNumber = ref (uint32(0))
        let minDistance = ref 0
        let activeNode = ref (new Node(!currentNodeNumber))
        let rootNode = !activeNode
        let mutable (lastBranchNode : Node option) = None

        do
            Tree.Text <- text
            if String.IsNullOrWhiteSpace(text) then raise (new ArgumentNullException("Empty string not allowed"))

        static member Text  
            with get () = Tree.Text

        member private tree.normalizeEnd value = if value < 0 then Text.Length - 1 else value

        member private tree.updateMinDistance index =
            if !lastBranchIndex < !activeLength + !minDistance + index then Math.Max(0, !lastBranchIndex - !activeLength - index) else !minDistance
        
        // Walks the tree breadth first and for each visited node and edge
        // invokes the appropriate delegate
        member tree.WalkTree( visitNode : VisitNode, visitEdge : VisitEdge) : unit=
            
            // quick queue implementation
            let enqueue item q  = q @ [item]

            let dequeue = function 
                | [] -> failwith "Empty queue. Cannot dequeue."
                | h :: t -> h, t

            let queue = ref []
            
            queue := !queue |> enqueue rootNode

            while not (!queue).IsEmpty do
                let (currentNode, newQueue) = !queue |> dequeue
                queue := newQueue
                visitNode.Invoke(int32(currentNode.Label))
            
                currentNode.Edges |>
                Map.iter (
                    fun key value -> 
                        queue := !queue |> enqueue value.EndNode
                        visitEdge.Invoke(int(currentNode.Label), int(value.EndNode.Label), value.Start, (tree.normalizeEnd value.End)))

        //creates the actual tree
        member tree.Create() : unit =
            let mutable followSuffixNode = false
            let mutable stop = false
            let mutable i = 0

            while (not stop) && i < Text.Length do
                minDistance := tree.updateMinDistance i

                let nodeEdge = (!activeNode).findNextRoute (i + !activeLength) followSuffixNode

                if i + !activeLength >= Text.Length && nodeEdge = (None, None) then stop <- true
                else
                    match nodeEdge with
                    // found a suffix node, move on
                    | (Some node, None) ->
                        activeNode := node
                        activeLength := !activeLength - 1
                        followSuffixNode <- false

                    // found a new active node
                    | (Some node, Some edge) ->
                        activeNode := node
                        activeLength := !activeLength + 1
                        followSuffixNode <- false

                    // found nothing, add a new node
                    | (None, None) ->
                        currentNodeNumber :=  !currentNodeNumber + uint32(1)
                        (!activeNode).addNode !currentNodeNumber (i + !activeLength) -1 |> ignore
                        lastBranchIndex := i + !activeLength
                        followSuffixNode <- true
                        i <- i + 1

                    // regular case: stopped in the middle of the edge
                    | (None, Some edge) ->
                        let edgePos = edge.walkTheEdge i activeLength minDistance activeNode

                        match edgePos with
                        | (edge, j) ->
                            minDistance := j
                            lastBranchIndex := i + j + !activeLength

                            if !lastBranchIndex >= Text.Length then
                                i <- i + 1
                                followSuffixNode <- true
                            else
                                currentNodeNumber := !currentNodeNumber + uint32(1)
                                let newBranchNode = edge.split (edge.Start + j - 1) !currentNodeNumber

                                if edge.Route.Length = 1 then
                                    newBranchNode.SuffixPointer <- Some !activeNode
                                    
                                if not (lastBranchNode = None) && (!!lastBranchNode).SuffixPointer = None then
                                    (!!lastBranchNode).SuffixPointer <- Some newBranchNode

                                currentNodeNumber := !currentNodeNumber + uint32(1)
                                newBranchNode.addNode !currentNodeNumber !lastBranchIndex -1
                                lastBranchNode <- Some newBranchNode
                                i <- i+ 1
                                followSuffixNode <- true
                                    