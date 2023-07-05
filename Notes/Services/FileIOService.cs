using Newtonsoft.Json;
using Notes.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services
{
    internal class FileIOService
    {

        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }
        public BindingList<dataModel> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<dataModel>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText  = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<dataModel>>(fileText);
            }

        }

        public void SaveData(object dataModelsList) 
        {
            using (StreamWriter writer = File.CreateText(PATH)) 
            {
                string output = JsonConvert.SerializeObject(dataModelsList);
                writer.Write(output);
            }
        }
    }
}
