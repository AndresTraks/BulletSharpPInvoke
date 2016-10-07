using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ConvexDecompositionDemo
{
    class WavefrontObj
    {
        private readonly char[] _faceSplitSchars = { '/' };
        private readonly char[] _lineSplitChars = { ' ' };

        //Vector2 ToVector2(string f0, string f1)
        //{
        //    return new Vector2(
        //        float.Parse(f0, CultureInfo.InvariantCulture),
        //        float.Parse(f1, CultureInfo.InvariantCulture));
        //}

        Vector3 ToVector3(string f0, string f1, string f2)
        {
            return new Vector3(
                float.Parse(f0, CultureInfo.InvariantCulture),
                float.Parse(f1, CultureInfo.InvariantCulture),
                float.Parse(f2, CultureInfo.InvariantCulture));
        }

        int GetVertex(string[] faceVertex)
        {
            int vertexIndex = int.Parse(faceVertex[0]);
            if (vertexIndex < 0)
            {
                indices.Add(indices[indices.Count + vertexIndex]);
            }
            Vector3 position = vertices[vertexIndex - 1];

            // Search for a duplicate
            for (int i = 0; i < finalVertices.Count; i++)
            {
                if (finalVertices[i].Equals(position))
                {
                    indices.Add(i);
                    return i;
                }
            }

            int newIndex = finalVertices.Count;
            finalVertices.Add(position);
            indices.Add(newIndex);
            return newIndex;
        }

        void ProcessLine(string line)
        {
            if (line.Length == 0)
            {
                return;
            }

            string[] parts = line.Split(_lineSplitChars, StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0];

            switch (command)
            {
                case "v":
                    vertices.Add(ToVector3(parts[1], parts[2], parts[3]));
                    break;
                case "vn":
                    normals.Add(ToVector3(parts[1], parts[2], parts[3]));
                    break;
                case "vt":
                    //texels.Add(ToVector2(parts[1], parts[2]));
                    break;
                case "f":
                    int numVertices = parts.Length - 1;
                    int[] face = new int[numVertices];

                    face[0] = GetVertex(parts[1].Split(_faceSplitSchars, StringSplitOptions.RemoveEmptyEntries));
                    face[1] = GetVertex(parts[2].Split(_faceSplitSchars, StringSplitOptions.RemoveEmptyEntries));
                    face[2] = GetVertex(parts[3].Split(_faceSplitSchars, StringSplitOptions.RemoveEmptyEntries));

                    if (numVertices == 4)
                    {
                        indices.Add(face[0]);
                        indices.Add(face[2]);
                        face[3] = GetVertex(parts[4].Split(_faceSplitSchars, StringSplitOptions.RemoveEmptyEntries));
                    }
                    break;
            }
        }

        // Loads a wavefront obj returns the number of triangles that were loaded.
        public int LoadObj(string filename)
        {
            indices = new List<int>();
            normals = new List<Vector3>();
            //texels = new List<Vector2>();
            vertices = new List<Vector3>();
            finalVertices = new List<Vector3>();

            using (var file = File.OpenRead(filename))
            {
                var reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    ProcessLine(reader.ReadLine());
                }
            }

            return indices.Count / 3;
        }

        List<int> indices;
        List<Vector3> normals;
        //List<Vector2> texels;
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
