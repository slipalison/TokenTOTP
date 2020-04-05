namespace Responses
{
#pragma warning disable S2344 // Enumeration type names should not have "Flags" or "Enum" suffixes

    public enum LayerEnum : byte
#pragma warning restore S2344 // Enumeration type names should not have "Flags" or "Enum" suffixes
    {
        None = 0,
        Core = 1,
        Operational = 2,
        Entrypoint = 3,
        Client = 4,
        Worker = 5
    }
}