using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace LearningPlatform.Data.Memory.Repository
{
    public class SurveyMemoryContext
    {
        private readonly List<object> _objects = new List<object>();

        public void Clear()
        {
            _objects.Clear();
        }

        public void Add(object obj)
        {
            var allObjects = new List<object>();
            GetAllObjects(obj, new HashSet<object>(), allObjects);
            foreach (var o in allObjects)
            {
                if (!_objects.Contains(o))
                {
                    _objects.Add(o);
                }
            }
        }

        private void GetAllObjects(object obj, HashSet<object> objectsSeen, List<object> found)
        {
            if (obj == null || objectsSeen.Contains(obj)) return;
            if (IsSimpleType(obj)) return;


            objectsSeen.Add(obj);
            IEnumerable enumerable = obj as IEnumerable;
            if (enumerable != null)
            {
                foreach (object o in enumerable)
                {
                    GetAllObjects(o, objectsSeen, found);
                }
                return;
            }

            found.Add(obj);
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                var newValue = property.GetValue(obj, null);
                GetAllObjects(newValue, objectsSeen, found);
            }
        }

        private static bool IsSimpleType(object obj)
        {
            return Convert.GetTypeCode(obj) != TypeCode.Object || obj is Guid;
        }

        public void AssignIds()
        {
            Dictionary<Type, long> maxIdDictionary = GetMaxIdDictionary();

            foreach (var obj in _objects)
            {
                var id = GetIdValue(obj);
                if (!id.HasValue) continue;

                var type = GetTypeUsedForMaxId(obj);
                var currentMax = maxIdDictionary[type];
                if (id == 0)
                {
                    var newMax = currentMax + 1;
                    maxIdDictionary[type] = newMax;
                    SetIdValue(obj, newMax);
                }
            }
        }

        private Dictionary<Type, long> GetMaxIdDictionary()
        {
            var maxIds = new Dictionary<Type, long>();
            foreach (var obj in _objects)
            {
                long maxIdForType;
                var type = GetTypeUsedForMaxId(obj);
                maxIds.TryGetValue(type, out maxIdForType);
                long? id = GetIdValue(obj);
                if (id.HasValue)
                {
                    maxIds[type] = Math.Max(maxIdForType, id.Value);
                }
            }
            return maxIds;
        }

        private long? GetIdValue(object obj)
        {
            var idPropertyInfo = GetIdPropertyInfo(obj);
            if (idPropertyInfo != null && idPropertyInfo.CanRead)
            {
                return (long)idPropertyInfo.GetValue(obj, null);
            }
            return null;
        }

        private static Type GetTypeUsedForMaxId(object obj)
        {
            //if (obj is QuestionDefinition)
            //{
            //    return typeof(QuestionDefinition);
            //}
            //if (obj is Node)
            //{
            //    return typeof(Node);
            //}

            return obj.GetType();
        }

        private void SetIdValue(object obj, long id)
        {
            var idPropertyInfo = GetIdPropertyInfo(obj);
            if (idPropertyInfo != null && idPropertyInfo.CanWrite)
            {
                idPropertyInfo.SetValue(obj, id, null);
            }
        }

        private static PropertyInfo GetIdPropertyInfo(object obj)
        {
            return obj.GetType().GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
        }
    }
}
