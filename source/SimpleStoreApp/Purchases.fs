namespace SimpleStore

type PaidOrder = {
    IdOrder : int32
    UserFullName : string
    ProductName : string
}

module Purchases =
    open Npgsql.FSharp

    let connectionString = 
        Sql.host "postgres"
        |> Sql.database "simple_store"
        |> Sql.username "postgres"
        |> Sql.password "dbpwd123@@"
        |> Sql.port 5432
        |> Sql.formatConnectionString
    
    let getPaidOrders =
        let query = 
            "SELECT u.first_name AS first_name, u.last_name AS last_name, o.id AS id_order, p.name AS product_name
            FROM users AS u
            INNER JOIN orders AS o ON u.id = o.user_id
            INNER JOIN products AS p ON p.id = o.product_id
            WHERE o.paid = true"
        connectionString
        |> Sql.connect
        |> Sql.query query
        |> Sql.execute (fun r ->
            {
                IdOrder = r.int "id_order"
                UserFullName = r.string "first_name" + r.string "last_name"
                ProductName = r.string "product_name"
            })
    
    let printPaidOrders paidOrder =
        printfn "%i | %s | %s" paidOrder.IdOrder paidOrder.UserFullName paidOrder.ProductName