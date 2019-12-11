using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCollections
{
    class Tree<T> where T : IComparable
    {

        TreeNode<T> _root;

        public Tree()
        {
            _root = new TreeNode<T>(default(T));
        }

        public TreeNode<T> Root
        {
            get
            {
                return _root;
            }
        }

        public List<T> GetAll()
        {
            List<T> allItems = new List<T>();
            GetAllHelper(_root, allItems);

            return allItems;
        }


        public void AppendChildTo(T targetParent, T child)
        {
            //TODO
        }

        public T Find(Predicate<T> match)
        {
            List<T> list = GetAll();

            T result = list.Find(match);

            return result;
        }
        public IEnumerable<T> Where(Func<T,bool> match)
        {
            List<T> list = GetAll();
            if (list == null || list.Count < 1)
            {
                return new List<T>();
            }
            IEnumerable<T> result = list.Where(match);

            return result;
        }


        private void GetAllHelper(TreeNode<T> root, List<T> list)
        {
            if (root != null)
            {
                if (root.Data != null)
                {
                    list.Add(root.Data);
                }
                      
                foreach (var node in root.Nodes)
                {
                    GetAllHelper(node, list);
                }
            }
        }
    }


    class TreeNode<T> : IComparable where T : IComparable
    {

        public T Data { get; set; }

        //TODO... possible option to use LinkedList for traversing siblings
        private List<TreeNode<T>> _children;
        private TreeNode<T> _parent;

        public TreeNode(T data)
        {
            Data = data;
            _children = new List<TreeNode<T>>();
        }

        public void AddChild(T data)
        {

            TreeNode<T> node = new TreeNode<T>(data);
            node._parent = this;
            _children.Add(node);
        }

        public bool RemoveChild(T data)
        {
            int countBefore = _children.Count;
            _children.Remove(new TreeNode<T>(data));

            int countAfter = _children.Count;

            return countAfter != countBefore;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            TreeNode<T> other = (TreeNode<T>)obj;
            T data = other.Data;
            if (other != null)
                return this.Data.CompareTo(data);
            else
                throw new ArgumentException($"Object is not a {nameof(T)}");
        }

        public override bool Equals(object o)
        {
            TreeNode<T> other = o as TreeNode<T>;
            return other.Data.Equals(Data);
        }


        public override int GetHashCode()
        {

            return Data.GetHashCode();
        }

        public List<TreeNode<T>> Nodes
        {
            get
            {
                return _children;
            }
            private set { }
        }

        public TreeNode<T> Parent
        {
            get
            {
                return _parent;
            }
            private set { }
        }
    }
}
