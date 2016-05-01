using ClangSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    public class CWriter : WrapperWriter
    {
        private bool _hasCppClassSeparatingWhitespace;
        private Dictionary<string, string> _wrapperHeaderGuards;

        public CWriter(DefaultParser parser) : base(parser)
        {
            _wrapperHeaderGuards = new Dictionary<string, string>
            {
                {"btActionInterface", "_BT_ACTION_INTERFACE_H"},
                {"btBroadphaseAabbCallback", "BT_BROADPHASE_INTERFACE_H"},
                {"btBroadphaseRayCallback", "BT_BROADPHASE_INTERFACE_H"},
                {"ContactResultCallback", "BT_COLLISION_WORLD_H"},
                {"ConvexResultCallback", "BT_COLLISION_WORLD_H"},
                {"RayResultCallback", "BT_COLLISION_WORLD_H"},
                {"btIDebugDraw", "BT_IDEBUG_DRAW__H"},
                {"btMotionState", "BT_MOTIONSTATE_H"},
                {"btSerializer", "BT_SERIALIZER_H"},
                {"btInternalTriangleIndexCallback", "BT_TRIANGLE_CALLBACK_H"},
                {"btTriangleCallback", "BT_TRIANGLE_CALLBACK_H"},
                {"IControl", "_BT_SOFT_BODY_H"},
                {"ImplicitFn", "_BT_SOFT_BODY_H"}
            };
        }

        private void WriteMethodDeclaration(MethodDefinition method, int numParameters, int level, int overloadIndex)
        {
            Write(1, "EXPORT ", WriteTo.Header);

            // Return type
            if (method.IsConstructor)
            {
                Write($"{GetFullNameC(method.Parent)}* ", WriteTo.Header | WriteTo.Source);
            }
            else if (method.OutValueParameter != null)
            {
                Write("void ", WriteTo.Header | WriteTo.Source);
            }
            else
            {
                Write($"{GetTypeNameC(method.ReturnType)} ", WriteTo.Header | WriteTo.Source);
            }


            // Name
            string methodName = method.IsConstructor ? "new" : method.Name;
            if (overloadIndex != 0)
            {
                methodName += (overloadIndex + 1).ToString();
            }
            Write($"{GetFullNameC(method.Parent)}_{methodName}(",
                WriteTo.Header | WriteTo.Source);


            // Parameters
            var parameters = method.Parameters.Take(numParameters);

            if (method.OutValueParameter != null) parameters = parameters.Concat(new[] {
                method.OutValueParameter
            });

            var parametersCpp = parameters.Select(p => $"{GetTypeNameC(p.Type)} {p.Name}");

            // The first parameter is the instance pointer (if not constructor or static method)
            if (!method.IsConstructor && !method.IsStatic)
            {
                parametersCpp =
                    new[] { $"{GetFullNameC(method.Parent)}* obj" }.Concat(parametersCpp);
            }

            WriteLine($"{string.Join(", ", parametersCpp)});", WriteTo.Header);
            WriteLine($"{ListToLines(parametersCpp, WriteTo.Source)})", WriteTo.Source);
        }

        void WriteMethodDefinition(MethodDefinition method, int numParameters, int level, int overloadIndex)
        {
            To = WriteTo.Source;

            if (method.IsDestructor)
            {
                WriteLine(1, "delete obj;", WriteTo.Source);
                return;
            }

            string qualifier = method.IsStatic ? $"{method.Parent.Name}::" : "obj->";

            // Struct field marshalling
            var field = method.Field;
            if (field != null)
            {
                string fieldName = $"{qualifier}{field.Name}";
                if (field.Type.Target != null && field.Type.Target.MarshalAsStruct)
                {
                    string macroPrefix = field.Type.Name.ToUpper();
                    string paramName = (method.OutValueParameter != null ? method.OutValueParameter : method.Parameters[0]).Name;
                    if (method == field.Getter)
                    {
                        WriteLine(1, $"{macroPrefix}_SET({paramName}, {fieldName});");
                    }
                    else
                    {
                        WriteLine(1, $"{macroPrefix}_COPY(&{fieldName}, {paramName});");
                    }
                }
                else if (method == field.Getter)
                {
                    if (field.Type.Kind == TypeKind.Record)
                    {
                        WriteLine(1, $"return &{qualifier}{field.Name};");
                    }
                    else
                    {
                        WriteLine(1, $"return {qualifier}{field.Name};");
                    }
                }
                else
                {
                    WriteLine(1, $"{qualifier}{field.Name} = value;");
                }
                return;
            }

            // Parameter marshalling prologues
            bool needTypeMarshalEpilogue = false;
            foreach (var param in method.Parameters.Take(numParameters))
            {
                if (param.Type.Kind == TypeKind.LValueReference &&
                    param.Type.Referenced.Target != null &&
                    param.Type.Referenced.Target.MarshalAsStruct)
                {
                    string macroPrefix = param.Type.Referenced.Target.Name.ToUpper();
                    if (param.MarshalDirection == MarshalDirection.Out)
                    {
                        WriteLine(1, $"{macroPrefix}_DEF({param.Name});");
                        needTypeMarshalEpilogue = true;
                    }
                    else
                    {
                        WriteLine(1, $"{macroPrefix}_IN({param.Name});");
                        if (param.MarshalDirection == MarshalDirection.InOut)
                        {
                            needTypeMarshalEpilogue = true;
                        }
                    }
                }
            }

            WriteTabs(1);

            if (method.IsConstructor)
            {
                Write($"return new {method.Parent.FullyQualifiedName}");
            }
            else
            {
                if (!method.IsVoid)
                {
                    var returnType = method.ReturnType.Canonical;

                    if (method.OutValueParameter != null)
                    {
                        string macroPrefix = method.OutValueParameter.Type.Referenced.Name.ToUpper();
                        if (returnType.Kind == TypeKind.LValueReference)
                        {
                            Write($"{macroPrefix}_COPY({method.OutValueParameter.Name}, &");
                        }
                        else
                        {
                            Write($"{macroPrefix}_SET({method.OutValueParameter.Name}, ");
                        }
                    }
                    else
                    {
                        if (needTypeMarshalEpilogue)
                        {
                            // Save the return value and return it after the epilogues
                            Write($"{GetTypeNameC(method.ReturnType)} ret = ");
                        }
                        else
                        {
                            // Return immediately
                            Write("return ");
                        }

                        switch (returnType.Kind)
                        {
                            case TypeKind.LValueReference:
                            case TypeKind.Record:
                                if (!(returnType.Target != null && returnType.Target is EnumDefinition))
                                {
                                    Write('&');
                                }
                                break;
                        }
                    }
                }

                Write($"{qualifier}{method.Name}");
            }

            // Call parameters
            var originalParams = method.Parameters.Take(numParameters)
                .Select(p =>
                {
                    if (p.Type.Target != null && p.Type.Target.MarshalAsStruct)
                    {
                        string macroPrefix = p.Type.Target.Name.ToUpper();
                        return $"{macroPrefix}_USE({p.Name})";
                    }

                    if (p.Type.Kind == TypeKind.LValueReference && p.Type.Referenced.Target != null && p.Type.Referenced.Target.MarshalAsStruct)
                    {
                        string macroPrefix = p.Type.Referenced.Target.Name.ToUpper();
                        return $"{macroPrefix}_USE({p.Name})";
                    }

                    var type = p.Type.Canonical;
                    if (type.Kind == TypeKind.LValueReference)
                    {
                        return $"*{p.Name}";
                    }
                    return p.Name;
                });
            Write("(");
            Write($"{ListToLines(originalParams, WriteTo.Source, 1)})");
            if (method.OutValueParameter != null)
            {
                Write(")");
            }
            WriteLine(";");

            // Write type marshalling epilogue
            if (needTypeMarshalEpilogue)
            {
                foreach (var param in method.Parameters.Take(numParameters)
                    .Where(p => p.MarshalDirection == MarshalDirection.Out ||
                        p.MarshalDirection == MarshalDirection.InOut))
                {
                    if (param.Type.Kind == TypeKind.LValueReference &&
                        param.Type.Referenced.Target != null &&
                        param.Type.Referenced.Target.MarshalAsStruct)
                    {
                        string macroPrefix = param.Type.Referenced.Target.Name.ToUpper();
                        WriteLine(1, $"{macroPrefix}_DEF_OUT({param.Name});");
                    }
                }

                if (!method.IsVoid)
                {
                    WriteLine(1, "return ret;");
                }
            }
        }

        void WriteMethod(MethodDefinition method, int level, ref int overloadIndex, int numOptionalParams = 0)
        {
            EnsureWhiteSpace(WriteTo.Source);
            if (!_hasCppClassSeparatingWhitespace)
            {
                WriteLine(WriteTo.Header | WriteTo.Source);
                _hasCppClassSeparatingWhitespace = true;
            }

            EnsureWhiteSpace(WriteTo.Source);

            int numOptionalParamsTotal = method.NumOptionalParameters;
            int numParameters = method.Parameters.Length - numOptionalParamsTotal + numOptionalParams;

            WriteMethodDeclaration(method, numParameters, level, overloadIndex);

            // Method body
            WriteLine('{', WriteTo.Source);
            WriteMethodDefinition(method, numParameters, level, overloadIndex);
            WriteLine('}', WriteTo.Source);
            hasSourceWhiteSpace = false;

            // If there are optional parameters, then output all possible combinations of calls
            overloadIndex++;
            if (numOptionalParams < numOptionalParamsTotal)
            {
                WriteMethod(method, level, ref overloadIndex, numOptionalParams + 1);
            }
        }

        void WriteClass(ClassDefinition @class, int level)
        {
            if (_wrapperHeaderGuards.ContainsKey(@class.Name))
            {
                WriteWrapperClassConstructor(@class);
            }

            // Write child classes
            foreach (var c in @class.NestedClasses
                .Where(c => !IsExcludedClass(c))
                .OrderBy(c => c.Name))
            {
                WriteClass(c, level + 1);
            }

            // Group methods into constructors, destructors and methods
            var methodGroups = @class.Methods.GroupBy(m =>
            {
                if (m.IsExcluded) return (CursorKind)0;
                if (m.IsConstructor) return CursorKind.Constructor;
                if (m.IsDestructor) return CursorKind.Destructor;
                return CursorKind.CxxMethod;
            }).ToDictionary(g => g.Key);

            // Constructors
            if (!@class.HidePublicConstructors && !@class.IsAbstract && !@class.IsStatic)
            {
                IGrouping<CursorKind, MethodDefinition> constructors;
                if (methodGroups.TryGetValue(CursorKind.Constructor, out constructors))
                {
                    int overloadIndex = 0;
                    foreach (var constructor in methodGroups[CursorKind.Constructor])
                    {
                        WriteMethod(constructor, level, ref overloadIndex);
                    }
                }
            }

            // Methods
            IGrouping<CursorKind, MethodDefinition> methods;
            if (methodGroups.TryGetValue(CursorKind.CxxMethod, out methods))
            {
                foreach (var groupByName in methods.GroupBy(m => m.Name))
                {
                    int overloadIndex = 0;
                    foreach (var method in groupByName)
                    {
                        WriteMethod(method, level, ref overloadIndex);
                    }
                }
            }

            // Destructors
            IGrouping<CursorKind, MethodDefinition> destructors;
            if (methodGroups.TryGetValue(CursorKind.Destructor, out destructors))
            {
                int overloadIndex = 0;
                foreach (var method in destructors)
                {
                    WriteMethod(method, level, ref overloadIndex);
                }
            }

            _hasCppClassSeparatingWhitespace = false;
        }

        public void WriteWrapperClass(ClassDefinition @class)
        {
            List<MethodDefinition> baseVirtualMethods;
            var thisVirtualMethods = @class.Methods.Where(x => x.IsVirtual).ToList();
            var virtualMethods = thisVirtualMethods.ToList();
            if (@class.BaseClass != null)
            {
                baseVirtualMethods = @class.BaseClass.Methods.Where(x => x.IsVirtual).ToList();
                virtualMethods.AddRange(baseVirtualMethods);
            }
            else
            {
                baseVirtualMethods = new List<MethodDefinition>();
            }
            var methodCallbacks = virtualMethods.Select(m =>
            {
                string className = baseVirtualMethods.Contains(m) ?
                    GetFullNameC(@class.BaseClass) : GetFullNameC(@class);
                return $"p_{className}_{m.Name} {m.Name}Callback";
            }).ToList();

            if (!_hasCppClassSeparatingWhitespace)
            {
                WriteLine();
                _hasCppClassSeparatingWhitespace = true;
            }

            // TODO: string headerGuard = wrapperHeaderGuards[@class.Name];
            string parameters;
            if (thisVirtualMethods.Count != 0)
            {
                foreach (var method in thisVirtualMethods)
                {
                    string methodPtr = $"p_{GetFullNameC(@class)}_{method.Name}";
                    Write($"typedef {method.ReturnType.Name} (*{methodPtr})(", WriteTo.Header);
                    parameters = ListToLines(method.Parameters
                        .Select(p => $"{GetTypeNameC(p.Type)} {p.Name}"), WriteTo.Header);
                    WriteLine($"{parameters});", WriteTo.Header);
                }
                WriteLine();
            }

            // Wrapper class
            string wrapperClassName = $"{GetFullNameC(@class)}Wrapper";
            WriteLine($"class {wrapperClassName} : public {GetFullNameC(@class)}");
            WriteLine("{");
            WriteLine("private:");
            foreach (var m in virtualMethods)
            {
                string className = baseVirtualMethods.Contains(m) ?
                    GetFullNameC(@class.BaseClass) : GetFullNameC(@class);
                WriteLine(1, $"p_{className}_{m.Name} _{m.Name}Callback;");
            }
            WriteLine();
            WriteLine("public:");

            // Wrapper constructor
            Write(1, $"{wrapperClassName}(");
            string constructorParams = ListToLines(methodCallbacks, WriteTo.Header, 1);
            WriteLine($"{constructorParams});");
            WriteLine();

            // Wrapper methods
            foreach (var m in virtualMethods)
            {
                Write(1, $"virtual {m.ReturnType.Name} {m.Name}(");
                string methodParams = ListToLines(
                    m.Parameters.Select(p => $"{GetTypeNameC(p.Type)} {p.Name}"),
                    WriteTo.Header, 1);
                WriteLine(methodParams + ");");
            }

            WriteLine("};");
            _hasCppClassSeparatingWhitespace = false;


            var prevTo = To;
            To = WriteTo.Source;
            EnsureWhiteSpace();

            // Wrapper C++ Constructor
            Write($"{wrapperClassName}::{wrapperClassName}(");
            parameters = ListToLines(methodCallbacks, WriteTo.Source);
            WriteLine($"{parameters})");
            WriteLine('{');
            foreach (var method in virtualMethods)
            {
                WriteLine(1, string.Format("_{0}Callback = {0}Callback;", method.Name));
            }
            WriteLine('}');
            WriteLine();

            // Wrapper C++ methods
            foreach (var method in virtualMethods)
            {
                Write($"{method.ReturnType.Name} {wrapperClassName}::{method.Name}(");
                parameters = ListToLines(method.Parameters
                    .Select(p => $"{GetTypeNameC(p.Type)} {p.Name}"), WriteTo.Source);
                WriteLine($"{parameters})");

                WriteLine('{');
                WriteTabs(1);
                if (!method.IsVoid)
                {
                    Write("return ");
                }
                Write($"_{method.Name}Callback(");
                parameters = ListToLines(method.Parameters
                    .Select(p => p.Name), WriteTo.Source, 1);
                WriteLine($"{parameters});");
                WriteLine('}');
                WriteLine();
            }
            WriteLine();

            To = prevTo;
        }

        public void WriteWrapperClassConstructor(ClassDefinition @class)
        {
            List<MethodDefinition> baseVirtualMethods;
            var thisVirtualMethods = @class.Methods.Where(x => x.IsVirtual).ToList();
            var virtualMethods = thisVirtualMethods.ToList();
            if (@class.BaseClass != null)
            {
                baseVirtualMethods = @class.BaseClass.Methods.Where(x => x.IsVirtual).ToList();
                virtualMethods.AddRange(baseVirtualMethods);
            }
            else
            {
                baseVirtualMethods = new List<MethodDefinition>();
            }
            var methodCallbacks = virtualMethods.Select(m =>
            {
                string className = baseVirtualMethods.Contains(m) ?
                    GetFullNameC(@class.BaseClass) : GetFullNameC(@class);
                return $"p_{className}_{m.Name} {m.Name}Callback";
            }).ToList();

            if (!_hasCppClassSeparatingWhitespace)
            {
                WriteLine(WriteTo.Header | WriteTo.Source);
                _hasCppClassSeparatingWhitespace = true;
            }
            EnsureWhiteSpace(WriteTo.Source);

            // Wrapper C Constructor
            string wrapperClassName = $"{GetFullNameC(@class)}Wrapper";
            Write(1, "EXPORT ", WriteTo.Header);
            Write($"{wrapperClassName}* {wrapperClassName}_new(", WriteTo.Header | WriteTo.Source);
            WriteLine(ListToLines(methodCallbacks, WriteTo.Header, 1) + ");", WriteTo.Header);
            WriteLine(ListToLines(methodCallbacks, WriteTo.Source) + ")", WriteTo.Source);
            WriteLine('{', WriteTo.Source);
            Write(1, $"return new {wrapperClassName}(", WriteTo.Source);
            WriteLine(
                ListToLines(virtualMethods.Select(m => $"{m.Name}Callback"), WriteTo.Source, 1)
                 + ");", WriteTo.Source);
            WriteLine('}', WriteTo.Source);
            _hasCppClassSeparatingWhitespace = false;
            hasSourceWhiteSpace = false;
        }

        void WriteHeader(HeaderDefinition header, string sourceRootFolder)
        {
            // Entirely skip headers that have no classes
            // TODO: parse C++ methods outside of classes
            if (header.AllClasses.All(@class => @class.IsExcluded)) return;

            // Some headers only need a C# wrapper class, skip C part
            bool hasCWrapper = header.AllClasses.Any(@class =>
                    !@class.IsExcluded &&
                    @class.Methods.Any(m => !m.IsExcluded) &&
                    !(@class is ClassTemplateDefinition));
            if (!hasCWrapper) return;

            // C++ header file
            string headerPath = header.Name + "_wrap.h";
            string headerFullPath = Path.Combine(Project.CProjectPathFull, headerPath);
            OpenFile(headerFullPath, WriteTo.Header);
            WriteLine("#include \"main.h\"");
            WriteLine();

            // C++ source file
            string sourcePath = header.Name + "_wrap.cpp";
            string sourceFullPath = Path.Combine(Project.CProjectPathFull, sourcePath);
            OpenFile(sourceFullPath, WriteTo.Source);

            // C++ #includes
            var includes = new List<string>();
            foreach (var includeHeader in header.Includes.Concat(new[] { header }))
            {
                // No need to include the base class header,
                // it will already be included by the header of this class.
                if (includeHeader != header &&
                    header.AllClasses.Any(c => c.BaseClass != null && c.BaseClass.Header == includeHeader))
                {
                    continue;
                }

                string includePath =
                    WrapperProject.MakeRelativePath(sourceRootFolder, includeHeader.Filename).Replace('\\', '/');
                includes.Add(includePath);
            }
            includes.Sort();
            foreach (var include in includes)
            {
                WriteLine($"#include <{include}>");
            }
            WriteLine();
            if (RequiresConversionHeader(header))
            {
                WriteLine("#include \"conversion.h\"");
            }
            WriteLine($"#include \"{headerPath}\"");


            // Write wrapper class headers
            To = WriteTo.Header;
            _hasCppClassSeparatingWhitespace = true;
            var wrappedClasses = header.AllClasses
                .Where(x => _wrapperHeaderGuards.ContainsKey(x.Name))
                .OrderBy(GetFullNameC).ToList();
            if (wrappedClasses.Count != 0)
            {
                WriteLine($"#ifndef {_wrapperHeaderGuards[wrappedClasses[0].Name]}");
                foreach (var @class in wrappedClasses)
                {
                    foreach (var method in @class.Methods.Where(m => m.IsVirtual))
                    {
                        WriteLine($"#define p_{GetFullNameC(@class)}_{method.Name} void*");
                    }
                }
                foreach (var @class in wrappedClasses)
                {
                    WriteLine($"#define {GetFullNameC(@class)}Wrapper void");
                }
                WriteLine("#else");
                foreach (var @class in wrappedClasses)
                {
                    WriteWrapperClass(@class);
                }
                WriteLine("#endif");
                WriteLine();
            }

            // Write classes
            WriteLine("extern \"C\"");
            WriteLine("{");
            _hasCppClassSeparatingWhitespace = true;

            foreach (var @class in header.Classes
                .Where(c => !IsExcludedClass(c)))
            {
                WriteClass(@class, 1);
            }

            WriteLine('}', WriteTo.Header);

            CloseFile(WriteTo.Header);
            CloseFile(WriteTo.Source);
        }

        public override void Output()
        {
            Directory.CreateDirectory(Project.CProjectPathFull);

            // C++ header file (includes all other headers)
            string includeFilename = Project.NamespaceName + ".h";
            var includeFile = new FileStream(Path.Combine(Project.CProjectPathFull, includeFilename), FileMode.Create, FileAccess.Write);
            var includeWriter = new StreamWriter(includeFile);

            var sourceRootFolders = Project.SourceRootFoldersFull.Select(s => s.Replace('\\', '/'));
            var headers = Project.HeaderDefinitions.Values.Where(h => !h.IsExcluded && !h.Classes.All(c => c.IsExcluded));
            var headersByRoot = headers.GroupBy(h => sourceRootFolders.First(s => h.Filename.StartsWith(s)));
            foreach (var headerGroup in headersByRoot)
            {
                string sourceRootFolder = headerGroup.Key;
                foreach (var header in headerGroup.OrderBy(h => h.Name))
                {
                    WriteHeader(header, sourceRootFolder);

                    // Include header
                    string headerPath = header.Name + "_wrap.h";
                    includeWriter.WriteLine("#include \"{0}\"", headerPath);
                }
            }

            includeWriter.Dispose();
            includeFile.Dispose();

            Console.WriteLine("C wrapper completed");
        }

        private static string GetTypeNameC(TypeRefDefinition type)
        {
            string name = "";
            if (type.IsConst)
            {
                // Note: basic type can be const
                if (type.Referenced == null ||
                    (type.Referenced != null && !type.Referenced.ConstCanonical))
                {
                    name = "const";
                }
            }

            if (type.IsBasic) return name + type.Name;

            if (type.Kind == TypeKind.Typedef)
            {
                if (type.Referenced.IsBasic) return type.Name;
                if (type.Referenced.Kind == TypeKind.Pointer &&
                    type.Referenced.Referenced.Kind == TypeKind.FunctionProto)
                {
                    return type.Name;
                }
                return name + GetTypeNameC(type.Referenced);
            }

            if (type.Referenced != null) return name + GetTypeNameC(type.Referenced) + "*";

            var target = type.Target;
            if (target != null && target is EnumDefinition)
            {
                if (target.Parent != null && target.Parent.IsPureEnum)
                {
                    return target.Parent.FullName.Replace("::", "_");
                }
            }

            // Template name to C form
            if (type.FullName.Contains("<"))
            {
                string template = type.FullName.Replace('<', '_').Replace(">", "").Replace(" *", "Ptr");
                template = template.Replace(' ', '_');
                return name + template.Replace("::", "_");
            }

            return name + type.FullName.Replace("::", "_");
        }

        private static bool RequiresConversionHeader(HeaderDefinition header)
        {
            return header.Classes.Any(RequiresConversionHeader);
        }

        private static bool RequiresConversionHeader(ClassDefinition @class)
        {
            if (@class.NestedClasses.Any(RequiresConversionHeader)) return true;

            foreach (var method in @class.Methods.Where(m => !m.IsExcluded))
            {
                if (DefaultParser.TypeRequiresMarshal(method.ReturnType)) return true;

                if (method.Parameters.Any(p => DefaultParser.TypeRequiresMarshal(p.Type))) return true;
            }

            return false;
        }
    }
}
