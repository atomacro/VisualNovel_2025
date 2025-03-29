using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VisualNovel_2025
{
    public class HelperClass
    {

        public GameObject GetGameObjectFromAnotherScene(String name, Scene scene)
        {
            foreach (GameObject obj in scene.GetRootGameObjects())
            {
                if (obj.name == name)
                {
                    return obj;
                }
            }
            return null;
        }

        public GameObject GetChildGameObject(string childName, GameObject parent)
        {
            Transform childTransform = parent.transform.Find(childName);
            if (childTransform != null)
            {
                return childTransform.gameObject;
            }
            else
            {
                return null;
            }
        }
    }
}