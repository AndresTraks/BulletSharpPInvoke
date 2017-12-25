using BulletSharp;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DemoFramework.DebugInfo
{
    public partial class DebugInfoForm : Form
    {
        private readonly Demo _demo;
        private DebugDrawModes[] _debugDrawModesList = new[] {
            DebugDrawModes.DrawWireframe,
            DebugDrawModes.DrawAabb,
            DebugDrawModes.DrawContactPoints,
            DebugDrawModes.DrawConstraints,
            DebugDrawModes.DrawConstraintLimits,
            DebugDrawModes.DrawFastWireframe,
            DebugDrawModes.DrawNormals,
            DebugDrawModes.DrawFrames
        };

        public DebugInfoForm(Demo demo)
        {
            _demo = demo;

            InitializeComponent();

            TakeSnapshot();
            SetDebugDrawFlags();

            debugDrawFlags.ItemCheck += DebugDrawFlags_ItemCheck;
        }

        private void SetDebugDrawFlags()
        {
            for (int i = 0; i < _debugDrawModesList.Length; i++)
            {
                SetDebugDrawFlag(i, _debugDrawModesList[i]);
            }
        }

        private void SetDebugDrawFlag(int index, DebugDrawModes drawMode)
        {
            bool isChecked = (_demo.DebugDrawMode & drawMode) != 0;
            debugDrawFlags.SetItemChecked(index, isChecked);
        }

        private void DebugDrawFlags_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            DebugDrawModes drawModes = _demo.DebugDrawMode;
            if (e.NewValue == CheckState.Checked)
            {
                drawModes |= _debugDrawModesList[e.Index];
            }
            else
            {
                drawModes &= (DebugDrawModes.All ^ _debugDrawModesList[e.Index]);
            }
            _demo.DebugDrawMode = drawModes;
        }

        private void TakeSnapshot()
        {
            SetWorldTreeInfo();
        }

        private void SetWorldTreeInfo()
        {
            var world = _demo.Simulation.World;

            TreeNode worldNode = GetOrCreateWorldNode(world);
            
            SetCollisionObjectsInfo(world.CollisionObjectArray, worldNode);
            SetBroadphaseInfo(world.Broadphase, worldNode);
        }

        private TreeNode GetOrCreateWorldNode(DiscreteDynamicsWorld world)
        {
            TreeNode worldNode;
            if (worldTree.Nodes.Count == 1)
            {
                worldNode = worldTree.Nodes[0];
                if (worldNode.Tag == world)
                {
                    return worldNode;
                }
                worldTree.Nodes.Clear();
            }
            worldNode = worldTree.Nodes.Add(world.GetType().Name);
            worldNode.Tag = world;
            worldNode.Expand();
            return worldNode;
        }

        private void SetCollisionObjectsInfo(IList<CollisionObject> collisionObjects, TreeNode worldNode)
        {
            TreeNode objectsNode = GetOrCreateChildNode("Objects", worldNode);

            foreach (CollisionObject collisionObject in collisionObjects)
            {
                SetCollisionObjectInfo(collisionObject, objectsNode);
            }

            RemoveMissingObjects(collisionObjects, objectsNode);
        }

        private void SetCollisionObjectInfo(CollisionObject collisionObject, TreeNode objectsNode)
        {
            string objectName = collisionObject.GetType().Name;
            CollisionShape shape = collisionObject.CollisionShape;
            string shapeName = shape.GetType().Name;

            string text = $"{objectName} ({shapeName})";
            GetOrCreateChildNode(collisionObject, text, objectsNode);
        }

        private void SetBroadphaseInfo(BroadphaseInterface broadphase, TreeNode worldNode)
        {
            string text = broadphase.GetType().Name;
            TreeNode broadphaseNode = GetOrCreateChildNode(broadphase, text, worldNode);

            var dbvtBroadphase = broadphase as DbvtBroadphase;
            if (dbvtBroadphase != null)
            {
                SetDbvtBroadphaseInfo(dbvtBroadphase, broadphaseNode);
            }
        }

        private void SetDbvtBroadphaseInfo(DbvtBroadphase dbvtBroadphase, TreeNode broadphaseNode)
        {
            var sets = dbvtBroadphase.Sets;
            Dbvt dynamicSet = sets[0];
            Dbvt staticSet = sets[1];

            TreeNode dynamicSetNode = GetOrCreateChildNode(dynamicSet, "Dynamic set", broadphaseNode);
            SetDbvtInfo(dynamicSet, dynamicSetNode);

            TreeNode staticSetNode = GetOrCreateChildNode(staticSet, "Static set", broadphaseNode);
            SetDbvtInfo(staticSet, staticSetNode);

            RemoveMissingObjects(sets, broadphaseNode);
        }

        private void SetDbvtInfo(Dbvt dbvtSet, TreeNode dbvtNode)
        {
            SetDbvtNodeInfo(dbvtSet.Root, dbvtNode);
        }

        private void SetDbvtNodeInfo(DbvtNode dbvtNode, TreeNode parentNode)
        {
            if (dbvtNode == null)
            {
                return;
            }

            string text = dbvtNode.GetType().Name;
            TreeNode dbvtNodeNode = GetOrCreateChildNode(dbvtNode, text, parentNode);

            RemoveObjectsOfType<DbvtProxy>(dbvtNodeNode);
            DbvtProxy proxy = dbvtNode.Data;
            GetOrCreateChildNode(proxy, "DbvtProxy" + " " + proxy.ClientObject, dbvtNodeNode);

            foreach (DbvtNode child in dbvtNode.Childs)
            {
                SetDbvtNodeInfo(child, dbvtNodeNode);
            }
            RemoveMissingObjects(dbvtNode.Childs, dbvtNodeNode);
        }

        private void snapshotButton_Click(object sender, System.EventArgs e)
        {
            TakeSnapshot();
        }

        private TreeNode GetOrCreateChildNode(object tag, string text, TreeNode parent)
        {
            TreeNode child = FindChildByTag(parent, tag);
            if (child == null)
            {
                child = parent.Nodes.Add(text);
                child.Tag = tag;
            }
            else
            {
                child.Text = text;
            }
            return child;
        }

        private TreeNode GetOrCreateChildNode(string text, TreeNode parent)
        {
            TreeNode child = FindChildByText(parent, text);
            if (child == null)
            {
                child = parent.Nodes.Add(text);
            }
            else
            {
                child.Text = text;
            }
            return child;
        }

        private TreeNode FindChildByText(TreeNode parent, string text)
        {
            foreach (TreeNode node in parent.Nodes)
            {
                if (node.Text == text)
                {
                    return node;
                }
            }
            return null;
        }

        private TreeNode FindChildByTag(TreeNode parent, object tag)
        {
            foreach (TreeNode node in parent.Nodes)
            {
                if (Equals(node.Tag, tag))
                {
                    return node;
                }
            }
            return null;
        }

        private void RemoveMissingObjects<T>(ICollection<T> collection, TreeNode parentNode) where T : class
        {
            IList<TreeNode> nodesToRemove = new List<TreeNode>();
            foreach (TreeNode objectNode in parentNode.Nodes)
            {
                var tag = objectNode.Tag;
                if (tag is T && !collection.Contains(tag as T))
                {
                    nodesToRemove.Add(objectNode);
                }
            }

            foreach (TreeNode node in nodesToRemove)
            {
                parentNode.Nodes.Remove(node);
            }
        }

        private void RemoveObjectsOfType<T>(TreeNode parentNode) where T : class
        {
            IList<TreeNode> nodesToRemove = new List<TreeNode>();
            foreach (TreeNode objectNode in parentNode.Nodes)
            {
                if (objectNode.Tag is T)
                {
                    nodesToRemove.Add(objectNode);
                }
            }

            foreach (TreeNode node in nodesToRemove)
            {
                parentNode.Nodes.Remove(node);
            }
        }
    }
}
