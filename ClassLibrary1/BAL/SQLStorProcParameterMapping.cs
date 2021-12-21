using ClassLibrary1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.BAL
{
    public partial class SQLStorProcParameterMapping
    {
        internal static string GetAddNewDocumnet(Uploader uploader)
        {
            string parameters = "@RelatedTo='{0}',  @RelatedRecordId={1}, @CreateDate='{2}', @CreatedBy='{3}', @Document='{4}',@Note='{5}'";
            parameters = string.Format(parameters, uploader.RelatedTo, uploader.RelatedRecordId,uploader.CreateDate, uploader.CreatedBy,uploader.Document,uploader.Note);
            return parameters;
        }
        internal static string GetAddNewDocumnetFile(FileDetailsModel fileDetailsModel)
        {
            string parameters = "@FileName='{0}',  @FileContent={1}";
            parameters = string.Format(parameters, fileDetailsModel.FileName, fileDetailsModel.FileContent);
            return parameters;
        }
    }
}
