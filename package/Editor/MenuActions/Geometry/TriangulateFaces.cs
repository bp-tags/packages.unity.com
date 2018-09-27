using UnityEngine;
using System.Linq;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

namespace UnityEditor.ProBuilder.Actions
{
	sealed class TriangulateFaces : MenuAction
	{
		public override ToolbarGroup group
		{
			get { return ToolbarGroup.Geometry; }
		}

		public override Texture2D icon
		{
			get { return IconUtility.GetIcon("Toolbar/Face_Triangulate", IconSkin.Pro); }
		}

		public override TooltipContent tooltip
		{
			get { return s_Tooltip; }
		}

		static readonly TooltipContent s_Tooltip = new TooltipContent
		(
			"Triangulate Faces",
			"Break all selected faces down to triangles."
		);

		public override SelectMode validSelectModes
		{
			get { return SelectMode.Face; }
		}

		public override bool enabled
		{
			get { return base.enabled && MeshSelection.selectedFaceCount > 0; }
		}

		public override ActionResult DoAction()
		{
			ActionResult res = ActionResult.NoSelection;

			UndoUtility.RecordSelection(MeshSelection.TopInternal(), "Triangulate Faces");

			foreach (ProBuilderMesh pb in MeshSelection.TopInternal())
			{
				pb.ToMesh();
				Face[] triangulatedFaces = pb.ToTriangles(pb.selectedFacesInternal);
				pb.Refresh();
				pb.Optimize();
				pb.SetSelectedFaces(triangulatedFaces);
				res = new ActionResult(ActionResult.Status.Success, string.Format("Triangulated {0} {1}", triangulatedFaces.Length, triangulatedFaces.Length < 2 ? "Face" : "Faces"));
			}

			ProBuilderEditor.Refresh();

			return res;
		}
	}
}
