using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BulletSharpGen
{
    [Flags]
    enum WriteTo
    {
        None = 0,
        Header = 1,
        Source = 2,
        CS = 4,
        Buffer = 8,
        AllFiles = Header | Source | CS
    }

    abstract class WrapperWriter
    {
        protected IEnumerable<HeaderDefinition> headerDefinitions;
        protected string NamespaceName { get; private set; }

        protected StreamWriter headerWriter, sourceWriter, csWriter;
        protected StringBuilder bufferBuilder = new StringBuilder();
        protected bool hasHeaderWhiteSpace;
        protected bool hasSourceWhiteSpace;
        protected bool hasCSWhiteSpace;

        public WrapperWriter(IEnumerable<HeaderDefinition> headerDefinitions, string namespaceName)
        {
            this.headerDefinitions = headerDefinitions;
            NamespaceName = namespaceName;
        }

        public abstract void Output();

        protected void Write(string s, WriteTo to = WriteTo.AllFiles)
        {
            if ((to & WriteTo.Header) != 0)
            {
                headerWriter.Write(s);
            }
            if ((to & WriteTo.Source) != 0)
            {
                sourceWriter.Write(s);
            }
            if ((to & WriteTo.CS) != 0)
            {
                csWriter.Write(s);
            }
            if ((to & WriteTo.Buffer) != 0)
            {
                bufferBuilder.Append(s);
            }
        }

        protected void Write(char c, WriteTo to = WriteTo.AllFiles)
        {
            Write(c.ToString(), to);
        }

        protected void WriteLine(string s, WriteTo to = WriteTo.AllFiles)
        {
            Write(s, to);
            WriteLine(to);
        }

        protected void WriteLine(char c, WriteTo to = WriteTo.AllFiles)
        {
            Write(c, to);
            WriteLine(to);
        }

        protected void WriteLine(WriteTo to = WriteTo.AllFiles)
        {
            Write("\r\n", to);
        }

        protected void WriteTabs(int n, WriteTo to = WriteTo.Header)
        {
            for (int i = 0; i < n; i++)
            {
                Write('\t', to);
            }
        }

        protected void EnsureWhiteSpace(WriteTo to)
        {
            if ((to & WriteTo.Source) != 0)
            {
                if (!hasSourceWhiteSpace)
                {
                    sourceWriter.WriteLine();
                    hasSourceWhiteSpace = true;
                }
            }
            if ((to & WriteTo.CS) != 0)
            {
                if (!hasCSWhiteSpace)
                {
                    csWriter.WriteLine();
                    hasCSWhiteSpace = true;
                }
            }
        }
    }
}
