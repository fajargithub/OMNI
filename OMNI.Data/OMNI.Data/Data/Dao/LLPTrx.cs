using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data.Dao
{
    public class LLPTrx : BaseDao
    {
        public virtual SpesifikasiJenis SpesifikasiJenis { get; set; }
        public string Port { get; set; }
        public string QRCode { get; set; }
        public string QRCodeText { get; set; }
        public decimal DetailExisting { get; set; }
        public string Kondisi { get; set; }
        public decimal TotalExistingJenis { get; set; }
        public decimal TotalExistingKeseluruhan { get; set; }
        public decimal TotalKebutuhanHubla { get; set; }
        public decimal SelisihHubla { get; set; }
        public string KesesuaianMP58 { get; set; }
        public decimal TotalKebutuhanOSCP { get; set; }
        public decimal SelisihOSCP { get; set; }
        public string KesesuaianOSCP { get; set; }
        public decimal PersentaseOSCP { get; set; }
        public decimal PersentaseHubla { get; set; }
        public string Brand { get; set; }
        public string SerialNumber { get; set; }
        public string Remark { get; set; }
        public int Year { get; set; }
    }
}
