﻿using System;
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
        public string Name { get; }

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
        private string dirName;
        private Dir422 parentDir;
        

        public StdFSDir(string path)
        {
            m_path = path;
        }

        public override string Name
        {
            get
            {
                return dirName;
            }
        }

        public override Dir422 Parent
        {
            get
            {
                return parentDir;
            }
        }

        public override bool ContainsDir(string dirName, bool recursive)
        {
            foreach (string dir in Directory.GetDirectories(m_path))
            {
                

            }

            if (recursive == true) //if recursive not true, return false
            {
                foreach (StdFSDir child in directoryChildren) //if it is true, search children
                {
                    if (child.ContainsDir(dirName, true))
                        return true;
                }
            }

            return false;
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


    public class MemoryFileSystem : FileSys422
    {
        MemFSDir root; 

        public override Dir422 GetRoot()
        {
            return root;
        }
    }

    public class MemFSDir : Dir422
    {
        private string dirName;
        private MemFSDir dirParent;
        private List<Dir422> directoryChildren;
        private List<File422> fileChildren;
        private string name;

        public MemFSDir(string name)
        {
            this.name = name;
        }

        public override string Name
        {
            get
            {
                return dirName;
            }
        }

        public override Dir422 Parent
        {
            get
            {
                return dirParent;
            }
        }

        public override bool ContainsDir(string dirName, bool recursive)
        {
            foreach (MemFSDir child in directoryChildren) //check all the files in this folder for a match
            {
                if (child.Name == dirName)
                    return true;
            }
            
            if (recursive == true) //if recursive not true, return false
            {
                foreach (MemFSDir child in directoryChildren) //if it is true, search children
                {
                    if (child.ContainsDir(dirName, true))
                        return true;
                }
            }

            return false;
        }

        public override bool ContainsFile(string fileName, bool recursive)
        {
            foreach (MemFSFile child in fileChildren) //check all the files in this folder for a match
            {
                if (child.Name == fileName)
                    return true;
            }

            if (recursive == true) //if recursive not true, return false
            {
                foreach (MemFSDir child in directoryChildren) //if it is true, search children
                {
                    if (child.ContainsFile(dirName, true))
                        return true;
                }
            }

            return false;
        }

        public override Dir422 CreateDir(string name)
        {
            directoryChildren.Add(new MemFSDir(name));
            return directoryChildren[directoryChildren.Count-1];
        }

        public override File422 CreateFile(string name)
        {
            fileChildren.Add(new MemFSFile(name));
            return fileChildren[fileChildren.Count - 1];
        }

        public override Dir422 getDir(string name)
        {
            for (int x = 0; x < directoryChildren.Count; x++)
            {
                if (directoryChildren[x].Name == name)
                    return directoryChildren[x];
            }

            return null;
        }

        public override IList<Dir422> GetDirs()
        {
            return directoryChildren;
        }

        public override File422 GetFile(string name)
        {
            for (int x = 0; x < fileChildren.Count; x++)
            {
                if (fileChildren[x].Name == name)
                    return fileChildren[x];
            }

            return null;
        }

        public override List<File422> GetFiles()
        {
            return fileChildren;
        }
    }

    public class MemFSFile : File422
    {
        private string fileName;
        MemFSDir parentDir;

        public MemFSFile(string name)
        {
            this.fileName = name;
        }

        public string Name
        {
            get
            {
                return fileName;
            }
        }

        public MemFSDir Parent
        {
            get
            {
                return parentDir;
            }
        }



        public override Stream OpenReadOnly()
        {
            throw new NotImplementedException();
        }

        public override Stream OpenReadWrite()
        {
            throw new NotImplementedException();
        }
    }
}