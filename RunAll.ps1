
Write-Host "Starting up Authentication Service"
Push-Location .\ItemsBasket.AuthenticationService
dotnet restore
start-process dotnet run
Pop-Location

Write-Host "Starting up Basket Service"
Push-Location .\ItemsBasket.BasketService
dotnet restore
start-process dotnet run
Pop-Location

Write-Host "Starting up Items Service"
Push-Location .\ItemsBasket.ItemsService
dotnet restore
start-process dotnet run
Pop-Location




