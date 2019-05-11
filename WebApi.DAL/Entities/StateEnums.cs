namespace WebApi.Entities
{
    public enum MartialState
    {
        NA = 1,
        Single = 2,
        Married = 4,
        Other = 8,
    }

    public enum SiblingState
    {
        NA = 1,
        Eldest = 2,
        Younges = 4,
        Middle = 8,
        Only_Child = 16,
        Other = 32,
    }
}
