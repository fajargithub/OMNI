using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OMNI.Utilities.Base
{
    public class BaseJson<T>
    {
        public int Id { get; set; }
        public int Code { get; set; } = (int)HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public string ErrorMsg { get; set; }
        public T Payload { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public List<string> Roles { get; set; }
    }

    public class BaseDatatable<T>
    {
        public int Draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public string ErrorMsg { get; set; }
        public T Data { get; set; }
    }

    public class BaseParamDatatable
    {
        public int Skip { get; set; }
        public int PageSize { get; set; }
        public int Draw { get; set; }
        public string ColumnIndex { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
    }
}
