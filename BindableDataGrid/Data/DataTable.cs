using System;
using System.IO;
using System.Xml;

namespace BindableDataGrid.Data
{
    /// <summary>
    /// Represents a DataTable
    /// </summary>
    public class DataTable : IDataSource
    {
        #region "Properties"

        /// <summary>
        /// Name of the DataTable
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection of columns
        /// </summary>
        public DataColumnCollection Columns { get; set; }

        /// <summary>
        /// Collection of rows
        /// </summary>
        public DataRowCollection Rows { get; set; }

        #endregion "Properties"

        #region "Methods"

        /// <summary>
        /// Constructor of the DataTable with a name
        /// </summary>
        /// <param name="name">Name of the DataTable</param>
        public DataTable(string name)
        {
            this.Name = name;
            this.Columns = new DataColumnCollection();
            this.Rows = new DataRowCollection();
        }

        /// <summary>
        /// Writes the current contents of the DataTable as XML using the specified XmlWriter.
        /// </summary>
        /// <param name="writer">Writer that will contain the DataTable representation</param>
        public void WriteXml(XmlWriter writer)
        {
            bool standAloneTable = (writer.WriteState == WriteState.Start);
            if (standAloneTable) writer.WriteStartElement("DocumentElement");
            foreach (DataRow dr in this.Rows)
            {
                writer.WriteStartElement(this.Name);
                foreach (DataColumn dc in this.Columns)
                {
                    writer.WriteElementString(dc.ColumnName, dr[dc.ColumnName].ToString());
                }
                writer.WriteEndElement();
            }
            if (standAloneTable) writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the current contents of the DataTable as XML using the specified TextWriter.
        /// </summary>
        /// <param name="writer">Writer that will contain the DataTable representation</param>
        public void WriteXml(TextWriter writer)
        {
            using (XmlWriter xm = XmlWriter.Create(writer))
            {
                this.WriteXml(xm);
            }
        }

        /// <summary>
        /// Writes the current contents of the DataTable as XML using the specified Stream.
        /// </summary>
        /// <param name="stream">Writer that will contain the DataTable representation</param>
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