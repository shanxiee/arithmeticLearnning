using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        bstTree.PreOrderRecur(orderType.last);

        //bstTree.PreOrderRecur(orderType.last);

    }

    #region 面试题 04.02. 最小高度树
    /*解题思路，使用二分类似的方法，每次取分开的数组的中间一个
     * 就是中序遍历，树中序遍历是升序的，所以可以从中间开始构建一个
     * 最矮的树
     */

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }
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
    #endregion
    #region 108. 将有序数组转换为二叉搜索树
    /**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
    public TreeNode SortedArrayToBST2(int[] nums)
    {   
        return sortHelp(nums,0,nums.Length);
    }
    public TreeNode sortHelp(int[] nums,int lo,int hi)
    {
        if (lo > hi)
        {
            return null;
        }
        int mid = lo + (hi - lo) / 2;
        int nodeVal = nums[mid];
        TreeNode node = new TreeNode(nodeVal);
        node.left = sortHelp(nums, lo, mid-1);
        node.right = sortHelp(nums, mid + 1, hi);
        return node;
    }
    #endregion

    #region 102. 二叉树的层序遍历
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        var queueList = new Queue<TreeNode>();
        var levelOrder = new List<IList<int>>();

        if (root == null)
        {
            return levelOrder;
        }


        queueList.Enqueue(root);
        while (queueList.Count > 0)
        {
            int curLsitLen = queueList.Count;
            var tempList = new List<int>();
            for(int i = 0; i < curLsitLen; i++)
            {
                var outNode = queueList.Dequeue();
                tempList.Add(outNode.val);
                if (outNode.left!=null) 
                    queueList.Enqueue(outNode.left);
                if(outNode.right!=null)
                    queueList.Enqueue(outNode.right);

            }
            levelOrder.Add(tempList);
        }
        return levelOrder;
    }

    #endregion
    #region 105. 从前序与中序遍历序列构造二叉树
    //Hashtable indexMap;
    //public TreeNode buildTree(int[] preorder,int []inorder)
    //{
    //    int n = preorder.Length;
    //    indexMap = new Hashtable();
    //    for(int i = 0; i < n; i++)
    //    {
    //        indexMap.Add(inorder[i],i);
    //    }
    //    return myBuildTree(preorder, inorder, 0, n - 1, 0, n - 1);
    //}

    //private TreeNode myBuildTree(int[] preorder, int[] inorder, int preorder_left, int preorder_right, int inorder_left, int inorder_right)
    //{
    //    if (preorder_left > preorder_right)
    //        return null;
    //    int preorder_root = preorder_left;
    //    int inorder_root = (int)indexMap[preorder[preorder_root]];
    //    TreeNode root = new TreeNode(preorder[preorder_root]);

    //    int size_left_subtree = inorder_root - inorder_left;

    //    root.left = myBuildTree(preorder, inorder, preorder_left + 1, preorder_left + size_left_subtree, inorder_left, inorder_root - 1);
    //    root.right = myBuildTree(preorder, inorder, preorder_left + size_left_subtree + 1, preorder_right, inorder_root + 1, inorder_right);
    //    return root;
    //}

    #endregion
    #region 270. 最接近的二叉搜索树值
    public int ClosestValue(TreeNode root, double target)
    {

        double pred = double.MinValue;
        Stack<TreeNode> nodeStack = new Stack<TreeNode>();
        TreeNode cur = root;
        while (nodeStack.Count != 0 || cur != null)
        {
            while (cur != null)
            {
                nodeStack.Push(cur);
                cur = cur.left;
            }

            TreeNode node = nodeStack.Pop();

            if (target >= pred && target < node.val)
            {

                return Math.Abs(pred - target) < Math.Abs(node.val - target) ? (int)pred : node.val;

            }

            pred = node.val;
            cur = node.right;
        }
        return (int)pred;
    }
    #endregion
    #region 236. 二叉树的最近公共祖先


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
                LastOrderStack_1(root);
                LastOrderStack_2(root);
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

        private void LastOrderStack_1(Node x)
        {
            if (x == null) return;

            Stack<Node> nodeStack1 = new Stack<Node>();
            Stack<Node> nodeStack2 = new Stack<Node>();

            Node cur = x;
            nodeStack1.Push(x);
            while (nodeStack1.Count != 0)
            {
                cur = nodeStack1.Pop();
                if  (cur.left != null)
                {
                    nodeStack1.Push(cur.left);
                }
                if (cur.right != null)
                {
                    nodeStack1.Push(cur.right);
                }
                nodeStack2.Push(cur);
            }

            while (nodeStack2.Count != 0)
            {
                Node node = nodeStack2.Pop();
                Debug.Log("树的后序遍历 Stack版本1 ==>" + node.val);
            }

        }
        //1.h 的作用是 阻止C一直向下寻找递归，C判断H不相等之后则会一直走到最后一个循环，然后不断向上pop遍历
        //2.Peek 和 Push ,相当于插入后不断拿最新的那个.left一直向下递归
        private void LastOrderStack_2(Node x)
        {
            if (x == null) return;
            Stack<Node> nodeStack = new Stack<Node>();
            nodeStack.Push(x);
            Node c = x;
            Node h = null;

            while (nodeStack.Count != 0)
            {
                c = nodeStack.Peek();
                if (c.left != null && h != c.left && h != c.right)
                {
                    nodeStack.Push(c.left);
                }
                else if(c.right !=null && h != c.right){
                    nodeStack.Push(c.right);
                }
                else
                {
                    Node node = nodeStack.Pop();
                    Debug.Log("树的后序遍历 Stack版本2 ==>" + node.val);
                    h = c;
                }
            }
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
