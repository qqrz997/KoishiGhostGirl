using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace KoishiGhost.Patches
{
    [HarmonyPatch(typeof(DressGirlAI))]
    internal class DressGirlAIPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPatch(DressGirlAI __instance)
        {
            Transform transformDressGirlModel = __instance.transform.Find("DressGirlModel");
            Transform animationTransform = transformDressGirlModel.Find("AnimContainer").Find("metarig");
            SkinnedMeshRenderer skinnedMeshRenderer = transformDressGirlModel.Find("basemesh").GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer != null && skinnedMeshRenderer.enabled)
            {
                skinnedMeshRenderer.enabled = false;
                foreach (MeshRenderer item in animationTransform.GetComponentsInChildren<MeshRenderer>())
                {
                    item.enabled = false;
                }

                GameObject koishiObject = Object.Instantiate(Plugin.koishMesh);
                koishiObject.transform.SetParent(transformDressGirlModel);
                koishiObject.transform.localPosition = Vector3.zero;
                koishiObject.transform.localRotation = Quaternion.identity;
                koishiObject.transform.localScale = Vector3.one;

                Transform rigTransform = koishiObject.transform.Find("metarig");
                rigTransform.SetParent(animationTransform.parent, worldPositionStays: true);
                rigTransform.transform.localScale = animationTransform.transform.localScale;
                rigTransform.transform.localRotation = animationTransform.transform.localRotation;
                rigTransform.transform.localPosition = animationTransform.transform.localPosition;

                Transform bodyTransform = koishiObject.transform.Find("Body");
                SkinnedMeshRenderer bodyMesh = bodyTransform.GetComponent<SkinnedMeshRenderer>();
                bodyMesh.rootBone = rigTransform;
                bodyMesh.gameObject.tag = "DoNotSet";

                if (!KoishiConfig.useOriginalSound.Value)
                {
                    __instance.heartbeatMusic.clip = Plugin.koishHeartbeatAudio;
                    __instance.heartbeatMusic.pitch = 1f;
                    __instance.heartbeatMusic.Play();
                }

                if (KoishiConfig.useOriginalShader.Value)
                {
                    bodyMesh.material = Plugin.originalGirlMaterial;
                }

                List<SkinnedMeshRenderer> list = new List<SkinnedMeshRenderer>();
                foreach (SkinnedMeshRenderer item in __instance.skinnedMeshRenderers)
                {
                    list.Add(item);
                }
                list.Add(bodyMesh);
                __instance.skinnedMeshRenderers = list.ToArray();


                animationTransform.name = "old_metarig";
            }
        }
    }
}
