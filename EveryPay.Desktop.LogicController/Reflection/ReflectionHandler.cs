using EveryPay.Desktop.ImportInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Desktop.LogicController.Reflection
{
    public class ReflectionHandler
    {
        public string FilesPath { get; set; }

        public ReflectionHandler(string path)
        {
            FilesPath = path;
        }

        public List<string> getMatchingDlls()
        {
            List<string> matchingDlls = new List<string>();

            string[] files = Directory.GetFiles(FilesPath);

            foreach(string file in files)
            {
                Assembly assembly = Assembly.LoadFile(file);
                foreach(Type classType in assembly.GetTypes())
                {
                    if(typeof(IProductsImporter).IsAssignableFrom(classType))
                    {
                        matchingDlls.Add(file);
                    }
                }
            }
            return matchingDlls;
        }

        public IProductsImporter getInterfaceInstance()
        {
            IProductsImporter interfaceToReturn=null;
            Assembly assembly = Assembly.LoadFile(FilesPath);
            foreach (Type classType in assembly.GetTypes())
            {
                if (typeof(IProductsImporter).IsAssignableFrom(classType))
                {
                    object instance = Activator.CreateInstance(classType);
                    interfaceToReturn=(IProductsImporter)instance;
                }
            }
            return interfaceToReturn;
        }
    }
}
