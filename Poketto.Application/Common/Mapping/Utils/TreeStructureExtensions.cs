using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poketto.Application.Common.Mapping.Utils
{
    public static class TreeExtensions
    {
        public static IEnumerable<TNode> Flatten<TNode>(this IEnumerable<TNode> nodes, Func<TNode, IEnumerable<TNode>> childrenSelector)
        {
            if (nodes == null) throw new ArgumentNullException(nameof(nodes)); 
            
            return nodes.SelectMany(c => childrenSelector(c)
                        .Flatten(childrenSelector))
                        .Concat(nodes);
        }

        public static ITree<T> ToTree<T>(this IList<T> items, Func<T, T, bool> parentSelector)
        {
            if (items == null) throw new ArgumentNullException(nameof(items)); 
            
            var lookup = items.ToLookup(item => items.FirstOrDefault(parent => parentSelector(parent, item)), child => child); 
            
            return Tree<T>.FromLookup(lookup);
        }
    }

    public interface ITree<T>
    {
        T Data { get; }
        ITree<T> Parent { get; }
        ICollection<ITree<T>> Children { get; }
        bool IsRoot { get; }
        bool IsLeaf { get; }
        int Level { get; }

    }

    public class Tree<T> : ITree<T>
    {
        private Tree(T data)
        {
            Children = new LinkedList<ITree<T>>();
            Data = data;
        }

        public static Tree<T> FromLookup(ILookup<T, T> lookup)
        {
            var rootData = lookup.Count == 1 ? lookup.First().Key : default;
            var root = new Tree<T>(rootData);
            root.LoadChildren(lookup);
            return root;
        }
        private void LoadChildren(ILookup<T, T> lookup)
        {
            foreach (var data in lookup[Data])
            {
                var child = new Tree<T>(data) { Parent = this };
                Children.Add(child);
                child.LoadChildren(lookup);
            }
        }

        public T Data { get; }
        public ITree<T> Parent { get; private set; }
        public ICollection<ITree<T>> Children { get; }
        public bool IsRoot => Parent == null;
        public bool IsLeaf => Children.Count < 1;
        public int Level => IsRoot ? 0 : Parent.Level + 1;
    }

}
