using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theory_of_information_calc.models
{
    public static class FileReader
    {
        public static async Task<string> Read(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open)) 
            {
                byte[] temp = new byte[fs.Length];

                await fs.ReadAsync(temp, 0, temp.Length);

                return Encoding.UTF8.GetString(temp);
            }
        }
    }
}
