using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public interface ISrlSerializable
    {
        SrlTag GetSrl();

        void SetSrl(SrlTag tag);
    }
}
