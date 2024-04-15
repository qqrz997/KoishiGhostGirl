using HarmonyLib;
using UnityEngine;

namespace KoishiGhostGirl.Patches
{
    [HarmonyPatch(typeof(DressGirlAI))]
    internal class DressGirlAIPatch
    {
        private static PluginConfig config => PluginConfig.Instance;

        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        private static void StartPostfix(DressGirlAI __instance)
        {
            if (!Koishi.Inited)
            {
                return;
            }

            PluginLog.Debug("Ghost girl spawned, replacing model");

            // make sure base model isn't enabled
            foreach (SkinnedMeshRenderer r in __instance.skinnedMeshRenderers)
            {
                r.enabled = false;
            }

            Transform girlModelTransform = __instance.transform.Find("DressGirlModel");
            Transform girlMetarig = girlModelTransform.Find("AnimContainer").Find("metarig");

            girlMetarig.name = "old_metarig";

            // disable the eyeballs
            foreach (Renderer renderer in girlMetarig.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }

            GameObject koishiObject = Object.Instantiate(Koishi.Mesh);
            koishiObject.transform.SetParent(girlModelTransform);
            koishiObject.transform.localPosition = Vector3.zero;
            koishiObject.transform.localRotation = Quaternion.identity;
            koishiObject.transform.localScale = Vector3.one;

            Transform rigTransform = koishiObject.transform.Find("metarig");
            rigTransform.SetParent(girlMetarig.parent, worldPositionStays: true);
            rigTransform.transform.localScale = girlMetarig.transform.localScale;
            rigTransform.transform.localRotation = girlMetarig.transform.localRotation;
            rigTransform.transform.localPosition = girlMetarig.transform.localPosition;

            SkinnedMeshRenderer bodyMesh = koishiObject.transform.Find("Body").GetComponent<SkinnedMeshRenderer>();
            bodyMesh.rootBone = rigTransform;
            bodyMesh.gameObject.tag = "DoNotSet";

            if (!config.UseOriginalSound)
            {
                __instance.heartbeatMusic.clip = Koishi.HeartbeatAudio;
                __instance.heartbeatMusic.pitch = 1f;
                __instance.heartbeatMusic.Play();
            }

            if (config.UseOriginalShader)
            {
                bodyMesh.material = Koishi.OriginalGirlMaterial;
            }

            // replace the mesh used by the AI
            __instance.skinnedMeshRenderers[0] = bodyMesh;

            __instance.EnableEnemyMesh(enable: false, overrideDoNotSet: true);
        }
    }
}
