function(ctx) {
    id: ctx.identity.id,
    username: ctx.identity.traits.username,
    firstname: ctx.identity.traits.name.first,
    lastname: ctx.identity.traits.name.last
}
