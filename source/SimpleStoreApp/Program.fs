open System
open SimpleStore


let listPaidOrders paidOrders =
    printfn "------------Id Order------------|------------User Name------------|------------Product Name------------"
    paidOrders
    |> List.iter Purchases.printPaidOrders

let endRecursion() =
    0

let rec readInput () = //Doesn't work yet
    printfn "----------------CONSOLE STORE----------------"
    // Create 3 categories of options:
    // 1 - Management of orders (Lists orders, paid orders, unpaid orders); 
    // 2 - Add, change and delete (user and products);
    // 3 - Purchase a product for a user -> Effectively will create an order
    Console.Write "1 - List Paid orders; 2 - "
    let input = Console.ReadLine() |> int
    match input with
    | 0 -> 0
    | 1 -> listPaidOrders Purchases.getPaidOrders |> readInput
    | _ -> readInput()

[<EntryPoint>]
let main argv =
    if argv.Length > 0 then
        readInput ()
    else
        0