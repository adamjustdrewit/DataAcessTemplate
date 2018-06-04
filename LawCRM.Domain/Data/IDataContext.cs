namespace Template.Domain.Data
{
    public interface IDataContext
    {
        /// <summary>
        /// Submits the collections state for storage
        /// </summary>
        void Submit();

        /// <summary>
        /// Cancels the collections changes and reinitialises the data
        /// </summary>
        void Cancel(); 
    }
}
