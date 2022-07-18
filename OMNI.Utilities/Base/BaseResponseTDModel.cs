using System;
using System.Collections.Generic;
using System.Text;

namespace OMNI.Utilities.Base
{
    public class BaseDTResponseModel<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Draw { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
    }
}
