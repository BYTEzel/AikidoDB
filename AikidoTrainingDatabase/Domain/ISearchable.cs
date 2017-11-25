namespace AikidoTrainingDatabase.Domain
{
    /// <summary>
    /// This interface enables classes to be searched automatically.
    /// All keywords have to be contained in the two defined fields
    /// "Name" and "Description".
    /// </summary>
    public interface ISearchable
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}
