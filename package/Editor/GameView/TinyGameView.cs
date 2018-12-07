using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if UNITY_2019_1_OR_NEWER
using UnityEngine.UIElements;
#else
using UnityEngine.Experimental.UIElements;
#endif

namespace Unity.Tiny
{
    internal class TinyGameView : TinyEditorWindowOverride<EditorWindow>
    {
        private Uri QRCodeURL { get; set; }
        private Texture2D QRCodeTexture { get; set; }

        public override void OnEnable()
        {
            base.OnEnable();

            var imgui = new IMGUIContainer(OnGUI);
            Root.Add(imgui);
            imgui.StretchToParentSize();
            Root.visible = EditorApplication.isPlayingOrWillChangePlaymode;
            EditorApplication.playModeStateChanged += HandlePlayModeStateChanged;
        }

        public override void OnDisable()
        {
            EditorApplication.playModeStateChanged -= HandlePlayModeStateChanged;
        }
        
        private void HandlePlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
                case PlayModeStateChange.EnteredPlayMode:
                    Root.visible = true;
                    break;
                case PlayModeStateChange.EnteredEditMode:
                case PlayModeStateChange.ExitingPlayMode:
                    Root.visible = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnGUI()
        {
            if (!EditorApplication.isPlaying)
            {
                return;
            }

            var httpServer = HTTPServer.Instance;
            if (httpServer == null || !httpServer.Listening)
            {
                return;
            }

            // Update QR Code
            if (httpServer.URL != QRCodeURL)
            {
                QRCodeURL = httpServer.URL;
                QRCodeTexture = QRCode.Generate(httpServer.URL);
            }

            // Draw background
            var rect = Root.contentRect;
            EditorGUI.DrawRect(rect, new Color(0, 0, 0, 0.5f));

            // Draw GUI elements
            using (new GUILayout.VerticalScope())
            {
                GUILayout.FlexibleSpace();

                // Draw IP address label
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button(httpServer.URL.AbsoluteUri, EditorStyles.whiteLabel))
                    {
                        Application.OpenURL(httpServer.LocalURL.AbsoluteUri);
                    }
                    GUILayout.FlexibleSpace();
                }

                GUILayout.Space(15);

                // Draw QR code texture
                using (new GUILayout.HorizontalScope())
                {
                    var size = Math.Min(rect.width, rect.height) / 3;
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button(QRCodeTexture, EditorStyles.whiteLabel, GUILayout.Width(size), GUILayout.Height(size)))
                    {
                        Application.OpenURL(httpServer.LocalURL.AbsoluteUri);
                    }
                    GUILayout.FlexibleSpace();
                }

                // Draw clients dropdown
                var wsServer = WebSocketServer.Instance;
                if (wsServer != null)
                {
                    GUILayout.Space(15);

                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.Label("Active Client:", EditorStyles.whiteLabel, GUILayout.ExpandWidth(false));

                        var size = rect.width / 3;
                        var index = wsServer.ActiveClientIndex;
                        if (index >= 0)
                        {
                            var clients = wsServer.Clients.Select(c => c.Label).ToArray();
                            wsServer.ActiveClientIndex = EditorGUILayout.Popup(index, clients, GUILayout.Width(size));
                        }
                        else
                        {
                            EditorGUILayout.Popup(0, new string[] { "None" }, GUILayout.Width(size));
                        }
                        GUILayout.FlexibleSpace();
                    }
                }

                GUILayout.Space(15);

                // Draw message about read only state
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(new GUIContent("Changes in the editor while in play mode will not be reflected in running clients."), EditorStyles.whiteLabel);
                    GUILayout.FlexibleSpace();
                }

                GUILayout.FlexibleSpace();
            }
        }
    }
}
