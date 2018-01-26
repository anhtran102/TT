using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Tieptuyen.Components.Core.AssetStore.TestHarness
{
    public sealed class AssetStoreTestController : ApiController
    {
        public IAssetStoreManager manager;

        public AssetStoreTestController(IAssetStoreManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public object[] GetAllFiles(string store, string path)
        {
            IAssetStore resourceStore = this.manager.GetResourceStore(store);
            List<object> results = new List<object>();
            foreach (IAssetStoreFile file in resourceStore.GetDirectory(path).Files)
            {

                dynamic result = new ExpandoObject();
                result.fileName = file.FileName;
                results.Add(result);
            }

            return results.ToArray();
        }

        [HttpGet]
        public object[] GetAllDirectories(string store, string path)
        {
            IAssetStore resourceStore = this.manager.GetResourceStore(store);
            List<object> results = new List<object>();
            foreach (IAssetStoreDirectory file in resourceStore.GetDirectory(path).Directories)
            {

                dynamic result = new ExpandoObject();
                result.name = file.Name;
                results.Add(result);
            }

            return results.ToArray();
        }

        [HttpGet]
        public object GetFile(string store, string fileName)
        {
            IAssetStore resourceStore = this.manager.GetResourceStore(store);
            IAssetStoreFile file = resourceStore.GetFile(fileName);
            dynamic result = new ExpandoObject();
            result.fileName = file.FileName;
            return result;
        }

        [HttpGet]
        public object GetDirectory(string store, string name)
        {
            IAssetStore resourceStore = this.manager.GetResourceStore(store);
            IAssetStoreDirectory directory = resourceStore.GetDirectory(name);
            dynamic result = new ExpandoObject();
            result.name = directory.Name;
            return result;
        }

        [HttpGet]
        public object FileExists(string store, string fileName)
        {
            IAssetStore resourceStore = this.manager.GetResourceStore(store);
            dynamic result = new ExpandoObject();
            result.exists = resourceStore.FileExists(fileName);
            return result;
        }

        [HttpGet]
        public object DirectoryExists(string store, string name)
        {
            IAssetStore resourceStore = this.manager.GetResourceStore(store);
            dynamic result = new ExpandoObject();
            result.exists = resourceStore.DirectoryExists(name);
            return result;
        }

        [HttpPost]
        public object CreateDirectory(dynamic postData)
        {
            IAssetStore resourceStore = this.manager.GetResourceStore(postData.store.Value);
            IAssetStoreDirectory parentDir = resourceStore.GetDirectory(postData.parentDirectory.Value);
            IAssetStoreDirectory newDir = parentDir.CreateDirectory(postData.name.Value);
            dynamic result = new ExpandoObject();
            result.name = newDir.Name;
            return result;
        }

        [HttpPost]
        public void DeleteDirectory(dynamic postData)
        {
            IAssetStore resourceStore = this.manager.GetResourceStore(postData.store.Value);
            IAssetStoreDirectory parentDir = resourceStore.GetDirectory(postData.parentDirectory.Value);
            parentDir.DeleteDirectory(postData.name.Value);
        }

        [HttpPost]
        public HttpResponseMessage UploadFile()
        {
            HttpRequest request = HttpContext.Current.Request;
            string store = request.Form["store"];
            string directory = request.Form["directory"];
            HttpPostedFile uploadedFile = request.Files["file"];
            IAssetStore resourceStore = this.manager.GetResourceStore(store);
            IAssetStoreDirectory parentDir = resourceStore.GetDirectory(directory);
            IAssetStoreFile virtualFile;
            if (parentDir.FileExists(uploadedFile.FileName))
                virtualFile = parentDir.GetFile(uploadedFile.FileName);
            else
                virtualFile = parentDir.CreateFile(uploadedFile.FileName);

            using (Stream virtualFileStream = virtualFile.Open(false))
            {
                uploadedFile.InputStream.CopyTo(virtualFileStream);
                virtualFileStream.Flush();
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Success!");
        }

        [HttpPost]
        public void DeleteFile(dynamic postData)
        {
            IAssetStore resourceStore = this.manager.GetResourceStore(postData.store.Value);
            IAssetStoreDirectory parentDir = resourceStore.GetDirectory(postData.parentDirectory.Value);
            parentDir.DeleteFile(postData.fileName.Value);
        }
    }
}
