using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leecodeBstTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BST<int, String> bstTree = new BST<int, string>();
        bstTree.put(6, "这是6");
        bstTree.put(3, "这是3");
        bstTree.put(5, "这是5");
        bstTree.put(8, "这是8");
        bstTree.put(55, "这是55");
        bstTree.put(44, "这是44");
        bstTree.put(8, "这是8,又变成了88");
        //Debug.Log(bstTree.get(8));
        bstTree.PreOrderRecur();
    }

    #region 面试题 04.02. 最小高度树
    /*解题思路，使用二分类似的方法，每次取分开的数组的中间一个
     * 就是中序遍历，树中序遍历是升序的，所以可以从中间开始构建一个
     * 最矮的树
     */
    public TreeNode SortedArrayToBST(int[] nums)
    {
        if (nums.Length == 0) return null;

        return put(0, nums.Length - 1, nums);
    }
    TreeNode put(int hi, int lo, int[] nums)
    {
        if (hi > lo) return null;
        int mid = (hi + lo) / 2;
        var node = new TreeNode(nums[mid]);
        node.left = put(hi, mid - 1, nums);
        node.right = put(mid + 1, lo, nums);
        return node;
    }
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }
    #endregion
    #region BST Tree
    public class BST<Key,Value> where Key : IComparable
    {
        internal Node root;
        internal class Node
        {
            internal Key key;
            internal Value val;
            internal Node left, right;
            internal int N;

            public Node(Key key,Value val,int N)
            {
                this.key = key;
                this.val = val;
                this.N = N;
            }
        }

        public void PreOrderRecur()
        {
            PreOrderRecur(root);
        }
        private void PreOrderRecur(Node x)
        {
          
            if (x == null) return;
            Debug.Log("前序遍历 " + x.val); ;
            PreOrderRecur(x.left);
            PreOrderRecur(x.right);
        }

        public int Size()
        {
            return size(root);
        }
        private int size(Node x )
        {
            if (x == null) return 0;
            else
                return x.N;
        }

        public Value get(Key key)
        {
            return get(root,key);
        }
        private Value get(Node root,Key key)
        {
            if (root == null) return default;

            int cmp = root.key.CompareTo(key);
            if (cmp > 0)
            {
                return get(root.left, key);
            }
            else if(cmp < 0)
            {
                return get(root.right, key);
            }
            else
            {
                return root.val;
            }
        }

        public void put(Key key, Value val)
        {
            root = put(root, key, val);
        }

        private Node put(Node root, Key key, Value val)
        {
            if (root == null) 
                return new Node(key, val,0);

            int cmp = key.CompareTo(root.key);
            if (cmp < 0)
            {
                root.left = put(root.left, key, val);
            }
            else if (cmp >0)
            {
                root.right = put(root.right, key, val);
            }
            else
            {
                root.val = val;
            }
            return root;
        }
    }




    #endregion
}
