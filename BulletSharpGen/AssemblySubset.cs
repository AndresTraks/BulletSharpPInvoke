using System;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace BulletSharpGen
{
    // Finds all references to a specified namespace within assemblies.
    // This way a wrapper with a minimal subset of classes and methods can be generated.
    class AssemblySubset
    {
        List<TypeReference> subsetTypes = new List<TypeReference>();
        List<MethodReference> subsetMethods = new List<MethodReference>();

        bool AddTypeReference(TypeReference type, string namespaceName)
        {
            if (type.Namespace == namespaceName ||
                (type.Namespace.StartsWith(namespaceName) && type.Namespace[namespaceName.Length] == '.'))
            {
                if (type.IsArray)
                {
                    var arrayElementType = (type as ArrayType).ElementType;
                    if (!subsetTypes.Contains(arrayElementType))
                    {
                        subsetTypes.Add(arrayElementType);
                    }
                }
                else if (type.IsByReference)
                {
                    var byRefElementType = (type as ByReferenceType).ElementType;
                    if (!subsetTypes.Contains(byRefElementType))
                    {
                        subsetTypes.Add(byRefElementType);
                    }
                }
                else
                {
                    if (!subsetTypes.Contains(type))
                    {
                        subsetTypes.Add(type);
                    }
                }
                return true;
            }
            return false;
        }

        bool AddMethodReference(MethodReference method, string namespaceName)
        {
            if (method.DeclaringType.Namespace == namespaceName)
            {
                if (!subsetMethods.Contains(method))
                {
                    subsetMethods.Add(method);
                    return true;
                }
            }
            return false;
        }

        public void LoadAssembly(string assemblyName, string namespaceName)
        {
            AssemblyDefinition assembly;
            try
            {
                assembly = AssemblyDefinition.ReadAssembly(assemblyName);
            }
            catch (FileNotFoundException e)
            {
                Console.Write(e);
                return;
            }

            var module = assembly.MainModule;
            foreach (var type in module.Types)
            {
                foreach (var field in type.Fields)
                {
                    AddTypeReference(field.FieldType, namespaceName);
                }

                foreach (var method in type.Methods)
                {
                    if (!method.HasBody)
                    {
                        continue;
                    }
                    foreach (var instruction in method.Body.Instructions)
                    {
                        var operand = instruction.Operand;
                        if (operand != null)
                        {
                            if (operand is MethodReference)
                            {
                                var methodOp = operand as MethodReference;
                                if (AddTypeReference(methodOp.ReturnType, namespaceName))
                                {
                                    AddMethodReference(methodOp, namespaceName);
                                    continue;
                                }
                                foreach (var param in methodOp.Parameters)
                                {
                                    if (AddTypeReference(param.ParameterType, namespaceName))
                                    {
                                        AddMethodReference(methodOp, namespaceName);
                                    }
                                }
                            }
                            else if (operand is FieldReference)
                            {
                                var field = operand as FieldReference;
                                AddTypeReference(field.FieldType, namespaceName);
                            }
                            else if (operand is VariableReference)
                            {
                                var variable = operand as VariableReference;
                                AddTypeReference(variable.VariableType, namespaceName);
                            }
                        }
                    }

                    AddTypeReference(method.ReturnType, namespaceName);
                }
            }
        }
    }
}
