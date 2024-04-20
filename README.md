# Secrets setup

```sh
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:Cod2ZomMysql" "Server=SERVER; Port=PORT; Database=NDB; Uid=USER; Pwd=PASS;"
```

# Run in docker locally

```sh
cd src && docker-compose -f docker-compose.local.yml up --build
```