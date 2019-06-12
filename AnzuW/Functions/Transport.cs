#region copyright

// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

#endregion copyright

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;

internal class Transport
{
    /// <summary>
    /// Функция переноса файла, измененного более года назад
    /// </summary>
    /// <param name="path"></param>
    /// <param name="finishpath"></param>
    public static void FileTransport(string path, string finishpath)
    {
        DirectoryInfo dir = new DirectoryInfo(@path);

        var FileList = dir.GetFiles();

        foreach (var temp in FileList)
        {
            if (File.GetLastWriteTime(temp.FullName) < DateTime.Now.Subtract(new TimeSpan(0, 1, 0, 0)))
            {
                temp.CopyTo(finishpath + "\\" + temp.Name);
                File.SetAttributes(temp.FullName.ToString(), FileAttributes.Normal);
                File.Delete(temp.FullName.ToString());
            }
        }
    }

    public static List<string> GetListFolder(string path)
    {
        List<string> FileList = new List<string>();

        DirectoryInfo dir = new DirectoryInfo(@path);
        foreach (var item in dir.GetDirectories())
        {
            FileList.Add(item.ToString());
        }

        return FileList;
    }
}