using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderMatch
{
    class FolderMatcher
    {
        private string targetPathname;
        private string searchPathname;

        public void PromptTarget()
        {
            Console.WriteLine("Specify a folder to match to:");
            targetPathname = Console.ReadLine();
        }

        public void PromptSearch()
        {
            Console.WriteLine("Specify a folder to search in:");
            searchPathname = Console.ReadLine();
        }

        public string ParseTarget()
        {
            int slash = targetPathname.LastIndexOf("\\");
            return targetPathname.Remove(0, slash + 1);
            //Console.WriteLine(matchTo);
        }


        public bool VerifyFolder(string pathname)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(pathname);
                if (dir.Exists)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("That folder does not exist");
                    return false;
                }
            } catch (ArgumentException e)
            {
                Console.WriteLine("Invalid Path");
                return false;
            }catch (NotSupportedException e)
            {
                Console.WriteLine("Path name not supported");
                return false;
            }

        }

        public void SearchFolder(string match)
        {
            if (match.Contains(" "))
            {
                string[] names = match.Split(' ', '-', '_');

                ArrayList possibleNames = new ArrayList();
                string temp = names[0];

                for (int i = 1; i < names.Length; i++)
                {
                    temp = temp + " " + names[i];
                }

                possibleNames.Add("*" + temp.Replace(" ", string.Empty) + "*");
                possibleNames.Add("*" + temp.Replace(' ', '?') + "*");
              
                DirectoryInfo dir = new DirectoryInfo(searchPathname);
                foreach (string i in possibleNames){
                    CopyFiles(dir.GetFiles(i));
                }
            }
        }

        public void CopyFiles(FileInfo[] files)
        {

            foreach (FileInfo i in files)
            {

                try
                {
                    Console.WriteLine(i.Name + " is being copied to " + targetPathname);
                    float size = i.Length;
                    //i.CopyTo(targetPathname + "\\" + i.Name);
                    FileStream destination = new FileStream(targetPathname + "\\" + i.Name, FileMode.CreateNew, FileAccess.Write);
                    FileStream source = new FileStream(searchPathname + "\\" + i.Name, FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[1048576];
                    float progress = 0;
                    while (progress < size)
                    {
                        progress = progress + source.Read(buffer, 0, buffer.Length);
                        destination.Write(buffer, 0, buffer.Length);
                        double status = progress / size * .001;
                        Console.WriteLine(status);
                    }
                    Console.WriteLine("{0} has finished copying", i.Name);


                }
                catch (IOException e)
                {
                    Console.WriteLine("File already exists");
                }

            }
            Console.WriteLine("Copying is complete");

        }

        public void Match()
        {
            PromptTarget();
            if (VerifyFolder(targetPathname))
            {
                PromptSearch();
                if (VerifyFolder(searchPathname))
                {
                    SearchFolder(ParseTarget());
                }
            }
           
        }

        public void Crawl(string pathname)
        {

        }

        
    }



}
