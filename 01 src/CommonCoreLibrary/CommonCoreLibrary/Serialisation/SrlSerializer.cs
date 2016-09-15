using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CommonCoreLibrary.Serialisation
{
    public class SrlSerializer
    {
        private Stream _stream;

        /// <summary>
        /// Stream
        /// </summary>
        public Stream Stream
        {
            get
            {
                return this._stream;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream">Stream</param>
        public SrlSerializer(Stream stream)
        {
            this._stream = stream;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file">File to read or write</param>
        public SrlSerializer(string file)
            : this(new FileStream(file, FileMode.OpenOrCreate))
        {

        }

        /// <summary>
        /// Serializer tag
        /// </summary>
        /// <param name="tag">SrlTag</param>
        public void Serializer(SrlTag tag)
        {
            SrlStreamWriter var1 = null;
            try
            {
                var1 = new SrlStreamWriter(this._stream);
            }
            catch (SrlException ex)
            {
                throw ex;
            }

            var1.WriteType(tag.Type);
            tag.GetBytes(var1);
            var1.Flush();
        }

        /// <summary>
        /// Deserialize Tag
        /// </summary>
        /// <returns>SrlTag</returns>
        public SrlTag Deserialize()
        {
            SrlStreamReader var1 = null;
            try
            {
                var1 = new SrlStreamReader(this._stream);
            }
            catch (SrlException ex)
            {
                throw ex;
            }

            if (var1 == null)
            {
                return null;
            }

            SrlType var2 = var1.ReadType();
            SrlTag var3 = SrlTag.GetInstance(var2);
            var3.SetBytes(var1);
            return var3;
        }
    }
}
