using BulletSharp.Math;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ConvexDecompositionDemo
{
    class WavefrontObj
    {
        //Vector2 ToVector2(string f0, string f1)
        //{
        //    return new Vector2(float.Parse(f0, CultureInfo.InvariantCulture),
        //        float.Parse(f1, CultureInfo.InvariantCulture));
        //}

        Vector3 ToVector3(string f0, string f1, string f2)
        {
            return new Vector3(float.Parse(f0, CultureInfo.InvariantCulture),
                float.Parse(f1, CultureInfo.InvariantCulture),
                float.Parse(f2, CultureInfo.InvariantCulture));
        }

        int GetVertex(string[] faceVertex)
        {
            int vindex = int.Parse(faceVertex[0]);
            Vector3 position = vertices[vindex - 1];

            // Search for a duplicate
            int index;
            for (index = 0; index < finalVertices.Count; index++)
            {
                if (finalVertices[index].Equals(position))
                {
                    indices.Add(index);
                    return index;
                }
            }

            finalVertices.Add(position);
            indices.Add(index);
            return index;
        }

        void ProcessLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return;
            }

            string[] parts = line.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            string cmd = parts[0];

            if (cmd.Equals("v"))
            {
                Vector3 v = ToVector3(parts[1], parts[2], parts[3]);
                vertices.Add(v);
            }
            else if (cmd.Equals("vt"))
            {
                //texels.Add(ToVector2(parts[1], parts[2]));
            }
            else if (cmd.Equals("vn"))
            {
                Vector3 n = ToVector3(parts[1], parts[2], parts[3]);
                normals.Add(n);
            }
            else if (cmd.Equals("f"))
            {
                int[] face = new int[parts.Length - 1];

                face[0] = GetVertex(parts[1].Split(faceSplit, System.StringSplitOptions.RemoveEmptyEntries));
                face[1] = GetVertex(parts[2].Split(faceSplit, System.StringSplitOptions.RemoveEmptyEntries));
                face[2] = GetVertex(parts[3].Split(faceSplit, System.StringSplitOptions.RemoveEmptyEntries));

                if (face.Length == 4)
                {
                    indices.Add(face[0]);
                    indices.Add(face[2]);
                    face[3] = GetVertex(parts[4].Split(faceSplit, System.StringSplitOptions.RemoveEmptyEntries));
                }
            }
        }

        // load a wavefront obj returns number of triangles that were loaded.  Data is persists until the class is destructed.
        public int LoadObj(string fname)
        {
            int ret = 0;
            triCount = 0;

            indices = new List<int>();
            normals = new List<Vector3>();
            //texels = new List<Vector2>();
            vertices = new List<Vector3>();
            finalVertices = new List<Vector3>();

            FileStream file = File.OpenRead(fname);
            StreamReader reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                ProcessLine(reader.ReadLine());
            }
            file.Dispose();

            if (vertices.Count > 0)
            {
                triCount = indices.Count / 3;
                ret = triCount;
            }

            vertices = null;

            return ret;
        }

        readonly char[] faceSplit = new char[] { '/' };
        public int triCount;
        List<int> indices;
        public List<Vector3> normals;
        //public List<Vector2> texels;
        List<Vector3> vertices;

        List<Vector3> finalVertices;

        public List<int> Indices
        {
            get { return indices; }
        }

        public List<Vector3> Vertices
        {
            get { return finalVertices; }
        }
    }
}
