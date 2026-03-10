using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScintillaNET;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
internal sealed class CodeDomPriorityAttribute(int priority) : Attribute
{
    public int Priority => priority;
}
