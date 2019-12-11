using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringsTrees
{
    class Program
    {
        public static void Main(string[] args)
        {

            Tree<string> tree = new Tree<string>();

            tree.Root.AddChild("C");
            tree.Root.Nodes[0].AddChild("B");
            tree.Root.Nodes[0].Nodes[0].AddChild("Z");
            tree.Root.Nodes[0].Nodes[0].AddChild("A");
            tree.Root.Nodes[0].Nodes[0].AddChild("F");

            string myVal = tree.Find(x => x == "Z");

            Console.WriteLine("MyVal " + myVal);


            foreach (var item in tree.GetAll())
            {
                Console.WriteLine(item);
            }


            Tree<Student> studentsTree = new Tree<Student>();
            studentsTree.Root.AddChild(new Student { FName = "Joe", LName = "SMith" });
            studentsTree.Root.Nodes[0].AddChild(new Student { FName = "Jane", LName = "Doe" });


            Student s = studentsTree.Find(x => x != null && x.FName == "Joe");
            Console.WriteLine("s " + s.LName);
        }
    }

    class Student : IComparable
    {
        public string FName { get; set; }
        public string LName { get; set; }


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Student other = (Student)obj;
            if (other != null)
                return this.FName.CompareTo(other.FName);
            else
                throw new ArgumentException($"Object is not a {nameof(Student)}");
        }

    }


    //TODO: implement sort
    //TODO: implement find
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


        private void GetAllHelper(TreeNode<T> root, List<T> list)
        {
            if (root != null)
            {
                list.Add(root.Data);

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
