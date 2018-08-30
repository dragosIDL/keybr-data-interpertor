open System
open KeybrDataInterpreter

let (./.) x y = 
    (x |> float ) / (y |> float) 

let charStringOrSPACE c =
    if c = (char)32 then "SPACE" else c.ToString()

let prettyPrintHistogram (data: KeybrData.Histogram) = 

    let charName = charStringOrSPACE (data.CharCode |> char)
    
    let missToHitCountRatio = data.MissCount ./. data.HitCount

    sprintf "%5s %10d %10d %10f %10d" charName data.HitCount data.MissCount missToHitCountRatio data.TimeToType

let printValues (data: KeybrData.Histogram []) = 
    data 
    |> Array.map prettyPrintHistogram
    |> Array.iter(printfn "%s")

(*
    Provide the path to the keybr data dump as a parameter.
*)

[<EntryPoint>]
let main argv =

    let dataDumpFilePath = argv.[0];
 
    let keyGroups = getKeyData dataDumpFilePath
    
    printfn "\n\n SORTED by hitcount descending \n\n"
    keyGroups
        |> Array.sortByDescending (fun d -> d.HitCount) 
        |> printValues

    printfn "\n\n SORTED by misscount descending \n\n"
    keyGroups
        |> Array.sortByDescending (fun d -> d.MissCount) 
        |> printValues

    printfn "\n\n SORTED by misscount/hitcount ratio descending \n\n"
    keyGroups
        |> Array.sortByDescending (fun d -> (d.MissCount ./. d.HitCount)) 
        |> printValues

    printfn "\n\n SORTED by average time to type descending \n\n"
    keyGroups
        |> Array.sortByDescending (fun d -> d.TimeToType) 
        |> printValues

    Console.ReadLine() |> ignore

    0