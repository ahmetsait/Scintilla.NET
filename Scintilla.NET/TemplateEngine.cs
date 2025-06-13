using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScintillaNET;

internal static class TemplateEngine
{
    private enum State
    {
        none,
        variable,
    }

    public static string Render(string template, IReadOnlyDictionary<string, string> variables, bool expandTilde)
    {
        StringBuilder sb = new StringBuilder();
        int cursor = 0;
        State state = State.none;
        void StateNone(int i, char c)
        {
            if (c == '{')
            {
                sb.Append(template, cursor, i - cursor);
                cursor = i;
                state = State.variable;
            }
            else if (expandTilde && c == '~')
            {
                sb.Append(template, cursor, i - cursor);
                sb.Append(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                cursor = i + 1;
            }
        }
        void StateVariable(int i, char c)
        {
            if (c == '}')
            {
                string key = template.Substring(cursor + 1, i - (cursor + 1));
                if (variables.TryGetValue(key, out string val))
                    sb.Append(val);
                else
                    sb.Append(template, cursor, (i + 1) - cursor);
                cursor = i + 1;
                state = State.none;
            }
        }
        for (int i = 0; i < template.Length; i++)
        {
            char c = template[i];
            switch (state)
            {
                case State.none:
                    StateNone(i, c);
                    break;

                case State.variable:
                    StateVariable(i, c);
                    break;
            }
        }
        sb.Append(template, cursor, template.Length - cursor);

        return sb.ToString();
    }
}
