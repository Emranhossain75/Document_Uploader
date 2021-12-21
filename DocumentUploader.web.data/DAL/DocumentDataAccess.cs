using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentUploader.web.data.DAL;
using DocumentUploader.web.data.Model;

namespace DocumentUploader.web.data.DAL
{
    public class DocumentDataAccess
    {
        private string _connectionString;

        public DocumentDataAccess(string connection)
        {
            _connectionString = connection;
        }
    }
}
