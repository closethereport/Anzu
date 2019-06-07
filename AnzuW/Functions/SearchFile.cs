using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

class SearchFile
{

    public List<string> FileSearch()
    {

        //ищем все вложенные папки 
        string[] S = SearchDirectory("C:\\Users\\Евгений\\Desktop");
        //создаем строку в которой соберем все пути
        List<string> ListPatch = new List<string>();
        foreach (string folderPatch in S)
        {
            //добавляем новую строку в список
            // ListPatch += folderPatch + "\n";

            //пытаемся найти данные в папке 
            string[] F = FileSearch(folderPatch, "*.png");
            foreach (string FF in F)
            {
                //добавляем файл в список 
                ListPatch.Add(FF.ToString());
            }

        }
        return ListPatch;

    }
    /*функция нахождения директорий по указанному пути*/
    private string[] SearchDirectory(string patch)
    {
        //находим все папки в по указанному пути
        string[] ReultSearch = Directory.GetDirectories(patch);
        //возвращаем список директорий
        return ReultSearch;
    }

    /*функция поиска файлов в директории передаваемой в параметр patch 
          по его имени или маске передаваемой в параметре pattern
         */
    private string[] FileSearch(string patch, string pattern)
    {
        /*флаг SearchOption.AllDirectories означает искать во всех вложенных папках*/
        string[] ReultSearch = Directory.GetFiles(patch, pattern, SearchOption.AllDirectories);
        //возвращаем список найденных файлов соответствующих условию поиска 
        return ReultSearch;
    }
}
