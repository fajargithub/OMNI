using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Web.Models
{
    public enum ModalSize
    {
        Small,
        Large,
        Medium
    }

    public enum ModalType
    {
        Normal,
        Center,
        Right
    }

    public class ModalViewModel
    {
        public string ID { get; set; }
        public string AreaLabeledId { get; set; }
        public ModalSize Size { get; set; }
        public ModalType Type { get; set; }
        public string Message { get; set; }

        public string ModalTypeClass
        {
            get
            {
                return this.Type switch
                {
                    ModalType.Right => "modal-dialog-right",
                    ModalType.Center => "modal-dialog-centered",
                    _ => "",
                };
            }
        }

        public string ModalSizeClass
        {
            get
            {
                return this.Size switch
                {
                    ModalSize.Small => "modal-sm",
                    ModalSize.Large => "modal-lg",
                    _ => "",
                };
            }
        }
    }

    public class ModalHeader
    {
        public string Heading { get; set; }
    }
}
