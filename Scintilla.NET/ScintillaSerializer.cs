#if NET
extern alias codedom;
#endif

using System;
#if NETFRAMEWORK
using System.CodeDom;
#endif
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
#if NET
using codedom::System.CodeDom;
using Microsoft.DotNet.DesignTools.Serialization;
#endif

namespace ScintillaNET;

internal class ScintillaSerializer : CodeDomSerializer
{
    private static readonly Dictionary<string, int> propertyPriorityMap = [];

    private static readonly string[] modulePathPropertyNames = [
        nameof(Scintilla.ScintillaX86ModulePath),
        nameof(Scintilla.ScintillaX64ModulePath),
        nameof(Scintilla.ScintillaArm64ModulePath),
        nameof(Scintilla.LexillaX86ModulePath),
        nameof(Scintilla.LexillaX64ModulePath),
        nameof(Scintilla.LexillaArm64ModulePath),
    ];

    private static readonly Dictionary<string, string> modulePathPropertyDefaults = [];

    static ScintillaSerializer()
    {
        foreach (PropertyInfo pi in typeof(Scintilla).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var attr = pi.GetCustomAttribute<CodeDomPriorityAttribute>(inherit: true);
            if (attr != null)
            {
                propertyPriorityMap[pi.Name] = attr.Priority;
            }
        }

        foreach (string name in modulePathPropertyNames)
        {
            PropertyInfo prop = typeof(Scintilla).GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
            DefaultValueAttribute attr = prop.GetCustomAttribute<DefaultValueAttribute>(true);
            if (attr != null)
                modulePathPropertyDefaults[name] = attr.Value as string;
        }
    }

    public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
    {
        return (manager.GetSerializer(typeof(Control), typeof(CodeDomSerializer)) as CodeDomSerializer)?.Deserialize(manager, codeObject);
    }

    public override object Serialize(IDesignerSerializationManager manager, object value)
    {
        Scintilla scintilla = (Scintilla)value;
        CodeDomSerializer codeDomSerializer = manager.GetSerializer(typeof(Control), typeof(CodeDomSerializer)) as CodeDomSerializer;
        object code = codeDomSerializer?.Serialize(manager, value);
        CodeStatementCollection statementCollection = (CodeStatementCollection)code;
        //if (code is CodeStatementCollection statementCollection)
        {
            IEnumerable<CodeStatement> GetStatements() => statementCollection.Cast<CodeStatement>();

            CodeStatement[] pre = GetStatements()
                .TakeWhile(s => s is not CodeAssignStatement a || a.Left is not CodePropertyReferenceExpression)
                .ToArray();
            CodeAssignStatement[] assignments = GetStatements()
                .SkipWhile(s => s is not CodeAssignStatement a || a.Left is not CodePropertyReferenceExpression)
                .TakeWhile(s => s is CodeAssignStatement a && a.Left is CodePropertyReferenceExpression)
                .Cast<CodeAssignStatement>()
                .ToArray();
            CodeStatement[] post = GetStatements()
                .SkipWhile(s => s is not CodeAssignStatement a || a.Left is not CodePropertyReferenceExpression)
                .SkipWhile(s => s is CodeAssignStatement a && a.Left is CodePropertyReferenceExpression)
                .ToArray();

            List<CodeAssignStatement> modulePathAssignments = new List<CodeAssignStatement>(6);

            if (scintilla.ScintillaX86ModulePath != modulePathPropertyDefaults[nameof(scintilla.ScintillaX86ModulePath)])
                modulePathAssignments.Add(
                    new CodeAssignStatement(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(typeof(Scintilla)), nameof(Scintilla.GlobalScintillaX86ModulePath)
                        ),
                        new CodePrimitiveExpression(scintilla.ScintillaX86ModulePath)
                    )
                );
            if (scintilla.ScintillaX64ModulePath != modulePathPropertyDefaults[nameof(scintilla.ScintillaX64ModulePath)])
                modulePathAssignments.Add(
                    new CodeAssignStatement(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(typeof(Scintilla)), nameof(Scintilla.GlobalScintillaX64ModulePath)
                        ),
                        new CodePrimitiveExpression(scintilla.ScintillaX64ModulePath)
                    )
                );
            if (scintilla.ScintillaArm64ModulePath != modulePathPropertyDefaults[nameof(scintilla.ScintillaArm64ModulePath)])
                modulePathAssignments.Add(
                    new CodeAssignStatement(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(typeof(Scintilla)), nameof(Scintilla.GlobalScintillaArm64ModulePath)
                        ),
                        new CodePrimitiveExpression(scintilla.ScintillaArm64ModulePath)
                    )
                );
            if (scintilla.LexillaX86ModulePath != modulePathPropertyDefaults[nameof(scintilla.LexillaX86ModulePath)])
                modulePathAssignments.Add(
                    new CodeAssignStatement(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(typeof(Scintilla)), nameof(Scintilla.GlobalLexillaX86ModulePath)
                        ),
                        new CodePrimitiveExpression(scintilla.LexillaX86ModulePath)
                    )
                );
            if (scintilla.LexillaX64ModulePath != modulePathPropertyDefaults[nameof(scintilla.LexillaX64ModulePath)])
                modulePathAssignments.Add(
                    new CodeAssignStatement(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(typeof(Scintilla)), nameof(Scintilla.GlobalLexillaX64ModulePath)
                        ),
                        new CodePrimitiveExpression(scintilla.LexillaX64ModulePath)
                    )
                );
            if (scintilla.LexillaArm64ModulePath != modulePathPropertyDefaults[nameof(scintilla.LexillaArm64ModulePath)])
                modulePathAssignments.Add(
                    new CodeAssignStatement(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(typeof(Scintilla)), nameof(Scintilla.GlobalLexillaArm64ModulePath)
                        ),
                        new CodePrimitiveExpression(scintilla.LexillaArm64ModulePath)
                    )
                );

            Array.Sort(assignments,
                (x1, x2) => {
                    var propRef1 = (CodePropertyReferenceExpression)x1.Left;
                    var propRef2 = (CodePropertyReferenceExpression)x2.Left;
                    if (!propertyPriorityMap.TryGetValue(propRef1.PropertyName, out int prio1))
                        prio1 = 0;
                    if (!propertyPriorityMap.TryGetValue(propRef2.PropertyName, out int prio2))
                        prio2 = 0;
                    if (prio1 != prio2)
                        return prio1 - prio2;
                    else
                        return string.CompareOrdinal(propRef1.PropertyName, propRef2.PropertyName);
                }
            );

            statementCollection.Clear();

            foreach (CodeAssignStatement stmt in modulePathAssignments)
                statementCollection.Add(stmt);
            statementCollection.AddRange(pre);
            statementCollection.AddRange(assignments);
            statementCollection.AddRange(post);
        }
        return code;
    }
}
