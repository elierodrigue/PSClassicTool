using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSClassicTool
{
    public class ConfigFileHandler
    {
        public static string GetSettingValue(string fileName, string key)
        {
            string ret = "";
            string fileContent = System.IO.File.ReadAllText(fileName);
            string[] lines = fileContent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(String line in lines)
            {
                string[] keyVal = line.Split(new string[] { "=" }, StringSplitOptions.None);
                if(keyVal[0].Trim() ==key)
                {
                    ret = keyVal[1].Trim();
                    break;
                }
            }
            return ret;
        }
        public static void SetSettingValue(string fileName, string key, string value)
        {
            string fileOutput = "";
            string fileContent = System.IO.File.ReadAllText(fileName);
            string[] lines = fileContent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String line in lines)
            {
                string[] keyVal = line.Split(new string[] { "=" }, StringSplitOptions.None);
                if (keyVal[0].Trim() == key)
                {
                    fileOutput += keyVal[0].Trim() + " = " + value+"\n";
                   
                }
                else
                {
                    fileOutput += line + "\n";
                }
            }
            System.IO.File.WriteAllText(fileName, fileOutput);
        }
    }
}
