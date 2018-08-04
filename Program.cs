using System;
using FolderMatch;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderMatch
{
    class Program
    {
        static void Main(string[] args)
        {
            FolderMatcher folderMatcher = new FolderMatcher();
            folderMatcher.Match();
            Console.ReadLine();
            
        }

        

    }


}

