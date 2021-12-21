using ClassLibrary1.BAL;
using ClassLibrary1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.DAL
{
    public class DocumentAccess
    {
        private string _connectionString;

        public DocumentAccess(string connection)
        {
            _connectionString = connection;
        }

        public void AddDocumnet(Uploader uploader)
        {
            string query = SQLStorProcParameterMapping.GetAddNewDocumnet(uploader);
            query = "exec [dbo].[sp_AddNewDocument]" + " " + query;
            List<Uploader> cList = new List<Uploader>();
            cList = CommonDBHelper.GetDataFromDB<Uploader>(query, _connectionString);

        }
        public void AddDocumnetFile(FileDetailsModel fileDetailsModel)
        {
            string query = SQLStorProcParameterMapping.GetAddNewDocumnetFile(fileDetailsModel);
            query = "exec [dbo].[AddFileDetails]" + " " + query;
            List<FileDetailsModel> cList = new List<FileDetailsModel>();
            cList = CommonDBHelper.GetDataFromDB<FileDetailsModel>(query, _connectionString);

        }
        public List<DocumentUploader> GetDocument()
        {
            List<DocumentUploader> cList = new List<DocumentUploader>();
            cList = CommonDBHelper.GetDataFromDB<DocumentUploader>("exec [dbo].[sp_GetDocument]", _connectionString);
            return cList;
        }

    }
}
