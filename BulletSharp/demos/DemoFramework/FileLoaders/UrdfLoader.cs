using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace DemoFramework.FileLoaders
{
    public static class UrdfLoader
    {
        public static UrdfRobot FromFile(string filename)
        {
            var document = new XmlDocument();
            document.Load(filename);
            return ParseRobot(document["robot"]);
        }

        private static UrdfRobot ParseRobot(XmlElement element)
        {
            var robot = new UrdfRobot
            {
                Name = element.GetAttribute("name")
            };

            foreach (XmlElement linkElement in element.SelectNodes("link"))
            {
                UrdfLink link = ParseLink(linkElement);
                robot.Links[link.Name] = link;
            }

            foreach (XmlElement jointElement in element.SelectNodes("joint"))
            {
                UrdfJoint joint = ParseJoint(jointElement, robot);
                robot.Joints.Add(joint);
            }

            return robot;
        }

        private static UrdfLink ParseLink(XmlElement element)
        {
            return new UrdfLink
            {
                Name = element.GetAttribute("name"),
                Collision = ParseCollision(element["collision"]),
                Inertial = ParseInertial(element["inertial"]),
            };
        }

        private static UrdfCollision ParseCollision(XmlElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new UrdfCollision
            {
                Geometry = ParseGeometry(element["geometry"]),
                Origin = ParseOrigin(element["origin"])
            };
        }

        private static UrdfInertial ParseInertial(XmlElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new UrdfInertial
            {
                Mass = ParseMass(element["mass"]),
                Inertia = ParseInertia(element["inertia"])
            };
        }

        private static double ParseMass(XmlElement element)
        {
            return ParseDouble(element.Attributes["value"].Value);
        }

        private static UrdfInertia ParseInertia(XmlElement element)
        {
            return new UrdfInertia()
            {
                XX = ParseDouble(element.Attributes["ixx"].Value),
                XY = ParseDouble(element.Attributes["ixy"].Value),
                XZ = ParseDouble(element.Attributes["ixz"].Value),
                YY = ParseDouble(element.Attributes["iyy"].Value),
                YZ = ParseDouble(element.Attributes["iyz"].Value),
                ZZ = ParseDouble(element.Attributes["izz"].Value)
            };
        }

        private static UrdfGeometry ParseGeometry(XmlElement element)
        {
            var shapeElement = element.SelectSingleNode("box|capsule|cylinder|mesh|sphere");
            switch (shapeElement.Name)
            {
                case "box":
                    return new UrdfBox
                    {
                        Size = shapeElement.Attributes["size"].Value
                    };
                case "capsule":
                    return new UrdfCapsule
                    {
                        Radius = ParseDouble(shapeElement.Attributes["radius"].Value),
                        Length = ParseDouble(shapeElement.Attributes["length"].Value)
                    };
                case "cylinder":
                    return new UrdfCylinder
                    {
                        Radius = ParseDouble(shapeElement.Attributes["radius"].Value),
                        Length = ParseDouble(shapeElement.Attributes["length"].Value)
                    };
                case "mesh":
                    return new UrdfMesh
                    {
                        FileName = shapeElement.Attributes["filename"].Value,
                        Scale = shapeElement.Attributes["scale"]?.Value
                    };
                case "sphere":
                    return new UrdfSphere
                    {
                        Radius = ParseDouble(shapeElement.Attributes["radius"].Value)
                    };
            }
            throw new NotSupportedException();
        }

        private static UrdfJoint ParseJoint(XmlElement jointElement, UrdfRobot robot)
        {
            XmlNode parentNode = jointElement.SelectSingleNode("parent");
            XmlNode childNode = jointElement.SelectSingleNode("child");

            string parentId = parentNode.Attributes["link"].Value;
            string childId = childNode.Attributes["link"].Value;

            UrdfJoint joint;

            string type = jointElement.GetAttribute("type");
            switch (type)
            {
                case "continuous":
                    joint = new UrdfContinuousJoint();
                    break;
                case "prismatic":
                    joint = new UrdfPrismaticJoint();
                    break;
                case "fixed":
                    joint = new UrdfFixedJoint();
                    break;
                case "revolute":
                    joint = new UrdfRevoluteJoint();
                    break;
                default:
                    throw new NotSupportedException();
            }

            joint.Parent = robot.Links[parentId];
            joint.Child = robot.Links[childId];

            return joint;
        }

        private static UrdfPose ParseOrigin(XmlElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new UrdfPose
            {
                Position = element.Attributes["xyz"]?.Value,
                RollPitchYaw = element.Attributes["rpy"]?.Value
            };
        }

        private static double ParseDouble(string value)
        {
            return double.Parse(value, CultureInfo.InvariantCulture);
        }
    }

    public class UrdfRobot
    {
        public string Name { get; set; }
        public IDictionary<string, UrdfLink> Links { get; } = new Dictionary<string, UrdfLink>();
        public List<UrdfJoint> Joints { get; } = new List<UrdfJoint>();

        public override string ToString()
        {
            return Name;
        }
    }

    public class UrdfLink
    {
        public string Name { get; set; }
        public UrdfCollision Collision { get; set; }
        public UrdfInertial Inertial { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class UrdfCollision
    {
        public UrdfGeometry Geometry { get; set; }
        public UrdfPose Origin { get; set; }
    }

    public class UrdfInertia
    {
        public double XX { get; set; }
        public double XY { get; set; }
        public double XZ { get; set; }
        public double YY { get; set; }
        public double YZ { get; set; }
        public double ZZ { get; set; }
    }

    public class UrdfInertial
    {
        public double Mass { get; set; }
        public UrdfInertia Inertia { get; set; }
    }

    public enum UrdfGeometryType
    {
        Box,
        Capsule,
        Cylinder,
        Mesh,
        Sphere
    }

    public abstract class UrdfGeometry
    {
        public abstract UrdfGeometryType Type { get; }
    }

    public class UrdfBox : UrdfGeometry
    {
        public override UrdfGeometryType Type => UrdfGeometryType.Box;

        public string Size { get; set; }
    }

    public class UrdfCapsule : UrdfGeometry
    {
        public override UrdfGeometryType Type => UrdfGeometryType.Capsule;

        public double Length { get; set; }
        public double Radius { get; set; }
    }

    public class UrdfCylinder : UrdfGeometry
    {
        public override UrdfGeometryType Type => UrdfGeometryType.Cylinder;

        public double Length { get; set; }
        public double Radius { get; set; }
    }

    public class UrdfMesh : UrdfGeometry
    {
        public override UrdfGeometryType Type => UrdfGeometryType.Mesh;

        public string FileName { get; set; }
        public string Scale { get; set; }
    }

    public class UrdfSphere : UrdfGeometry
    {
        public override UrdfGeometryType Type => UrdfGeometryType.Sphere;

        public double Radius { get; set; }
    }

    public enum UrdfJointType
    {
        Continuous,
        Prismatic,
        Fixed,
        Revolute
    }

    public abstract class UrdfJoint
    {
        public abstract UrdfJointType Type { get; }

        public UrdfLink Parent { get; set; }
        public UrdfLink Child { get; set; }

        public override string ToString()
        {
            return $"{Child} -> {Parent}";
        }
    }

    public class UrdfContinuousJoint : UrdfJoint
    {
        public override UrdfJointType Type => UrdfJointType.Continuous;
    }

    public class UrdfPrismaticJoint : UrdfJoint
    {
        public override UrdfJointType Type => UrdfJointType.Prismatic;
    }

    public class UrdfFixedJoint : UrdfJoint
    {
        public override UrdfJointType Type => UrdfJointType.Fixed;
    }

    public class UrdfRevoluteJoint : UrdfJoint
    {
        public override UrdfJointType Type => UrdfJointType.Revolute;
    }

    public class UrdfPose
    {
        public string Position { get; set; }
        public string RollPitchYaw { get; set; }
    }
}
