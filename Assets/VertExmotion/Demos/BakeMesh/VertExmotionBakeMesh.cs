using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kalagaan
{
	public class VertExmotionBakeMesh : MonoBehaviour
    {	
                
        public VertExmotion m_vtm;
        public MeshFilter m_target;

        public bool m_bake = false;

        public void Awake()
		{
            if(m_vtm == null)
                m_vtm = GetComponent<VertExmotion>();
		}


		public void Update()
		{
			if (m_bake && m_vtm != null)
			{
				Mesh m = m_vtm.BakeMesh();
				if (m_target != null)
				{
					m_target.sharedMesh = m;
				}
			}
		}       
	}
}