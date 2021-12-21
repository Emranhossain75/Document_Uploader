using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Model
{
    public partial class DocumentUploader
    {
        public int UploadId { get; set; }
        public string RelatedTo { get; set; }
        public Nullable<int> RelatedRecordId { get; set; }

        public string Document { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
