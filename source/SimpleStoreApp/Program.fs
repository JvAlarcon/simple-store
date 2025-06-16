open System
open SimpleStore

[<EntryPoint>]
let main argv =
    let paidOrders = Purchases.getPaidOrders
    printfn "------------Id Order------------|------------User Name------------|------------Product Name------------"
    paidOrders
    |> List.iter Purchases.printPaidOders
    0