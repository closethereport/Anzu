using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

class DataSorting
{
    /// <summary>
    /// Class for sorting files in the directory by creation date
    /// </summary>
    /// <param name="path"></param>
    public static void DS(string path)
    {
        String[] files = Directory.GetFiles(path).OrderBy(d => new FileInfo(d).CreationTime).ToArray();

    }
}
