using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum orderType
{
    per,
    mid,
    last
}
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
        //bstTree.PreOrderRecur(orderType.per);
        bstTree.PreOrderRecur(orderType.mid);
        //bstTree.PreOrderRecur(orderType.last);
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
        #region 树的遍历


        public void PreOrderRecur(orderType orderType)
        {
            if(orderType == orderType.per)
            {
                PreOrderRecur(root);
                PreOrderStack(root);
            }
            else if(orderType == orderType.mid)
            {
                MidOrderRecur(root);
                MidOrderStack(root);
            }
            else if(orderType == orderType.last)
            {
                LastOrderRecur(root);
                LastOrderStack(root);
            }
 
        }
        private void PreOrderRecur(Node x)
        {
            if (x == null) return;
            Debug.Log("前序遍历 " + x.val); ;
            PreOrderRecur(x.left);
            PreOrderRecur(x.right);
        }
        private void MidOrderRecur(Node x)
        {
            if (x == null) return;
            MidOrderRecur(x.left);
            Debug.Log("中序遍历 " + x.val);
            MidOrderRecur(x.right);
        }
        private void LastOrderRecur(Node x)
        {
            if (x == null) return;
            LastOrderRecur(x.left);
            LastOrderRecur(x.right);
            Debug.Log("后序遍历 " + x.val);
        }
        
        private void PreOrderStack(Node x)
        {
            Stack<Node> nodeStack = new Stack<Node>();
            nodeStack.Push(x);

            while (nodeStack.Count != 0)
            {
                Node curNode = nodeStack.Pop();
                Debug.Log("Stack 前序遍历 "+ curNode.val);
                if (curNode.right!=null)
                {
                    nodeStack.Push(curNode.right);
                }
                if (curNode.left != null)
                {
                    nodeStack.Push(curNode.left);
                }
            }
        }
        //树的非递归方式中序遍历
        //1.空节点  树通过判断当前节点的子节点是否是空节点，来对stack进行pop操作
        //2.cur当前节点 当前节点会根据stack的pop操作进行更新，（由于前面会递归，一直会把当前节点更
        //  新为没有左节点的子节点，此子节点没有左节点(离开while循环)，则会进行pop操作，然后把节点更新为右节点） 此时有2种情况，如果右节点为空则继续pop，更新cur节点（为上一层节点），
        // 否则cur会变成右节点，然后继续执行左节点更新递归
        private void MidOrderStack(Node x)
        {
            Stack<Node> nodeStack = new Stack<Node>();
            Node cur = x;
            while (nodeStack.Count != 0||cur !=null)
            {
                while (cur != null)
                {
                    nodeStack.Push(cur);
                    cur = cur.left;
                }

                Node node = nodeStack.Pop();
                Debug.Log("Stack 中序遍历 " + node.val);
                cur = node.right;
            }  
        }
        private void LastOrderStack(Node x)
        {

        }


        #endregion

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
            if (root == null) return default(Value);

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
