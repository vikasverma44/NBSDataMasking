using System.IO;
using static NetCommonUtilities.Enums;

namespace NetCommonUtilities.Models
{
    public class FileProcessingModel
    {
        public bool HasOutputFileDetails { get; set; }
        public bool HasOutputErrors { get; set; }

        //Input
        public string InputFileName { get; set; }
        public string InputFileNameExtension { get; set; }
        public FileStream InputFileStream { get; set; }
        public string InputMetadataFileName { get; set; }
        public string InputMetadataFileNameExtension { get; set; }
        public FileStream InputMetaDataFileStream { get; set; }

        //Output
        public string OutputFileName { get; set; }
        public string OutputFileNameExtension { get; set; }
        public string OutputMetadataFileName { get; set; }
        public string OutputMetadataFileNameExtension { get; set; }

        //public FileStream OutputFileStream { get; set; }
        //public FileStream OutputMetaDataFileStream { get; set; }

        public byte[] OutputDataFileBytes { get; set; }

        public byte[] OutputMetaDataFileBytes { get; set; }

        public FileExtnType FileExtensionType  { get; set; }
        public FileExtn FileExtension  { get; set; }
    }
}
