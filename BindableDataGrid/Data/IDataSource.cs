using System.Xml;
using System.IO;
namespace BindableDataGrid.Data
{
    /// <summary>
    /// Simple interface to define what objects can be used as DataSource for the grid
    /// </summary>
    public interface IDataSource
    {
        #region "Properties"

        /// <summary>
        /// Name of the DataSource
        /// </summary>
        string Name { get; set; }

        #endregion "Properties"

        #region "Methods"

        /// <summary>
        /// Writes the current contents of the DataSource as XML using the specified XmlWriter.
        /// </summary>
        /// <param name="writer">Writer that will contain the DataSource representation</param>
        void WriteXml(XmlWriter writer);

        /// <summary>
        /// Writes the current contents of the DataSource as XML using the specified TextWriter.
        /// </summary>
        /// <param name="writer">Writer that will contain the DataSource representation</param>
        void WriteXml(TextWriter writer);

        /// <summary>
        /// Writes the current contents of the DataSource as XML using the specified Stream.
        /// </summary>
        /// <param name="stream">Writer that will contain the DataSource representation</param>
        void WriteXml(Stream stream);

        #endregion "Methods"
    }
}