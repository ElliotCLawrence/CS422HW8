using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CS422
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryFileSystem mfs = new MemoryFileSystem();
            MemFSDir root = (MemFSDir)mfs.GetRoot();

            MemFSFile testing = (MemFSFile)root.CreateFile("test.txt");
            MemoryStream stream = (MemoryStream)testing.OpenReadWrite();

            string test = "This is a test string!";
            byte[] buf = Encoding.ASCII.GetBytes(test);
            stream.Write(buf, 0, buf.Length);

            stream.Close();

            MemoryStream stream2 = (MemoryStream)testing.OpenReadOnly();
            int numBytes = stream2.Read(buf, 0, buf.Length);
            string test2 = Encoding.ASCII.GetString(buf);

            MemFSDir test1 = (MemFSDir)root.CreateDir("test1");
            MemFSDir subTest1 = (MemFSDir)test1.CreateDir("subTest1");
            MemFSFile file = (MemFSFile)subTest1.CreateFile("test.txt");
            test1.CreateDir("subTest2");

            root.CreateDir("test2");
            root.CreateDir("test3");
        }
    }
}
