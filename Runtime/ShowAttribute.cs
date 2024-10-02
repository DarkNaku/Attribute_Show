using UnityEngine;

namespace DarkNaku.Attribute
{
    public class ShowAttribute : PropertyAttribute
    {
        public string Condition { get; private set; }
        public object[] Parameters { get; private set; }

        public ShowAttribute(string condition, params object[] parameters)
        {
            Condition = condition;
            Parameters = parameters;
        }
    }
}