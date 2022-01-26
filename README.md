JChat
=====

## Como rodar o projeto

## Descrição da stack

## Migrações

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

## Configurações

### [Kratos](https://github.com/ory/kratos)

O arquivo de configuração do Kratos pode ser encontrada em [JChat.Infrastructure/Files/kratos/kratos.yml](./JChat.Infrastructure/Files/kratos/kratos.yml);

> Por enquanto, com a versão do v0.8.2-alpha.1, não há como carregar todas as variáveis do ambiente. Por esse motivo, algumas configurações precisaram ficar em um arquivo separado.
> 
> [link da discussão referente ao problema](https://github.com/ory/kratos/issues/1792).

[Documentação de referência](https://www.ory.sh/kratos/docs).

### [Keto](https://github.com/ory/keto)

O arquivo de configuração do Keto pode ser encontradro em [JChat.Infrastructure/Files/keto/keto.yml](./JChat.Infrastructure/Files/keto/keto.yml);

[Documentação de referência](https://www.ory.sh/keto/docs).

## Docs

### NSwag

- [ClientGeneratorBaseSettings](https://github.com/RicoSuter/NSwag/wiki/ClientGeneratorBaseSettings);
- [TypescriptGeneratorSettings](https://github.com/RicoSuter/NJsonSchema/wiki/TypeScriptGeneratorSettings);
- [NJsonSchema.CodeGeneration.TypeScript](https://apimundo.com/organizations/nuget-org/nuget-feeds/public/packages/NJsonSchema.CodeGeneration.TypeScript/versions/latest?type=NJsonSchema.CodeGeneration.TypeScript.TypeScriptGeneratorSettings&namespace=NJsonSchema.CodeGeneration.TypeScript&assembly=netstandard-2.0);

# TODO:

- [ ] Soft deletes filter;
