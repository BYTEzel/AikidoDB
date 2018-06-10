namespace AikidoTrainingDatabase.Domain
{
    public interface ICategory : ISearchable, IID
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}
