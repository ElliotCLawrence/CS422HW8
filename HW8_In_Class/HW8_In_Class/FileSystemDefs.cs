using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CS422
{
    public abstract class Dir422
    {
        public abstract string Name { get; }

        public abstract IList<Dir422> GetDirs();

        public abstract List<File422> GetFiles();

        public abstract Dir422 Parent { get; }

        public abstract bool ContainsFile(string fileName, bool recursive);

        public abstract bool ContainsDir(string dirName, bool recursive);

        public abstract Dir422 getDir(string name);

        public abstract File422 GetFile(string name);

        public abstract File422 CreateFile(string name);

        public abstract Dir422 CreateDir(string name);



    }

    public abstract class File422
    {
        string Name { get; }

        public Dir422 Parent { get; }

        public abstract Stream OpenReadOnly();

        public abstract Stream OpenReadWrite();
    }

    public abstract class FileSys422
    {
        public abstract Dir422 GetRoot();

        public virtual bool Contains(File422 file)
        {
            return Contains(file.Parent);
        }

        public virtual bool Contains(Dir422 dir)
        {
            if (dir == null) { return false; }
            if (dir == this.GetRoot()) { return true; }
            return Contains(dir.Parent);
        }
    }



    public class StandardFileSystem : FileSys422
    {
        Dir422 root;

        public override Dir422 GetRoot()
        {
            return root;
        }
    }

    public class StdFSDir : Dir422
    {
        private string m_path;

        public StdFSDir(string path)
        {
            m_path = path;
        }

        public override string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Dir422 Parent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool ContainsDir(string dirName, bool recursive)
        {
            throw new NotImplementedException();
        }

        public override bool ContainsFile(string fileName, bool recursive)
        {
            throw new NotImplementedException();
        }

        public override Dir422 CreateDir(string name)
        {
            throw new NotImplementedException();
        }

        public override File422 CreateFile(string name)
        {
            throw new NotImplementedException();
        }

        public override Dir422 getDir(string name)
        {
            throw new NotImplementedException();
        }

        public override IList<Dir422> GetDirs()
        {
            throw new NotImplementedException();
        }

        public override File422 GetFile(string name)
        {
            throw new NotImplementedException();
        }

        public override List<File422> GetFiles()
        {
            List<File422> files = new List<File422>();
            foreach (string file in Directory.GetFiles(m_path))
            {
                files.Add(new STDFSFile(file));
            }
            return files;
        }
    }

    public class STDFSFile : File422
    {
        private string m_path;

        public STDFSFile(string path) { m_path = path; }

        public override Stream OpenReadOnly() //one line function return a stream with m_path in it
        {
            throw new NotImplementedException();
        }

        public override Stream OpenReadWrite() //one line function return a stream with m_path in it
                                               //if you fail to open this stream, return null, don't throw exception
        {
            throw new NotImplementedException();
        }
    }

    public class MemoryFileSystem
    {

    }
    
    public class MemFSDir
    {

    }

    public class MemFSFile
    {

    }
}