module KeybrDataInterpreter
    
    open FSharp.Data;

    [<Literal>]
    let public Source = "reference.json"

    type public KeybrData = JsonProvider<Source>

    let createHistogram key (time) (hitCount, missCount) = 
        KeybrData.Histogram (key, hitCount, missCount, time);

    let histogramFolder (hitCount, missCount) (h:KeybrData.Histogram) =
        (hitCount + h.HitCount, missCount + h.MissCount)

    let reduceGroupHistograms (key, arr) = 

        let extractTimeToType (d:KeybrData.Histogram) =
            d.TimeToType |> double

        let averageTime =  
            arr 
            |> Array.map extractTimeToType
            |> Array.average
            |> int

        arr
        |> Array.fold histogramFolder (0, 0)
        |> createHistogram key averageTime

    
    let groupHistory (histograms: KeybrData.Histogram []) =
        histograms
        |> Array.groupBy (fun h -> h.CharCode)
        |> Array.map reduceGroupHistograms

    let getKeyData (dataDumpFilePath:string) = 
        KeybrData.Load dataDumpFilePath
        |> fun x -> x.UsWorkmanAuto.Data
        |> Array.map (fun d -> d.Histogram)
        |> Array.concat
        |> groupHistory