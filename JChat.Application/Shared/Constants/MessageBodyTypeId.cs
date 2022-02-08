namespace JChat.Application.Shared.Constants;

public static class MessageBodyTypeId
{
    public static readonly Guid Audio = Guid.Parse("0bad45cd-595e-48c9-aff8-b927e2b30851");
    public static readonly Guid Gif = Guid.Parse("60f47ec7-5025-4e55-892b-7b1f2c444ee0");
    public static readonly Guid Image = Guid.Parse("f082a1f1-74de-4349-9ed6-24fe42188aea");
    public static readonly Guid Text = Guid.Parse("87159cfd-1db5-42ec-9250-084b3fc41964");
    public static readonly Guid Video = Guid.Parse("35cbb295-74cc-4157-9253-91c76492a1f2");
    public static readonly Guid ChannelEvent = Guid.Parse("d10bef76-e162-41dd-a625-bb04d24b0064");
}