namespace Tieptuyen.Components.Core.Api.ObjectModel
{
    /// <summary>
    /// Defines an object which can ordered on a display along with other objects of the same type.
    /// </summary>
    public interface IDisplayOrderable
    {
        /// <summary>
        /// Gets the display order.
        /// </summary>
        byte DisplayOrder { get; }
    }
}
