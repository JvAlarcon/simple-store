open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Giraffe

open SimpleStore

let mainPageHttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        HtmlTemplate.renderMainPage {
            count = 10
        }

let renderView 
    (handler : HttpFunc -> HttpContext -> String)
    : HttpHandler
    =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let result = handler next ctx
            return! htmlString result next ctx
        }

let webApp =
    choose [
        GET >=>
            choose [
                route "/" >=> renderView mainPageHttpHandler
            ]
    ]

let configureApp (app : IApplicationBuilder) =
    app.UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder 
                    .Configure(configureApp)
                    .ConfigureServices configureServices
                    |> ignore)
        .Build()
        .Run()
    0