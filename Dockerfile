FROM mcr.microsoft.com/dotnet/sdk:7.0.202 as builder

WORKDIR /app
COPY . .
RUN dotnet publish -c release -o `pwd`/build -r debian-x64 --sc true --packages /cache/nuget/packages ExchangeScrapper

FROM mcr.microsoft.com/dotnet/runtime-deps:7.0.4
WORKDIR /app
COPY --from=builder /app/build ./

CMD ["./ExchangeScrapper"]