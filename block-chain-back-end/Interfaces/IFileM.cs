using API1.Models;
using Newtonsoft.Json.Linq;
namespace API1.Interfaces

{
    public interface IFileM
    {
        public List<FileM> getFiles();

        public List<FileM> getFileByUserName(string userName);
        public FileM getFileById(string id);
        public void deleteFile(List<JObject> id);
        public List<FileM> postFiles(List<IFormFile> files, string usuario);



    }
}
