JChat
=====

### Migrações

Criar uma migração:

```shell
dotnet ef migrations add {Nome} --project JChat.Infrastructure --verbose --output-dir Persistence/Migrations --startup-project JChat.WebUI
```

Aplicar uma migração:

```shell
dotnet ef database update --project JChat.Infrastructure --verbose --startup-project JChat.WebUI
```

## Swagger

- [AspNetCore-Middleware](https://github.com/RicoSuter/NSwag/wiki/AspNetCore-Middleware);

# TODO:

- [ ] Soft deletes filter;
