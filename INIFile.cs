using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Update
{
    public class INIFile
    {
        public string path
        {
            get; private set;
        }
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public INIFile(string INIPath = null)
        {
            //path = INIPath;
            string DiretorioINI = Directory.GetCurrentDirectory() + @"\setting.ini";
            if (!File.Exists(DiretorioINI))
            {
                File.Create(DiretorioINI);
            }
            path = Directory.GetCurrentDirectory() + @"\setting.ini";
        }
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }
    }
}
