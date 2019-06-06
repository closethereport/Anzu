using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


class Transport
{
    public static void TransportFile(string directory)
    {
        string[] files = Directory.GetFiles(@"C:\\MinGW", "*.bbc");

        foreach (string file in files)
        {
            File.Copy(file, directory);
        }
    }

}
