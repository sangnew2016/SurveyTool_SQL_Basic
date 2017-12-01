using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace LearningPlatform.Domain.Common
{
    public class TypeNameSerializationBinder : SerializationBinder
    {
        private readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();
        private readonly string _namespaceName;
        private readonly string[] _excludedNamespaces = { "LearningPlatform.Domain.SurveyExecution" };


        public TypeNameSerializationBinder()
        {
            var assembly = Assembly.GetAssembly(GetType());
            _namespaceName = assembly.GetName().Name;
            foreach (Type type in assembly.GetTypes())
            {
                bool excluded = _excludedNamespaces.Any(excludedNamespace => type.Namespace != null && type.Namespace.StartsWith(excludedNamespace));
                if (!excluded)
                {
                    _types[type.Name] = type;
                }
            }
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            if (serializedType.Namespace != null && serializedType.Namespace.StartsWith(_namespaceName))
            {
                assemblyName = null;
                typeName = serializedType.Name;
            }
            else
            {
                assemblyName = serializedType.Assembly.FullName;
                typeName = serializedType.FullName;
            }
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            if (string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
            {
                return _types[typeName];
            }
            return Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
        }
    }
}
