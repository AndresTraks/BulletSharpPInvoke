using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClangSharp;

namespace BulletSharpGen
{
    class ReaderContext
    {
        public TranslationUnit TranslationUnit { get; set; }
        public HeaderDefinition Header { get; set; }
        public string Namespace { get; set; }
        public ClassDefinition Class { get; set; }
        public MethodDefinition Method { get; set; }
        public ParameterDefinition Parameter { get; set; }
        public FieldDefinition Field { get; set; }

        public AccessSpecifier MemberAccess { get; set; }
    }

    class CppReader
    {
        Index index;
        List<string> headerQueue = new List<string>();
        List<string> clangOptions = new List<string>();
        HashSet<string> excludedMethods = new HashSet<string>();

        ReaderContext _context = new ReaderContext();
        WrapperProject project;

        public CppReader(WrapperProject project)
        {
            this.project = project;

            foreach (string sourceRelDir in project.SourceRootFolders)
            {
                string sourceFullDir = Path.GetFullPath(sourceRelDir).Replace('\\', '/');

                // Enumerate all header files in the source tree
                var headerFiles = Directory.EnumerateFiles(sourceFullDir, "*.h", SearchOption.AllDirectories);
                foreach (string headerFullDir in headerFiles)
                {
                    string headerFullDirCanonical = headerFullDir.Replace('\\', '/');
                    string headerRelDir = headerFullDirCanonical.Substring(sourceFullDir.Length);

                    HeaderDefinition header;
                    if (project.HeaderDefinitions.TryGetValue(headerFullDirCanonical, out header))
                    {
                        if (header.IsExcluded)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("New file {0}", headerFullDirCanonical);
                    }

                    headerQueue.Add(headerFullDirCanonical);
                }

                // Include directory
                clangOptions.Add("-I");
                clangOptions.Add(sourceFullDir);
            }

            // WorldImporter include directory
            //clangOptions.Add("-I");
            //clangOptions.Add(src + "../Extras/Serialize/BulletWorldImporter");

            // Specify C++ headers, not C ones
            clangOptions.Add("-x");
            clangOptions.Add("c++-header");

            //clangOptions.Add("-DUSE_DOUBLE_PRECISION");

            // Exclude irrelevant methods
            excludedMethods.Add("operator new");
            excludedMethods.Add("operator delete");
            excludedMethods.Add("operator new[]");
            excludedMethods.Add("operator delete[]");
            excludedMethods.Add("operator+=");
            excludedMethods.Add("operator-=");
            excludedMethods.Add("operator*=");
            excludedMethods.Add("operator/=");
            excludedMethods.Add("operator==");
            excludedMethods.Add("operator!=");
            excludedMethods.Add("operator()");

            Console.Write("Reading headers");

            index = new Index();

            while (headerQueue.Count != 0)
            {
                ReadHeader(headerQueue[0]);
            }
            /*
            if (Directory.Exists(src + "..\\Extras\\"))
            {
                ReadHeader(src + "..\\Extras\\Serialize\\BulletFileLoader\\btBulletFile.h");
                ReadHeader(src + "..\\Extras\\Serialize\\BulletWorldImporter\\btBulletWorldImporter.h");
                ReadHeader(src + "..\\Extras\\Serialize\\BulletWorldImporter\\btWorldImporter.h");
                ReadHeader(src + "..\\Extras\\Serialize\\BulletXmlWorldImporter\\btBulletXmlWorldImporter.h");
            }
            */
            index.Dispose();

            Console.WriteLine();
            Console.WriteLine("Read complete - headers: {0}, classes: {1}",
                project.HeaderDefinitions.Count, project.ClassDefinitions.Count);


            foreach (var @class in project.ClassDefinitions.Values.Where(c => !c.IsParsed))
            {
                Console.WriteLine("Class removed: {0}", @class.FullyQualifiedName);
            }
        }

        Cursor.ChildVisitResult HeaderVisitor(Cursor cursor, Cursor parent)
        {
            string filename = cursor.Extent.Start.File.Name.Replace('\\', '/');

            // Skip this header if it's not part of any project source folders
            if (project.SourceRootFolders.All(s => !filename.StartsWith(s.Replace('\\', '/'), StringComparison.OrdinalIgnoreCase)))
            {
                return Cursor.ChildVisitResult.Continue;
            }

            // Have we visited this header already?
            if (project.HeaderDefinitions.ContainsKey(filename))
            {
                _context.Header = project.HeaderDefinitions[filename];
            }
            else
            {
                // No, define a new one
                _context.Header = new HeaderDefinition(filename);
                project.HeaderDefinitions.Add(filename, _context.Header);
                headerQueue.Remove(filename);
            }

            if (cursor.Kind == CursorKind.Namespace)
            {
                _context.Namespace = cursor.Spelling;
                return Cursor.ChildVisitResult.Recurse;
            }
            if (cursor.IsDefinition)
            {
                switch (cursor.Kind)
                {
                    case CursorKind.ClassDecl:
                    case CursorKind.ClassTemplate:
                    case CursorKind.EnumDecl:
                    case CursorKind.StructDecl:
                    case CursorKind.TypedefDecl:
                        ParseClassCursor(cursor);
                        break;
                }
            }

            return Cursor.ChildVisitResult.Continue;
        }

        Cursor.ChildVisitResult EnumVisitor(Cursor cursor, Cursor parent)
        {
            if (cursor.Kind == CursorKind.EnumConstantDecl)
            {
                var @enum = _context.Class as EnumDefinition;
                @enum.EnumConstants.Add(cursor.Spelling);
                @enum.EnumConstantValues.Add("");
            }
            else if (cursor.Kind == CursorKind.IntegerLiteral)
            {
                var @enum = _context.Class as EnumDefinition;
                Token intLiteralToken = _context.TranslationUnit.Tokenize(cursor.Extent).First();
                @enum.EnumConstantValues[@enum.EnumConstants.Count - 1] = intLiteralToken.Spelling;
            }
            else if (cursor.Kind == CursorKind.ParenExpr)
            {
                return Cursor.ChildVisitResult.Continue;
            }
            return Cursor.ChildVisitResult.Recurse;
        }

        void ParseClassCursor(Cursor cursor)
        {
            string className = cursor.Spelling;

            // Unnamed struct
            // A combined "typedef struct {}" definition is split into separate struct and typedef statements
            // where the struct is also a child of the typedef, so the struct can be skipped for now.
            if (string.IsNullOrEmpty(className) && cursor.Kind == CursorKind.StructDecl)
            {
                return;
            }

            string fullyQualifiedName = TypeRefDefinition.GetFullyQualifiedName(cursor);
            if (project.ClassDefinitions.ContainsKey(fullyQualifiedName))
            {
                if (project.ClassDefinitions[fullyQualifiedName].IsParsed)
                {
                    return;
                }
                var parent = _context.Class;
                _context.Class = project.ClassDefinitions[fullyQualifiedName];
                _context.Class.Parent = parent;
            }
            else
            {
                if (cursor.Kind == CursorKind.ClassTemplate)
                {
                    _context.Class = new ClassTemplateDefinition(className, _context.Header, _context.Class);
                }
                else if (cursor.Kind == CursorKind.EnumDecl)
                {
                    _context.Class = new EnumDefinition(className, _context.Header, _context.Class);
                }
                else
                {
                    _context.Class = new ClassDefinition(className, _context.Header, _context.Class);
                }

                _context.Class.NamespaceName = _context.Namespace;

                if (_context.Class.FullyQualifiedName != fullyQualifiedName)
                {
                    // TODO
                }
                project.ClassDefinitions.Add(fullyQualifiedName, _context.Class);
            }

            _context.Class.IsParsed = true;

            // Unnamed struct escapes to the surrounding scope
            if (!(string.IsNullOrEmpty(className) && cursor.Kind == CursorKind.StructDecl))
            {
                if (_context.Class.Parent != null)
                {
                    _context.Class.Parent.Classes.Add(_context.Class);
                }
                else
                {
                    _context.Header.Classes.Add(_context.Class);
                }
            }

            AccessSpecifier parentMemberAccess = _context.MemberAccess;

            // Default class/struct access specifier
            if (cursor.Kind == CursorKind.ClassDecl)
            {
                _context.MemberAccess = AccessSpecifier.Private;
            }
            else if (cursor.Kind == CursorKind.StructDecl)
            {
                _context.Class.IsStruct = true;
                _context.MemberAccess = AccessSpecifier.Public;
            }
            else if (cursor.Kind == CursorKind.ClassTemplate)
            {
                if (cursor.TemplateCursorKind != CursorKind.ClassDecl)
                {
                    _context.MemberAccess = AccessSpecifier.Private;
                }
                else
                {
                    _context.MemberAccess = AccessSpecifier.Public;
                }
            }

            if (cursor.Kind == CursorKind.EnumDecl)
            {
                cursor.VisitChildren(EnumVisitor);
            }
            else if (cursor.Kind == CursorKind.TypedefDecl)
            {
                _context.Class.IsTypedef = true;
                if (cursor.TypedefDeclUnderlyingType.Canonical.TypeKind != ClangSharp.Type.Kind.FunctionProto)
                {
                    _context.Class.TypedefUnderlyingType = new TypeRefDefinition(cursor.TypedefDeclUnderlyingType);
                }
            }
            else
            {
                cursor.VisitChildren(ClassVisitor);
            }

            // Restore parent state
            _context.Class = _context.Class.Parent;
            _context.MemberAccess = parentMemberAccess;
        }

        Cursor.ChildVisitResult MethodTemplateTypeVisitor(Cursor cursor, Cursor parent)
        {
            if (cursor.Kind == CursorKind.TypeRef)
            {
                if (cursor.Referenced.Kind == CursorKind.TemplateTypeParameter)
                {
                    if (_context.Parameter != null)
                    {
                        _context.Parameter.Type.HasTemplateTypeParameter = true;
                    }
                    else
                    {
                        _context.Method.ReturnType.HasTemplateTypeParameter = true;
                    }
                    return Cursor.ChildVisitResult.Break;
                }
            }
            else if (cursor.Kind == CursorKind.TemplateRef)
            {
                // TODO
                return Cursor.ChildVisitResult.Recurse;
            }
            return Cursor.ChildVisitResult.Continue;
        }

        Cursor.ChildVisitResult FieldTemplateTypeVisitor(Cursor cursor, Cursor parent)
        {
            switch (cursor.Kind)
            {
                case CursorKind.TypeRef:
                    if (cursor.Referenced.Kind == CursorKind.TemplateTypeParameter)
                    {
                        _context.Field.Type.HasTemplateTypeParameter = true;
                    }
                    return Cursor.ChildVisitResult.Break;

                case CursorKind.TemplateRef:
                    if (parent.Type.Declaration.Kind == CursorKind.ClassTemplate)
                    {
                        _context.Field.Type.HasTemplateTypeParameter = true;
                        return Cursor.ChildVisitResult.Break;
                    }
                    return Cursor.ChildVisitResult.Continue;

                default:
                    return Cursor.ChildVisitResult.Continue;
            }
        }

        Cursor.ChildVisitResult ClassVisitor(Cursor cursor, Cursor parent)
        {
            switch (cursor.Kind)
            {
                case CursorKind.CxxAccessSpecifier:
                    _context.MemberAccess = cursor.AccessSpecifier;
                    return Cursor.ChildVisitResult.Continue;
                case CursorKind.CxxBaseSpecifier:
                    string baseName = TypeRefDefinition.GetFullyQualifiedName(cursor.Type);
                    ClassDefinition baseClass;
                    if (!project.ClassDefinitions.TryGetValue(baseName, out baseClass))
                    {
                        Console.WriteLine("Base {0} for {1} not found! Missing header?", baseName, _context.Class.Name);
                        return Cursor.ChildVisitResult.Continue;
                    }
                    _context.Class.BaseClass = baseClass;
                    return Cursor.ChildVisitResult.Continue;
                case CursorKind.TemplateTypeParameter:
                    var classTemplate = _context.Class as ClassTemplateDefinition;
                    if (classTemplate.TemplateTypeParameters == null)
                    {
                        classTemplate.TemplateTypeParameters = new List<string>();
                    }
                    classTemplate.TemplateTypeParameters.Add(cursor.Spelling);
                    return Cursor.ChildVisitResult.Continue;
            }

            // We only care about public members
            if (_context.MemberAccess != AccessSpecifier.Public)
            {
                // And also private/protected virtual methods that override public abstract methods,
                // necessary for checking whether a class is abstract or not.
                if (cursor.IsPureVirtualCxxMethod || !cursor.IsVirtualCxxMethod)
                {
                    return Cursor.ChildVisitResult.Continue;
                }
            }

            if ((cursor.Kind == CursorKind.ClassDecl || cursor.Kind == CursorKind.StructDecl ||
                cursor.Kind == CursorKind.ClassTemplate || cursor.Kind == CursorKind.TypedefDecl ||
                cursor.Kind == CursorKind.EnumDecl) && cursor.IsDefinition)
            {
                ParseClassCursor(cursor);
            }
            else if (cursor.Kind == CursorKind.CxxMethod || cursor.Kind == CursorKind.Constructor)
            {
                string methodName = cursor.Spelling;
                if (excludedMethods.Contains(methodName))
                {
                    return Cursor.ChildVisitResult.Continue;
                }

                _context.Method = new MethodDefinition(methodName, _context.Class, cursor.NumArguments)
                {
                    ReturnType = new TypeRefDefinition(cursor.ResultType),
                    IsConstructor = cursor.Kind == CursorKind.Constructor,
                    IsStatic = cursor.IsStaticCxxMethod,
                    IsVirtual = cursor.IsVirtualCxxMethod,
                    IsAbstract = cursor.IsPureVirtualCxxMethod
                };

                // Check if the return type is a template
                cursor.VisitChildren(MethodTemplateTypeVisitor);

                // Parse arguments
                for (uint i = 0; i < cursor.NumArguments; i++)
                {
                    Cursor arg = cursor.GetArgument(i);

                    string parameterName = arg.Spelling;
                    if (parameterName.Length == 0)
                    {
                        parameterName = "__unnamed" + i;
                    }

                    _context.Parameter = new ParameterDefinition(parameterName, new TypeRefDefinition(arg.Type));
                    _context.Method.Parameters[i] = _context.Parameter;
                    arg.VisitChildren(MethodTemplateTypeVisitor);

                    // Check for a default value (optional parameter)
                    var argTokens = _context.TranslationUnit.Tokenize(arg.Extent);
                    if (argTokens.Any(a => a.Spelling.Equals("=")))
                    {
                        _context.Parameter.IsOptional = true;
                    }

                    // Get marshalling direction
                    if (_context.Parameter.Type.IsPointer || _context.Parameter.Type.IsReference)
                    {
                        if (_context.Parameter.MarshalDirection != MarshalDirection.Out &&
                            !_context.TranslationUnit.Tokenize(arg.Extent).Any(a => a.Spelling.Equals("const")))
                        {
                            _context.Parameter.MarshalDirection = MarshalDirection.InOut;
                        }
                    }

                    _context.Parameter = null;
                }

                // Discard any private/protected virtual method unless it
                // implements a public abstract method
                if (_context.MemberAccess != AccessSpecifier.Public)
                {
                    if (_context.Method.Parent.BaseClass == null ||
                        !_context.Method.Parent.BaseClass.AbstractMethods.Contains(_context.Method))
                    {
                        _context.Method.Parent.Methods.Remove(_context.Method);
                    }
                }

                _context.Method = null;
            }
            else if (cursor.Kind == CursorKind.FieldDecl)
            {
                _context.Field = new FieldDefinition(cursor.Spelling,
                    new TypeRefDefinition(cursor.Type), _context.Class);
                if (!cursor.Type.Declaration.SpecializedCursorTemplate.IsInvalid)
                {
                    if (cursor.Children[0].Kind != CursorKind.TemplateRef)
                    {
                        throw new InvalidOperationException();
                    }
                    if (cursor.Children.Count == 1)
                    {
                        string displayName = cursor.Type.Declaration.DisplayName;
                        int typeStart = displayName.IndexOf('<') + 1;
                        int typeEnd = displayName.LastIndexOf('>');
                        displayName = displayName.Substring(typeStart, typeEnd - typeStart);
                        var specializationTypeRef = new TypeRefDefinition
                        {
                            IsBasic = true,
                            Name = displayName
                        };
                        _context.Field.Type.SpecializedTemplateType = specializationTypeRef;
                    }
                    if (cursor.Children.Count == 2)
                    {
                        if (cursor.Children[1].Type.TypeKind != ClangSharp.Type.Kind.Invalid)
                        {
                            _context.Field.Type.SpecializedTemplateType = new TypeRefDefinition(cursor.Children[1].Type);
                        }
                        else
                        {
                            // TODO
                        }
                    }
                }
                //cursor.VisitChildren(FieldTemplateTypeVisitor);
                _context.Field = null;
            }
            else if (cursor.Kind == CursorKind.UnionDecl)
            {
                return Cursor.ChildVisitResult.Recurse;
            }
            else
            {
                //Console.WriteLine(cursor.Spelling);
            }
            return Cursor.ChildVisitResult.Continue;
        }

        void ReadHeader(string headerFile)
        {
            Console.Write('.');

            var unsavedFiles = new UnsavedFile[] { };
            using (_context.TranslationUnit = index.CreateTranslationUnit(headerFile, clangOptions.ToArray(), unsavedFiles, TranslationUnitFlags.SkipFunctionBodies))
            {
                var cur = _context.TranslationUnit.Cursor;
                _context.Namespace = "";
                cur.VisitChildren(HeaderVisitor);
            }
            _context.TranslationUnit = null;
            headerQueue.Remove(headerFile);
        }
    }
}
