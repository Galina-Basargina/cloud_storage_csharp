using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace coursework3.Classes
{
    class File
    {
        public int FileId { get; set; }
        public string OriginalFilename { get; set; }
        public double FileSize { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }

        public File(int fileId, string originalFilename, double fileSize, string contentType, DateTime uploadDate)
        {
            FileId = fileId;
            OriginalFilename = originalFilename;
            FileSize = fileSize;
            ContentType = contentType;
            UploadDate = uploadDate;
        }

    }
}
