using Mozaeek.CR.PublicDto.Enum.CoreDomain;
using MozaeekCore.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.Domain
{
    public class MosaikFile
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public FileType Type { get; set; }
        public DateTime CreateDate { get; set; }
        public string Url { get; set; }
        public MosaikFile(long id, string name, string path,string extension,string url, FileType type)
        {
            Id = id;
            Name = name;
            Path = path;
            Type = type;
            CreateDate = DateTime.Now;
            Extension = extension;
            Url = url;
            
        }
    }
}
