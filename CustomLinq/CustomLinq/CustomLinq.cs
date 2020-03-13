using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomLinq
{
    public static class CustomLinq
    {
        public static IEnumerable<T> CustomWhere<T>(this IEnumerable<T> source, Func<T, bool> filter)
        {
            foreach (var item in source)
            {
                if (filter(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> CustomSelect<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            foreach (var item in source)
            {
                var newItem = selector(item);
                yield return newItem;
            }
        }

        public static IEnumerable<TResult> CustomOfType<TSource, TResult>(this IEnumerable<TSource> source) where TResult : class
        {
            foreach(var item in source)
            {
                if(item is TResult itemResult)
                {
                    yield return itemResult;
                }
            }
        }

        public static TSource CustomFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if(predicate(item))
                    return item;
            }

            throw new InvalidOperationException();
        }

        public static TSource CustomFirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)        
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    return item;
            }
            return default;
        }

        public static IEnumerable<TResult> CustomGroupBy<TSource, TKey, TElement, TResult>(
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            var groups = new Dictionary<TKey, List<TElement>>();
            
            foreach (var item in source)
            {
                if (!groups.TryGetValue(keySelector(item), out var existing))
                {
                    existing = new List<TElement>();
                    groups[keySelector(item)] = existing;
                }
                existing.Add(elementSelector(item));
            }

            foreach (var group in groups)
            {
                yield return resultSelector(group.Key, group.Value);
            }
        }

       public static IEnumerable<TSource> CustomOrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var groups = new Dictionary<TKey, List<TSource>>();
            var keys = new List<TKey>();

            foreach (var item in source)
            {
                if (!groups.TryGetValue(keySelector(item), out var existing))
                {
                    existing = new List<TSource>();
                    groups[keySelector(item)] = existing;
                }
                existing.Add(item);
            }

            foreach(var key in groups.Keys)
            {
                keys.Add(key);
            }

            keys.Sort();
            foreach (var key in keys)
            {
                if (groups.TryGetValue(key, out var existing))
                {
                    foreach(var item in existing)
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IEnumerable<TSource> CustomDistinct<TSource>(this IEnumerable<TSource> source)
        {
            var itemsList = new List<TSource>();
            foreach (var item in source)
            {
                if (!itemsList.Contains(item))
                {
                    itemsList.Add(item);
                    yield return item;
                }
            }
        }

        public static bool CustomAny<TSource>(this IEnumerable<TSource> source)
        {
            var i = 0;
            foreach (var item in source)
            {
                i++;
            }
            return i > 0;
        }

        public static bool CustomAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    return true;
            }
            return false;
        }

        public static bool CustomAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (!predicate(item))
                    return false;
            }
            return true;
        }
    }
}
