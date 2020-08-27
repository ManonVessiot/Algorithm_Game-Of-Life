using UnityEngine;
using UnityEditor;

namespace GameOfLife
{
    [CustomEditor(typeof(ViewMapGameOfLife_Screeen))]
    public class ViewMapGameOfLife_ScreenEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ViewMapGameOfLife_Screeen viewMap = (ViewMapGameOfLife_Screeen)target;

            GUI.enabled = (!Application.isPlaying || !viewMap.solving);
            if (DrawDefaultInspector() && Application.isPlaying)
            {
                viewMap.Reset();
            }
            GUI.enabled = true;

            if (Application.isPlaying)
            {
                if (viewMap.solving)
                {
                    if (GUILayout.Button("Stop solving"))
                    {
                        Debug.Log("Stop solving");
                        viewMap.StopSolving();
                    }
                }
                else
                {
                    if (GUILayout.Button("Start solving"))
                    {
                        Debug.Log("Start solving");
                        viewMap.StartSolving();
                    }
                }

                if (GUILayout.Button("Reset"))
                {
                    if (viewMap.solving)
                    {
                        Debug.Log("Stop solving");
                        viewMap.StopSolving();
                    }
                    Debug.Log("Reset");
                    viewMap.Reset();
                }
            }
            else
            {
                if (GUILayout.Button("Next Stage"))
                {
                    Debug.Log("Next Stage");
                    viewMap.EvolveGameNextStage();
                }
                if (GUILayout.Button("Reset"))
                {
                    Debug.Log("Reset");
                    viewMap.Reset();
                }
            }            
        }
    }
}
