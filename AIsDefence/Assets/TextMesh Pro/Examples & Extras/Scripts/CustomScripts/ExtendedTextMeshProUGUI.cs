using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExtendedTMPro
{
    public class ExtendedTextMeshProUGUI : TextMeshProUGUI
    {
        private const float ONE_RAD_IN_DEGREES = 57.2957795f;

        [SerializeField]
        private bool enableVertexWarping = true;
        [SerializeField]
        private AnimationCurve vertexCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, -1f), new Keyframe(1f, 0f));
        [SerializeField]
        private float scaleMultiplierPerCharacter = 1f;

        /// <summary>
        /// 
        /// </summary>
        //protected override void Start()
        //{
        //    base.Start();

        //    vertexCurve.preWrapMode = WrapMode.Clamp;
        //    vertexCurve.postWrapMode = WrapMode.Clamp;

        //    //this.OnValidate();
        //}

        /// <summary>
        /// 
        /// </summary>
        //protected override void OnValidate()
        //{
        //    base.OnValidate();
        //    Rebuild(CanvasUpdate.PreRender);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        //public override void Rebuild(CanvasUpdate update)
        //{
        //    base.Rebuild(update);

        //    if (update == CanvasUpdate.PreRender)
        //    {
        //        UpdateVertexPositionWarps();
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        private void UpdateVertexPositionWarps()
        {
            if (enableVertexWarping)
            {
                renderMode = TextRenderFlags.DontRender; // Instructing TextMesh Pro not to upload the mesh as we will be modifying it.
            }
            else
            {
                renderMode = TextRenderFlags.Render; // Instructing TextMesh Pro not to upload the mesh as we will be modifying it.
                return;
            }

            for (int i = 0; i < fontMaterials.Length; i++)
            {
                UpdateVertexPositionsWarp(i);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        private AnimationCurve CopyAnimationCurve(AnimationCurve curve)
        {
            AnimationCurve newCurve = new AnimationCurve();

            newCurve.keys = curve.keys;

            return newCurve;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meshInfoIndex"></param>
        private void UpdateVertexPositionsWarp(int meshInfoIndex)
        {
            TMP_MeshInfo meshInfo = textInfo.meshInfo[meshInfoIndex];
            Mesh mesh = meshInfo.mesh;

            bool isValidMeshInfo = IsValidMeshInfo(meshInfo);
            if (!isValidMeshInfo)
            {
                return;
            }

            ForceMeshUpdate(); // Generate the mesh and populate the textInfo with data we can use and manipulate.

            int characterCount = textInfo.characterCount;
            if (characterCount == 0)
            {
                return;
            }

            float finalCurveScale = scaleMultiplierPerCharacter * characterCount;
            Vector3[] vertices = meshInfo.vertices;

            float boundsMinX = bounds.min.x;
            float boundsMaxX = bounds.max.x;

            for (int i = 0; i < characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible)
                {
                    continue;
                }

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                // Compute the baseline mid point for each character
                Vector3 offsetToMidBaseline = new Vector2((vertices[vertexIndex + 0].x + vertices[vertexIndex + 2].x) / 2, textInfo.characterInfo[i].baseLine);

                // Apply offset to adjust our pivot point.
                vertices[vertexIndex + 0] += -offsetToMidBaseline;
                vertices[vertexIndex + 1] += -offsetToMidBaseline;
                vertices[vertexIndex + 2] += -offsetToMidBaseline;
                vertices[vertexIndex + 3] += -offsetToMidBaseline;

                // Compute the angle of rotation for each character based on the animation curve
                float x0 = (offsetToMidBaseline.x - boundsMinX) / (boundsMaxX - boundsMinX); // Character's position relative to the bounds of the mesh.
                float x1 = x0 + 0.0001f;
                float y0 = vertexCurve.Evaluate(x0) * finalCurveScale;
                float y1 = vertexCurve.Evaluate(x1) * finalCurveScale;

                Vector3 horizontal = new Vector3(1, 0, 0);
                Vector3 tangent = new Vector3(x1 * (boundsMaxX - boundsMinX) + boundsMinX, y1) - new Vector3(offsetToMidBaseline.x, y0);

                float dot = Mathf.Acos(Vector3.Dot(horizontal, tangent.normalized)) * ONE_RAD_IN_DEGREES;
                Vector3 cross = Vector3.Cross(horizontal, tangent);
                float angle = cross.z > 0 ? dot : 360 - dot;

                Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(0, y0, 0), Quaternion.Euler(0, 0, angle), Vector3.one);

                vertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 0]);
                vertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 1]);
                vertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 2]);
                vertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 3]);

                vertices[vertexIndex + 0] += offsetToMidBaseline;
                vertices[vertexIndex + 1] += offsetToMidBaseline;
                vertices[vertexIndex + 2] += offsetToMidBaseline;
                vertices[vertexIndex + 3] += offsetToMidBaseline;
            }

            UpdateMesh(mesh, vertices, meshInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meshInfo"></param>
        /// <returns></returns>
        private bool IsValidMeshInfo(TMP_MeshInfo meshInfo)
        {
            return mesh != null && meshInfo.vertices != null && meshInfo.vertexCount > 0 && meshInfo.uvs0 != null && meshInfo.uvs2 != null && meshInfo.colors32 != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="newVertices"></param>
        /// <param name="meshInfo"></param>
        private void UpdateMesh(Mesh mesh, Vector3[] newVertices, TMP_MeshInfo meshInfo)
        {
            mesh.vertices = newVertices;
            mesh.uv = meshInfo.uvs0;
            mesh.uv2 = meshInfo.uvs2;
            mesh.colors32 = meshInfo.colors32;

            mesh.RecalculateBounds();

            canvasRenderer.SetMesh(mesh);
        }
    }
}