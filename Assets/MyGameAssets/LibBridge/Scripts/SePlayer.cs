/******************************************************************************/
/*!    \brief  指定SEを再生する.
*******************************************************************************/

using UnityEngine;
#if USE_POOL_MANAGER
using PathologicalGames;
#endif
using System.Collections.Generic;
namespace VMUnityLib
{
    public sealed class SePlayer : MonoBehaviour
    {
#if USE_POOL_MANAGER
        [SerializeField]
        SpawnPool sePool;
#endif

        [SerializeField]
        GameObject sePrefab;

#if USE_POOL_MANAGER && USE_ADX
        List<CriAtomSource> endWatchSeList = new List<CriAtomSource>();
#endif

        /// <summary>
        /// 再生.
        /// </summary>
        public void PlaySe(string cueName, float volume = 1, float pitch = 0)
        {
#if USE_POOL_MANAGER && USE_ADX
            GameObject prefab = sePrefab;

            Transform spawnedSeTrans;
            spawnedSeTrans = sePool.Spawn(prefab);
            CriAtomSource spawnedSe = spawnedSeTrans.GetComponent<CriAtomSource>();

            // 一つ目、二つ目のfloatパラメータをそれぞれボリュームとピッチに設定.
            spawnedSe.volume = volume;
            spawnedSe.pitch = pitch;
            spawnedSe.cueName = cueName;
            spawnedSe.Play();

            endWatchSeList.Add(spawnedSe);
#else
            Debug.LogError("you need pool manager and cri");
#endif
        }


        /// <summary>
        /// 再生終了したものをすべて削除する.
        /// </summary>
        void LateUpdate()
        {
#if USE_POOL_MANAGER && USE_ADX
            List<CriAtomSource> removeSeList = endWatchSeList.FindAll(se => (se.status == CriAtomSource.Status.PlayEnd));
            foreach (var item in removeSeList)
            {
                sePool.Despawn(item.transform);
                endWatchSeList.Remove(item);
            }
#endif
        }
        /// <summary>
        /// 全ての音を停止させる.
        /// </summary>
        public void StopSeAll()
        {
#if USE_POOL_MANAGER && USE_ADX
            List<CriAtomSource> removeSeList = endWatchSeList.FindAll(se => (se.status == CriAtomSource.Status.Playing));
            foreach (var item in removeSeList)
            {
                item.Stop();
            }
        }
#else
            Debug.LogError("you need pool manager and cri");
#endif
        }
    }
}