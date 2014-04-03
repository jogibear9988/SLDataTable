using System;
using System.IO;
using System.Xml;

namespace BindableDataGrid.Data
{
    /// <summary>
    /// Represents a DataSet
    /// </summary>
    public class DataSet : IDataSource
    {
        #region "Properties"

        /// <summary>
        /// Name of the DataSet
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of DataTables
        /// </summary>
        public DataTableCollection Tables { get; set; }

        #endregion "Properties"

        #region "Methods"

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="name"></param>
        public DataSet(string name)
        {
            this.Name = name;
            this.Tables = new DataTableCollection();
        }

        /// <summary>
        /// Writes the current data for the DataSet to the specified XmlWriter.
        /// </summary>
        /// <param name="writer">Writer that will contain the DataSet representation</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(this.Name);
            foreach (DataTable dt in this.Tables)
            {
                dt.WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the current data for the DataSet using the specified TextWriter.
        /// </summary>
        /// <param name="writer">Writer that will contain the DataSet representation</param>
        public void WriteXml(TextWriter writer)
        {
            using (XmlWriter xm = XmlWriter.Create(writer))
            {
                this.WriteXml(xm);
            }
        }

        /// <summary>
        /// Writes the current contents of the DataSet as XML using the specified Stream.
        /// </summary>
        /// <param name="stream">Writer that will contain the DataSet representation</param>
        public void WriteXml(Stream stream)
        {
            using (XmlWriter xm = XmlWriter.Create(stream))
            {
                this.WriteXml(xm);
            }
        }

        /// <summary>
        /// Returns a serialized representation of the object into a String
        /// </summary>
        /// <returns>String version of the object</returns>
        public override string ToString()
        {
            string ret = String.Empty;
            using (StringWriter sw = new StringWriter())
            {
                this.WriteXml(sw);
                ret = sw.ToString();
            }
            return ret;
        }

        #endregion "Methods"
    }
}