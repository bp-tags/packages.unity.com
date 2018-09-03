using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Experimental.U2D;

namespace UnityEditor.Experimental.U2D.Animation
{
    public interface IBoneModel
    {
        List<UniqueSpriteBone> GetRawData();
        void SetRawData(List<UniqueSpriteBone> spriteBones, Vector3 offset);
        
        IBone CreateNewRoot(Vector3 position);
        IBone CreateNewChildBone(IBone parentBone, Vector3 position);

        void MoveBone(IBone bone, Vector3 position, bool keepChildPosition = true);
        void MoveTip(IBone bone, Vector3 tipPosition, bool keepChildPosition = true);
        void Parent(IBone child, IBone parent);
        void DeleteBone(IBone bone);
        void SetBoneName(IBone bone, string newName);
        
        void RecordUndo(IBone bone, string operationName);

        IEnumerable<IBone> bones { get; }
    }

    internal class BoneUtility
    {
        public static bool IsOffspringOf(IBone bone, IBone potentialAncestor)
        {
            var isOffspring = false;
            do
            {
                bone = bone.parent;
                if (bone == potentialAncestor)
                {
                    isOffspring = true;
                    break;
                }
            }
            while (bone != null && !isOffspring);

            return isOffspring;
        }
    }

    internal class BoneList : ScriptableObject
    {
        [SerializeField]
        public List<Bone> bones = new List<Bone>();
    }

    internal class BoneModel : IBoneModel
    {
        private Action m_DataModifiedCallback;
        private BoneList m_BoneList;
        private Vector3 m_Offset;
        private bool m_OrderDirty;

        public BoneModel(Action dataModified)
        {
            m_DataModifiedCallback = dataModified;

            m_BoneList = ScriptableObject.CreateInstance<BoneList>();
            m_OrderDirty = false;

            Undo.undoRedoPerformed += UndoRedoPerformed;
        }

        ~BoneModel()
        {
            Undo.undoRedoPerformed -= UndoRedoPerformed;
        }

        private void UndoRedoPerformed()
        {
            FixParent();
        }

        public IEnumerable<IBone> bones
        {
            get
            {
                return m_BoneList.bones.AsEnumerable().Cast<IBone>();
            }
        }

        private void SetDirty()
        {
            m_DataModifiedCallback();
        }

        // TODO : We will live with this for now.
        private string RandomName()
        {
            return "_" + (char)UnityEngine.Random.Range(65, 90) + (char)UnityEngine.Random.Range(65, 90) + (char)UnityEngine.Random.Range(65, 90);
        }

        public void SetRawData(List<UniqueSpriteBone> rawBoneDatas, Vector3 offset)
        {
            m_Offset = offset;
            m_BoneList.bones = new List<Bone>();
            foreach (var sb in rawBoneDatas)
            {
                var isRoot = sb.parentId == -1;
                var Bone = new Bone(
                    sb.name,
                    isRoot ? null : m_BoneList.bones[sb.parentId],
                    // Root position was in mesh/rect space, convert to texture space.
                    isRoot ? sb.position + offset : sb.position,
                    sb.rotation,
                    sb.length,
                    m_BoneList.bones.Count,
                    sb.id.ToString()
                );

                Bone.RecalculateMatrix();

                m_BoneList.bones.Add(Bone);
            }
        }

        public List<UniqueSpriteBone> GetRawData()
        {
            if (m_OrderDirty)
                SortBones();

            var bones = new List<UniqueSpriteBone>();
            foreach (var bone in m_BoneList.bones)
            {
                var spriteBone = new UniqueSpriteBone();
                spriteBone.name = bone.name;
                spriteBone.parentId = bone.parentId;

                // Store position in mesh/rect space for root.
                spriteBone.position = bone.isRoot ? bone.localPosition - m_Offset : bone.localPosition;
                spriteBone.rotation = bone.localRotation;
                spriteBone.length = bone.length;
                spriteBone.id = new GUID(bone.uniqueId);
                bones.Add(spriteBone);
            }

            return bones;
        }

        private class BoneNameSort : IComparer<Bone>
        {
            public int Compare(Bone x, Bone y)
            {
                return x.name.CompareTo(y.name);
            }
        }

        public void SortBones()
        {
            if (m_BoneList.bones.Count > 2)
            {
                // Assuming the first bone always the root
                var frontSeat = 1;
                for (var sorted = 0; sorted < m_BoneList.bones.Count; ++sorted)
                {
                    var parent = m_BoneList.bones[sorted];
                    var startedFrontSeat = frontSeat;
                    for (var i = frontSeat; i < m_BoneList.bones.Count; ++i)
                    {
                        var bone = m_BoneList.bones[i];
                        if (bone.parent == parent)
                        {
                            if (i > frontSeat)
                            {
                                // Swap
                                var swap = m_BoneList.bones[frontSeat];
                                m_BoneList.bones[frontSeat] = bone;
                                m_BoneList.bones[i] = swap;

                                ++frontSeat;
                            }
                            else if (i == frontSeat)
                            {
                                ++frontSeat;
                            }
                        }
                    }

                    // Sort the direct children of current parent by name
                    m_BoneList.bones.Sort(startedFrontSeat, frontSeat - startedFrontSeat, new BoneNameSort());
                }
            }

            for (var i = 0; i < m_BoneList.bones.Count; ++i)
            {
                m_BoneList.bones[i].debugIndex = i;
                m_BoneList.bones[i].UpdateParentId();
            }
        }

        public void FixParent()
        {
            foreach (var bone in m_BoneList.bones)
            {
                if (bone.parentId != -1)
                    bone.parent = m_BoneList.bones[bone.parentId];
            }
            m_OrderDirty = true;
            UpdateHierarchy();
        }

        public IBone CreateNewChildBone(IBone parentBone, Vector3 position)
        {
            if (parentBone == null)
                throw new InvalidOperationException("Creating a bone with an invalid parent");

            var childBone = new Bone();
            childBone.parent = parentBone;
            childBone.position = position;
            childBone.debugIndex = m_BoneList.bones.Count;

            if (parentBone.isRoot)
                childBone.name = "bone" + RandomName();
            else
                childBone.name = parentBone.name + RandomName();

            m_BoneList.bones.Add(childBone);

            m_OrderDirty = true;
            SetDirty();
            
            return childBone;
        }

        public IBone CreateNewRoot(Vector3 position)
        {
            var root = new Bone();
            root.name = "root";
            root.position = position;
            root.debugIndex = m_BoneList.bones.Count;
            root.parentId = -1;
            
            if (m_BoneList.bones.Any())
                throw new InvalidOperationException("Creating a new root when there are bones in this sprite");

            m_BoneList.bones.Add(root);
            SetDirty();

            return root;
        }

        public void DeleteBone(IBone bone)
        {
            foreach (var otherBone in m_BoneList.bones)
            {
                if (BoneUtility.IsOffspringOf(otherBone, bone))
                    throw new InvalidOperationException("Cannot delete a parent bone with children. Children should all be removed/transfered first.");
            }

            m_BoneList.bones.Remove((Bone)bone);

            m_OrderDirty = true;
            SetDirty();
            MarkChildRetain(bone);
            UpdateHierarchyFromBone(bone, true);
        }

        public void Parent(IBone newChild, IBone parent)
        {
            if (newChild == parent)
                throw new InvalidOperationException("Cannot parent a bone to itself.");
            if (BoneUtility.IsOffspringOf(parent, newChild))
                throw new InvalidOperationException(string.Format("Cannot parent {0} to {1}. This will create a loop.", newChild.name, parent.name));

            MarkChildRetain(newChild);

            ((Bone)newChild).parent = parent;

            m_OrderDirty = true;
            SetDirty();
            UpdateHierarchyFromBone(newChild, true);
        }

        public void MoveBone(IBone bone, Vector3 position, bool keepChildPosition)
        {
            ((Bone)bone).position = position;
            SetDirty();
            if (keepChildPosition) 
                MarkChildRetain(bone);
            UpdateHierarchyFromBone(bone, keepChildPosition);
        }

        public void MoveTip(IBone bone, Vector3 tipPosition, bool keepChildPosition)
        {
            ((Bone)bone).tip = tipPosition;
            SetDirty();
            if (keepChildPosition) 
                MarkChildRetain(bone);
            UpdateHierarchyFromBone(bone, keepChildPosition);
        }

        public void SetBoneName(IBone bone, string newName)
        {
            ((Bone)bone).name = newName;
            SetDirty();
        }

        public void RecordUndo(IBone bone, string operationName)
        {
            Undo.RegisterCompleteObjectUndo(m_BoneList, operationName);
        }

        private void UpdateHierarchy()
        {
            if (m_BoneList.bones.Any())
                UpdateHierarchyFromBone(m_BoneList.bones.First(), false);
        }

        private void UpdateHierarchyFromBone(IBone fromBone, bool keepChildPosition)
        {
            if (m_OrderDirty)
            {
                m_OrderDirty = false;
                SortBones();
            }
            
            foreach (var bone in m_BoneList.bones)
            {
                if (BoneUtility.IsOffspringOf(bone, fromBone) || bone == fromBone)
                {
                    if (bone.markedRetainWorldPosition)
                        bone.RestoreWorldPosition();
                    bone.RecalculateMatrix();
                }
            }
        }

        private void MarkChildRetain(IBone parent)
        {
            foreach (var bone in m_BoneList.bones)
            {
                if (BoneUtility.IsOffspringOf(bone, parent) || bone == parent)
                {
                    bone.markedRetainWorldPosition = true;
                }
            }
        }
    }
}
