﻿using UnityEngine;
using UnityEditor;

namespace GameOfLife
{
    [CustomEditor(typeof(ViewMapGameOfLife_Gizmos))]
    public class ViewMapGameOfLife_GizmosEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ViewMapGameOfLife_Gizmos viewMap = (ViewMapGameOfLife_Gizmos)target;

            GUI.enabled = (!Application.isPlaying || !viewMap.solving);
            DrawDefaultInspector();
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