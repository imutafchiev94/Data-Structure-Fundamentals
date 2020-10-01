using System.Linq;

namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            foreach (var line in input)
            {
                int[] keys = line.Split(' ').Select(int.Parse).ToArray();

                int parentKey = keys[0];
                int chieldKey = keys[1];

                this.AddEdge(parentKey, chieldKey);
            }

            return this.GetRoot();
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            if (!this.nodesBykeys.ContainsKey(key))
            {
                this.nodesBykeys.Add(key, new Tree<int>(key));
            }
            return this.nodesBykeys[key];
        }

        public void AddEdge(int parent, int child)
        {
            var parentNode = this.CreateNodeByKey(parent);
            var chieldNode = this.CreateNodeByKey(child);

            parentNode.AddChild(chieldNode);
            chieldNode.AddParent(parentNode);
        }

        private Tree<int> GetRoot()
        {
            foreach (var keyByNode in this.nodesBykeys)
            {
                if (keyByNode.Value.Parent == null)
                {
                    return keyByNode.Value;
                }
            }

            return null;
        }
    }
}
