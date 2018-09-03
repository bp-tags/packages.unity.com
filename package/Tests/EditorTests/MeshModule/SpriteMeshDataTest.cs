using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NSubstitute;
using UnityEditor;
using UnityEngine;
using UnityEditor.U2D;
using UnityEditor.U2D.Interface;
using UnityEngine.U2D.Interface;
using UnityEngine.Experimental.U2D;

namespace UnityEditor.Experimental.U2D.Animation.Test.MeshModule.SpriteMeshDataTest
{
    [TestFixture]
    public class SpriteMeshDataTest
    {
        private SpriteMeshData m_SpriteMeshData;
        private List<int> m_SelectedVertices;
        private ISelection m_Selection;

        [SetUp]
        public void Setup()
        {
            m_SelectedVertices = new List<int>();
            m_Selection = Substitute.For<ISelection>();
            m_Selection.Count.Returns(x => m_SelectedVertices.Count);
            m_Selection.IsSelected(Arg.Any<int>()).Returns(x => m_SelectedVertices.Contains((int)x[0]));
            m_Selection.GetEnumerator().Returns(x => m_SelectedVertices.GetEnumerator());
            m_Selection.single.Returns(x =>
                {
                    if (m_SelectedVertices.Count == 1)
                        return m_SelectedVertices[0];
                    return -1;
                });

            m_SpriteMeshData = new SpriteMeshData();
        }

        private void CreateTwoVerticesAndEdge(Vector2 v1, Vector2 v2)
        {
            m_SpriteMeshData.CreateVertex(v1);
            m_SpriteMeshData.CreateVertex(v2);
            m_SpriteMeshData.CreateEdge(m_SpriteMeshData.vertices.Count - 2, m_SpriteMeshData.vertices.Count - 1);
        }

        private void SplitEdge(int edgeIndex)
        {
            m_SpriteMeshData.CreateVertex(Vector2.zero, edgeIndex);
        }

        private void CreateTenVertices()
        {
            for (int i = 0; i < 10; ++i)
                m_SpriteMeshData.CreateVertex(Vector2.one * i);
        }

        [Test]
        public void CreateVertex_NewVertexAddedToVertexList_IncrementsVertexCount()
        {
            m_SpriteMeshData.CreateVertex(Vector2.zero);
            Assert.AreEqual(1, m_SpriteMeshData.vertices.Count, "Should contain 1 vertex.");
            Assert.AreEqual(Vector2.zero, m_SpriteMeshData.vertices[0].position, "Should contain a Vector2.zero vertex.");
        }

        [Test]
        public void CreateVertexAndSplitEdge_RemoveFirstEdge_CreatesTwoEdgesConnectingTheThreeVertices()
        {
            CreateTwoVerticesAndEdge(Vector2.zero, Vector2.one);
            SplitEdge(0);
            Assert.AreEqual(3, m_SpriteMeshData.vertices.Count, "Vertex count after CreateVertex in edge should return 3");
            Assert.AreEqual(2, m_SpriteMeshData.edges.Count, "Edge count after CreateVertex in edge should return 2");
            Assert.IsFalse(m_SpriteMeshData.edges.Contains(new Edge(0, 1)), "MeshStorage should not contain an edge from the first to the second");
            Assert.IsTrue(m_SpriteMeshData.edges.Contains(new Edge(0, 2)), "MeshStorage should contain an edge from the first vertex to the last");
            Assert.IsTrue(m_SpriteMeshData.edges.Contains(new Edge(1, 2)), "MeshStorage should contain an edge from the second vertex to the last");
        }

        [Test]
        public void RemoveVertex_RemovesVertexFromVertexList_DecrementsVertexCount()
        {
            m_SpriteMeshData.CreateVertex(Vector2.zero);
            m_SpriteMeshData.RemoveVertex(0);
            Assert.AreEqual(0, m_SpriteMeshData.vertices.Count, "Should contain no vertices.");
        }

        [Test]
        public void RemoveCollectionOfVertices()
        {
            int[] vertIndices = new int[] {3, 8, 9, 5};

            CreateTenVertices();
            m_SpriteMeshData.CreateEdge(0, 1);
            m_SpriteMeshData.CreateEdge(1, 3);
            m_SpriteMeshData.CreateEdge(3, 4);
            m_SpriteMeshData.CreateEdge(2, 4);
            m_SpriteMeshData.CreateEdge(6, 7);
            m_SpriteMeshData.CreateEdge(8, 9);

            m_SpriteMeshData.RemoveVertex(vertIndices);

            Assert.True(m_SpriteMeshData.vertices.Count == 6);
            Assert.True(m_SpriteMeshData.vertices[0].position == Vector2.zero);
            Assert.True(m_SpriteMeshData.vertices[1].position == Vector2.one);
            Assert.True(m_SpriteMeshData.vertices[2].position == Vector2.one * 2f);
            Assert.True(m_SpriteMeshData.vertices[3].position == Vector2.one * 4f);
            Assert.True(m_SpriteMeshData.vertices[4].position == Vector2.one * 6f);
            Assert.True(m_SpriteMeshData.vertices[5].position == Vector2.one * 7f);

            Assert.True(m_SpriteMeshData.edges.Count == 4);
            Assert.True(m_SpriteMeshData.edges.Contains(new Edge(0, 1)));
            Assert.True(m_SpriteMeshData.edges.Contains(new Edge(1, 3)));
            Assert.True(m_SpriteMeshData.edges.Contains(new Edge(2, 3)));
            Assert.True(m_SpriteMeshData.edges.Contains(new Edge(4, 5)));
        }

        [Test]
        public void RemoveEmptyCollectionOfVertices()
        {
            int[] vertIndices = new int[0];

            CreateTenVertices();

            m_SpriteMeshData.RemoveVertex(vertIndices);

            Assert.True(m_SpriteMeshData.vertices.Count == 10);
        }

        [Test]
        public void RemoveVertex_EdgesHaveHigherIndices_DecrementEdgeIndicesHigherThanVertexIndex()
        {
            m_SpriteMeshData.CreateVertex(Vector2.zero);
            CreateTwoVerticesAndEdge(Vector2.zero, Vector2.one);
            m_SpriteMeshData.RemoveVertex(0);
            Assert.AreEqual(2, m_SpriteMeshData.vertices.Count, "Should contain 2 vertices after removal.");
            Assert.AreEqual(1, m_SpriteMeshData.edges.Count, "Edge should not be removed.");
            Assert.AreEqual(new Edge(0, 1), m_SpriteMeshData.edges[0], "Edge indices should have decremented.");
        }

        [Test]
        public void RemoveVertex_WhereEdgeContainsVertexIndex_RemovesTheEdge()
        {
            CreateTwoVerticesAndEdge(Vector2.zero, Vector2.one);
            m_SpriteMeshData.RemoveVertex(0);
            Assert.AreEqual(1, m_SpriteMeshData.vertices.Count, "Vertex count after RemoveVertex in edge should return 1.");
            Assert.AreEqual(0, m_SpriteMeshData.edges.Count, "Edge count after RemoveVertex should return 0.");
        }

        [Test]
        public void RemoveVertex_WhereTwoEdgesShareVertexIndex_CreatesEdgeConnectingEndpoints()
        {
            CreateTwoVerticesAndEdge(Vector2.zero, Vector2.one);
            SplitEdge(0);
            m_SpriteMeshData.RemoveVertex(2);
            Assert.AreEqual(2, m_SpriteMeshData.vertices.Count, "GetVertexCount after RemoveVertex in edge should return 2");
            Assert.AreEqual(1, m_SpriteMeshData.edges.Count, "GetEdgeCount after RemoveVertex should return 1.");
            Assert.AreEqual(new Edge(0, 1), m_SpriteMeshData.edges[0], "The remaining edge should connect the remaining 2 vertices.");
        }

        [Test]
        public void RemoveVertex_WhereMoreThanTwoEdgesShareVertexIndex_RemoveEdgesContainingVertexIndex()
        {
            CreateTwoVerticesAndEdge(Vector2.zero, Vector2.one);
            SplitEdge(0);
            m_SpriteMeshData.CreateVertex(Vector2.right);
            m_SpriteMeshData.CreateEdge(3, 2);
            Assert.AreEqual(4, m_SpriteMeshData.vertices.Count, "Vertex count should return 4.");
            Assert.AreEqual(3, m_SpriteMeshData.edges.Count, "Edge count should return 3.");
            Assert.IsTrue(m_SpriteMeshData.edges.Contains(new Edge(2, 3)), "Edges should contain an edge connecting the last 2 vertices.");
            m_SpriteMeshData.RemoveVertex(2);
            Assert.AreEqual(3, m_SpriteMeshData.vertices.Count, "Vertex count after RemoveVertex in edge should return 3");
            Assert.AreEqual(0, m_SpriteMeshData.edges.Count, "Edge count after RemoveVertex should return 0.");
        }

        [Test]
        public void CreateEdge_AddsEdgeToMeshStorage_IncrementsEdgeCount()
        {
            CreateTwoVerticesAndEdge(Vector2.zero, Vector2.one);
            Assert.AreEqual(1, m_SpriteMeshData.edges.Count, "Edge count should increment.");
            Assert.IsTrue(m_SpriteMeshData.edges.Contains(new Edge(0, 1)), "A Edge(0,1) should be created");
        }

        [Test]
        public void CreateEdge_CannotAddDuplicateEdge()
        {
            CreateTwoVerticesAndEdge(Vector2.zero, Vector2.one);
            m_SpriteMeshData.CreateEdge(0, 1);
            Assert.AreEqual(1, m_SpriteMeshData.edges.Count, "No duplicate edges should be allowed.");
        }

        [Test]
        public void CalculateWeights_FiltersSmallWeights()
        {
            float tolerance = 0.1f;

            IWeightsGenerator generator = Substitute.For<IWeightsGenerator>();

            m_SpriteMeshData.CreateVertex(Vector2.zero);

            BoneWeight[] weigts = new BoneWeight[]
            {
                new BoneWeight()
                {
                    boneIndex0 = 0,
                    boneIndex1 = 1,
                    weight0 = 0.05f,
                    weight1 = 0.95f
                }
            };

            generator.Calculate(Arg.Any<Vector2[]>(), Arg.Any<Edge[]>(), Arg.Any<Vector2[]>(), Arg.Any<Edge[]>()).Returns(weigts);

            m_SpriteMeshData.CalculateWeights(generator, null, tolerance);

            BoneWeight result = m_SpriteMeshData.vertices[0].editableBoneWeight.ToBoneWeight(false);

            Assert.AreEqual(0, result.boneIndex0, "Incorrect bone index");
            Assert.AreEqual(1, result.boneIndex1, "Incorrect bone index");
            Assert.AreEqual(0f, result.weight0, "Incorrect bone weight");
            Assert.AreEqual(1f, result.weight1, "Incorrect bone weight");
        }

        [Test]
        public void CalculateWeightsSafe_SetWeightsOnlyToVerticesWithoutInfluences()
        {
            IWeightsGenerator generator = Substitute.For<IWeightsGenerator>();

            m_SpriteMeshData.CreateVertex(Vector2.zero);
            m_SpriteMeshData.CreateVertex(Vector2.one);

            m_SpriteMeshData.vertices[0].editableBoneWeight = EditableBoneWeightUtility.CreateFromBoneWeight(new BoneWeight());
            m_SpriteMeshData.vertices[1].editableBoneWeight = EditableBoneWeightUtility.CreateFromBoneWeight(new BoneWeight() { weight0 = 0.5f });

            BoneWeight[] weigts = new BoneWeight[]
            {
                new BoneWeight() { weight0 = 1f },
                new BoneWeight() { weight0 = 1f }
            };

            generator.Calculate(Arg.Any<Vector2[]>(), Arg.Any<Edge[]>(), Arg.Any<Vector2[]>(), Arg.Any<Edge[]>()).Returns(weigts);

            m_SpriteMeshData.CalculateWeightsSafe(generator, null, 0f);

            BoneWeight result1 = m_SpriteMeshData.vertices[0].editableBoneWeight.ToBoneWeight(false);
            BoneWeight result2 = m_SpriteMeshData.vertices[1].editableBoneWeight.ToBoneWeight(false);

            Assert.AreEqual(1f, result1.weight0, "Incorrect bone weight");
            Assert.AreEqual(0.5f, result2.weight0, "Incorrect bone weight");
        }

        [Test]
        public void NormalizeWeights()
        {
            m_SpriteMeshData.CreateVertex(Vector2.zero);

            m_SpriteMeshData.vertices[0].editableBoneWeight = EditableBoneWeightUtility.CreateFromBoneWeight(new BoneWeight() { weight0 = 0.1f });

            m_SpriteMeshData.NormalizeWeights(null);

            BoneWeight result = m_SpriteMeshData.vertices[0].editableBoneWeight.ToBoneWeight(false);

            Assert.AreEqual(1f, result.weight0, "Incorrect bone weight");
        }

        [Test]
        public void NormalizeWeights_WithSelection_NormalizeSelectedVertices()
        {
            m_SelectedVertices.Add(1);

            m_SpriteMeshData.CreateVertex(Vector2.zero);
            m_SpriteMeshData.CreateVertex(Vector2.zero);

            m_SpriteMeshData.vertices[0].editableBoneWeight = EditableBoneWeightUtility.CreateFromBoneWeight(new BoneWeight() { weight0 = 0.1f });
            m_SpriteMeshData.vertices[1].editableBoneWeight = EditableBoneWeightUtility.CreateFromBoneWeight(new BoneWeight() { weight0 = 0.1f });

            m_SpriteMeshData.NormalizeWeights(m_Selection);

            BoneWeight result0 = m_SpriteMeshData.vertices[0].editableBoneWeight.ToBoneWeight(false);
            BoneWeight result1 = m_SpriteMeshData.vertices[1].editableBoneWeight.ToBoneWeight(false);

            Assert.AreEqual(0.1f, result0.weight0, "Incorrect bone weight");
            Assert.AreEqual(1f, result1.weight0, "Incorrect bone weight");
        }

        [Test]
        public void GetMultiEditChannelData_WithNullSelection_TrowsException()
        {
            BoneWeightData data;
            bool channelEnabled, mixedChannelEnabled, mixedBoneIndex, mixedWeight;

           Assert.Throws<ArgumentNullException>(() => m_SpriteMeshData.GetMultiEditChannelData(null, 0, out channelEnabled, out data, out mixedChannelEnabled, out mixedBoneIndex, out mixedWeight));
        }

        [Test]
        public void GetMultiEditChannelData_WithNoMixedData_ReturnsFalseToMixedValues_ReturnsCommonBoneWeightData_ReturnsCommonChannelEnabledState()
        {
            m_SelectedVertices.Add(0);
            m_SelectedVertices.Add(1);

            m_SpriteMeshData.CreateVertex(Vector2.zero);
            m_SpriteMeshData.CreateVertex(Vector2.zero);

            m_SpriteMeshData.vertices[0].editableBoneWeight.SetFromBoneWeight(new BoneWeight() { boneIndex0 = 0, boneIndex1 = 1, boneIndex2 = 2, boneIndex3 =  3, weight0 = 0.1f, weight1 = 0.2f, weight2 = 0.3f, weight3 = 0.4f });
            m_SpriteMeshData.vertices[1].editableBoneWeight.SetFromBoneWeight(new BoneWeight() { boneIndex0 = 0, boneIndex1 = 1, boneIndex2 = 2, boneIndex3 =  3, weight0 = 0.1f, weight1 = 0.2f, weight2 = 0.3f, weight3 = 0.4f });

            BoneWeightData data;
            bool channelEnabled, mixedChannelEnabled, mixedBoneIndex, mixedWeight;

           m_SpriteMeshData.GetMultiEditChannelData(m_Selection, 0, out channelEnabled, out data, out mixedChannelEnabled, out mixedBoneIndex, out mixedWeight);

           Assert.True(channelEnabled, "Incorrect channel enabled state");
           Assert.False(mixedChannelEnabled, "Incorrect mixed value state");
           Assert.False(mixedBoneIndex, "Incorrect mixed value state");
           Assert.False(mixedWeight, "Incorrect mixed value state");
           Assert.AreEqual(0, data.boneIndex, "Incorrect mixed boneIndex");
           Assert.AreEqual(0.1f, data.weight, "Incorrect mixed boneWeight");
        }

        [Test]
        public void GetMultiEditChannelData_WithMixedChannelEnabled_ReturnsTrueToMixedChannelEnabled_ReturnsFalseChannelEnabledState()
        {
            m_SelectedVertices.Add(0);
            m_SelectedVertices.Add(1);

            m_SpriteMeshData.CreateVertex(Vector2.zero);
            m_SpriteMeshData.CreateVertex(Vector2.zero);

            m_SpriteMeshData.vertices[0].editableBoneWeight.SetFromBoneWeight(new BoneWeight() { boneIndex0 = 0, boneIndex1 = 1, boneIndex2 = 2, boneIndex3 =  3, weight0 = 0.1f, weight1 = 0.2f, weight2 = 0.3f, weight3 = 0.4f });
            m_SpriteMeshData.vertices[1].editableBoneWeight.SetFromBoneWeight(new BoneWeight() { boneIndex0 = 0, boneIndex1 = 1, boneIndex2 = 2, boneIndex3 =  3, weight0 = 0.1f, weight1 = 0.2f, weight2 = 0.3f, weight3 = 0.4f });
            m_SpriteMeshData.vertices[0].editableBoneWeight.EnableChannel(0, false);

            BoneWeightData data;
            bool channelEnabled, mixedChannelEnabled, mixedBoneIndex, mixedWeight;

           m_SpriteMeshData.GetMultiEditChannelData(m_Selection, 0, out channelEnabled, out data, out mixedChannelEnabled, out mixedBoneIndex, out mixedWeight);

           Assert.False(channelEnabled, "Incorrect channel enabled state");
           Assert.True(mixedChannelEnabled, "Incorrect mixed value state");
        }

        [Test]
        public void GetMultiEditChannelData_WithMixedWeight_ReturnstrueToMixedWeight_ReturnsZeroWeight()
        {
            m_SelectedVertices.Add(0);
            m_SelectedVertices.Add(1);

            m_SpriteMeshData.CreateVertex(Vector2.zero);
            m_SpriteMeshData.CreateVertex(Vector2.zero);

            m_SpriteMeshData.vertices[0].editableBoneWeight.SetFromBoneWeight(new BoneWeight() { boneIndex0 = 0, boneIndex1 = 1, boneIndex2 = 2, boneIndex3 =  3, weight0 = 0.1f, weight1 = 0.2f, weight2 = 0.3f, weight3 = 0.4f });
            m_SpriteMeshData.vertices[1].editableBoneWeight.SetFromBoneWeight(new BoneWeight() { boneIndex0 = 0, boneIndex1 = 1, boneIndex2 = 2, boneIndex3 =  3, weight0 = 0f, weight1 = 0.2f, weight2 = 0.3f, weight3 = 0.4f });

            BoneWeightData data;
            bool channelEnabled, mixedChannelEnabled, mixedBoneIndex, mixedWeight;

           m_SpriteMeshData.GetMultiEditChannelData(m_Selection, 0, out channelEnabled, out data, out mixedChannelEnabled, out mixedBoneIndex, out mixedWeight);

           Assert.True(mixedWeight, "Incorrect mixed value state");
           Assert.AreEqual(0f, data.weight, "Incorrect mixed boneWeight");
        }

        [Test]
        public void GetMultiEditChannelData_WithMixedBoneIndex_ReturnsTrueToMixedBoneIndex_ReturnsInvalidBoneIndex()
        {
            m_SelectedVertices.Add(0);
            m_SelectedVertices.Add(1);

            m_SpriteMeshData.CreateVertex(Vector2.zero);
            m_SpriteMeshData.CreateVertex(Vector2.zero);

            m_SpriteMeshData.vertices[0].editableBoneWeight.SetFromBoneWeight(new BoneWeight() { boneIndex0 = 0, boneIndex1 = 1, boneIndex2 = 2, boneIndex3 =  3, weight0 = 0.1f, weight1 = 0.2f, weight2 = 0.3f, weight3 = 0.4f });
            m_SpriteMeshData.vertices[1].editableBoneWeight.SetFromBoneWeight(new BoneWeight() { boneIndex0 = 1, boneIndex1 = 1, boneIndex2 = 2, boneIndex3 =  3, weight0 = 0.1f, weight1 = 0.2f, weight2 = 0.3f, weight3 = 0.4f });

            BoneWeightData data;
            bool channelEnabled, mixedChannelEnabled, mixedBoneIndex, mixedWeight;

           m_SpriteMeshData.GetMultiEditChannelData(m_Selection, 0, out channelEnabled, out data, out mixedChannelEnabled, out mixedBoneIndex, out mixedWeight);

           Assert.True(mixedBoneIndex, "Incorrect mixed value state");
           Assert.AreEqual(-1, data.boneIndex, "Incorrect mixed boneIndex");
        }

        [Test]
        public void SetMultiEditChannelData_WithNullSelection_TrowsException()
        {
           Assert.Throws<ArgumentNullException>(() => m_SpriteMeshData.SetMultiEditChannelData(null, 0, false, false, new BoneWeightData(), new BoneWeightData()));
        }

        [Test]
        public void SetMultiEditChannelData_SetsValuesToSelectedVertices_CompensatesOtherChannels()
        {
            m_SelectedVertices.Add(0);

            m_SpriteMeshData.CreateVertex(Vector2.zero);

            m_SpriteMeshData.vertices[0].editableBoneWeight.SetFromBoneWeight(new BoneWeight() { boneIndex0 = 0, weight0 = 0f, weight1 = 1f });

           m_SpriteMeshData.SetMultiEditChannelData(m_Selection, 0, false, true, new BoneWeightData(0,0f), new BoneWeightData(1,1f));

           Assert.True(m_SpriteMeshData.vertices[0].editableBoneWeight.IsChannelEnabled(0), "Incorrect channel enabled state");
           Assert.AreEqual(1, m_SpriteMeshData.vertices[0].editableBoneWeight.GetBoneWeightData(0).boneIndex, "Incorrect bone index");
           Assert.AreEqual(1f, m_SpriteMeshData.vertices[0].editableBoneWeight.GetBoneWeightData(0).weight, "Incorrect weight");
           Assert.AreEqual(0f, m_SpriteMeshData.vertices[0].editableBoneWeight.GetBoneWeightData(1).weight, "Incorrect weight");
        }

        private static IEnumerable<TestCaseData> BoneMetadataCases()
        {
            var originalMetadata = new SpriteBone[5]
            {
                new SpriteBone()
                {
                    name = "root",
                    parentId = -1,
                    position = Vector2.one,
                    rotation = Quaternion.Euler(0.0f, 0.0f, 30.0f),
                    length = 1.0f
                },
                new SpriteBone()
                {
                    name = "child1",
                    parentId = 0,
                    position = Vector3.up,
                    rotation = Quaternion.Euler(0.0f, 0.0f, 60.0f),
                    length = 1.5f
                },
                new SpriteBone()
                {
                    name = "child2",
                    parentId = 1,
                    position = Vector3.right,
                    rotation = Quaternion.identity,
                    length = 1.5f
                },
                new SpriteBone()
                {
                    name = "child3",
                    parentId = 1,
                    position = Vector3.left,
                    rotation = Quaternion.Euler(0.0f, 0.0f, 120.0f),
                    length = 2.5f
                },
                new SpriteBone()
                {
                    name = "child4",
                    parentId = 3,
                    position = Vector3.up,
                    rotation = Quaternion.identity,
                    length = 1.0f
                }
            };

            Vector2 kOffset = Vector2.one;
            var expected = new SpriteBone[5]
            {
                new SpriteBone()
                {
                    name = "root",
                    parentId = -1,
                    position = Vector2.one + kOffset,
                    rotation = Quaternion.Euler(0.0f, 0.0f, 30.0f),
                    length = 1.0f
                },
                new SpriteBone()
                {
                    name = "child1",
                    parentId = 0,
                    position = new Vector2(1.5f, 2.866025f),
                    rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f),
                    length = 1.5f
                },
                new SpriteBone()
                {
                    name = "child2",
                    parentId = 1,
                    position = new Vector2(1.5f, 3.866025f),
                    rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f),
                    length = 1.5f
                },
                new SpriteBone()
                {
                    name = "child3",
                    parentId = 1,
                    position = new Vector2(1.5f, 1.866025f),
                    rotation = Quaternion.Euler(0.0f, 0.0f, 210.0f),
                    length = 2.5f
                },
                new SpriteBone()
                {
                    name = "child4",
                    parentId = 3,
                    position = new Vector2(2.0f, 1.0f),
                    rotation = Quaternion.Euler(0.0f, 0.0f, 210.0f),
                    length = 1.0f
                }
            };

            var testcase = new TestCaseData(originalMetadata.ToList(), expected.ToList(), kOffset);
            testcase.SetName("Normal Hierarchical order");
            yield return testcase;

            var reservedOriginal = new List<SpriteBone>();
            var originalBone = originalMetadata[4];
            originalBone.parentId = 1;
            reservedOriginal.Add(originalBone);

            originalBone = originalMetadata[3];
            originalBone.parentId = 3;
            reservedOriginal.Add(originalBone);

            originalBone = originalMetadata[2];
            originalBone.parentId = 3;
            reservedOriginal.Add(originalBone);

            originalBone = originalMetadata[1];
            originalBone.parentId = 4;
            reservedOriginal.Add(originalBone);

            originalBone = originalMetadata[0];
            reservedOriginal.Add(originalBone);

            var reversedExpected = new List<SpriteBone>();
            var expectedBone = expected[4];
            expectedBone.parentId = 1;
            reversedExpected.Add(expectedBone);

            expectedBone = expected[3];
            expectedBone.parentId = 3;
            reversedExpected.Add(expectedBone);

            expectedBone = expected[2];
            expectedBone.parentId = 3;
            reversedExpected.Add(expectedBone);

            expectedBone = expected[1];
            expectedBone.parentId = 4;
            reversedExpected.Add(expectedBone);

            expectedBone = expected[0];
            reversedExpected.Add(expectedBone);
            
            testcase = new TestCaseData(reservedOriginal, reversedExpected, kOffset);
            testcase.SetName("Reversed Hierarchical order");
            yield return testcase;
        }

        [Test, TestCaseSource("BoneMetadataCases")]
        public void ConvertBoneMetadataToTextureSpace_RegardlessOfOrder(List<SpriteBone> original, List<SpriteBone> expected, Vector2 offset)
        {
            var converted = MeshModuleUtility.ConvertBoneFromLocalSpaceToTextureSpace(original, offset);

            VerifyApproximatedSpriteBones(expected.ToArray(), converted.ToArray());
        }
        
        private static void VerifyApproximatedSpriteBones(SpriteBone[] expected, SpriteBone[] actual)
        {
            const double kLooseEqual = 0.001;
            Assert.AreEqual(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; ++i)
            {
                var expectedBone = expected[i];
                var actualBone = actual[i];

                Assert.AreEqual(expectedBone.name, actualBone.name, "Name not matched at #{0}", i);
                Assert.AreEqual(expectedBone.parentId, actualBone.parentId, "ParentId not matched at #{0}", i);
                Assert.AreEqual(expectedBone.length, actualBone.length, kLooseEqual, "Length not matched at #{0}", i);
                Assert.AreEqual(expectedBone.position.x, actualBone.position.x, kLooseEqual, "Position X not matched at #{0}", i);
                Assert.AreEqual(expectedBone.position.y, actualBone.position.y, kLooseEqual, "Position Y not matched at #{0}", i);
                Assert.AreEqual(expectedBone.position.z, actualBone.position.z, kLooseEqual, "Position Z not matched at #{0}", i);

                var expectedEuler = expectedBone.rotation.eulerAngles;
                var actualEuler = actualBone.rotation.eulerAngles;
                Assert.AreEqual(expectedEuler.x, actualEuler.x, kLooseEqual, "Rotation X not matched at #{0}", i);
                Assert.AreEqual(expectedEuler.y, actualEuler.y, kLooseEqual, "Rotation Y not matched at #{0}", i);
                Assert.AreEqual(expectedEuler.z, actualEuler.z, kLooseEqual, "Rotation Z not matched at #{0}", i);
            }
        }
    }
}
