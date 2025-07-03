namespace SimpleStore

module HtmlTemplate =
    let buildHtmlTemplateParser<'TTemplateProps> (template : string) =
        let compiledTemplate = Scriban.Template.Parse template

        match compiledTemplate.HasErrors with
        | true -> raise (System.SystemException $"Failed to parse template {compiledTemplate.Messages}")
        | false -> ()

        fun (props : 'TTemplateProps) ->
            compiledTemplate.Render(
                props,
                memberRenamer = fun m -> m.Name
            )
    
    type PaidOrderTableComponentProps =
        {
            Orders : PaidOrder list
        }
    
    let renderPaidOrderTableComponent : PaidOrderTableComponentProps -> string =
        let template =
            """
            <table>
                <tr>
                    <th>User name</th>
                    <th>Id Order</th>
                    <th>Product Name</th>
                </tr>
                {{ for paidOrder in Orders}}
                    <tr>
                        <td>{{ paidOrder.UserFullName }}</td>
                        <td>{{ paidOrder.IdOrder }}</td>
                        <td>{{ paidOrder.ProductName }}</td>
                    </tr>
                {{ end }}
            </table>
            """
        let parser = buildHtmlTemplateParser<PaidOrderTableComponentProps> template

        fun (props : PaidOrderTableComponentProps) ->
            parser props

    type MainPageProps =
        {
            count : int
        }
    
    let renderMainPage props : string =
        let paidOrders = Purchases.getPaidOrders
        let template =
            $"""
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset="UTF-8">
                <title>Purchases</title>
            </head>
            <body>
                <main>
                    <h2>Paid orders</h2>
                </main>
                {renderPaidOrderTableComponent { Orders = paidOrders }}
            </body>
            </html>
            """
        let parser = buildHtmlTemplateParser<MainPageProps> template
        parser props
