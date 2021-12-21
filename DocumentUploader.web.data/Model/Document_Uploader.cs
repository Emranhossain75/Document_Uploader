using System;

namespace DocumentUploader.web.data.Model
{
    public partial class UploadedDocument
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
