namespace Tieptuyen.Components.Core.Api.DataAccess
{
    /// <summary>
    /// Represents a data-access repository.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Begins an atomic unit of work.
        /// </summary>
        /// <returns>An <see cref="IUnitOfWork"/> object.</returns>
        IUnitOfWork BeginWork();
    }
}
