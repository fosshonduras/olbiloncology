((Get-Content -path C:\projects\olbiloncology\OLBIL.OncologyWebApp\ClientApp\src\app\app.component.html -Raw) -replace '{{BuildNumber}}',$env:APPVEYOR_BUILD_VERSION)| Set-Content -Path C:\projects\olbiloncology\OLBIL.OncologyWebApp\ClientApp\src\app\app.component.html