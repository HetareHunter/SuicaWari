using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MultiHitRay : MonoBehaviour
{
	[Range(0.1f, 30f)]
	public float Length = 20;
	public LayerMask Mask;
	private List<Vector3> posList = new List<Vector3>();
	int rayCount = 0;
	public Vector3 hitPoint;

	void Update()
	{
		rayCount = 0;

		posList.Clear();

		//  双方向Rayで大雑把な形状を取得
		RaycastHit[] foward = Physics.RaycastAll(transform.position, transform.forward, Length, Mask);
		RaycastHit[] back = Physics.RaycastAll(transform.position + transform.forward * Length, transform.forward * -1, Length, Mask);

		rayCount += 2;

		// 外枠のポイント一覧を登録
		foreach (RaycastHit hit in foward)
		{
			posList.Add(hit.point);
		}
		foreach (RaycastHit hit in back)
		{
			posList.Add(hit.point);
		}

		// 内部のコライダーを取得
		foreach (RaycastHit hit in foward)
		{
			foreach (RaycastHit hit2 in back)
			{
				if (hit.collider == hit2.collider)
				{

					float maxDistance = Vector3.Distance(hit.point, hit2.point);

					// 正方向の外枠から逆方向の外枠までをチェック
					{
						RaycastHit inMeshHit;
						float distance = maxDistance;
						Vector3 currentPoint = hit.point;
						while (hit.collider.Raycast(new Ray(currentPoint + transform.forward, transform.forward), out inMeshHit, distance))
						{
							posList.Add(inMeshHit.point);
							currentPoint = inMeshHit.point;
							distance -= inMeshHit.distance;

							hitPoint = SetPosition(currentPoint);
							rayCount++;
						}
					}

					// 逆方向の外枠から正方向の外枠までをチェック
					{
						RaycastHit inMeshHit;
						float distance = maxDistance;
						Vector3 currentPoint = hit2.point;
						while (hit.collider.Raycast(new Ray(currentPoint - transform.forward, transform.forward * -1), out inMeshHit, distance))
						{
							posList.Add(inMeshHit.point);
							currentPoint = inMeshHit.point;
							distance -= inMeshHit.distance;

							hitPoint = SetPosition(currentPoint);
							rayCount++;
						}
					}
				}
			}
		}
	}

	void OnGUI()
	{
		GUILayout.Label(string.Format("point count{0} raycount:{1}", posList.Count, rayCount));
	}

	void OnDrawGizmos()
	{
		if (posList == null)
		{
			return;
		}
		Gizmos.color = Color.white;
		Gizmos.DrawSphere(transform.position, 0.1f);
		Gizmos.DrawSphere(transform.position + transform.forward * Length, 0.1f);
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * Length);
		Gizmos.color = Color.red;
		foreach (Vector3 pos in posList)
		{
			Gizmos.DrawSphere(pos, 0.1f);
		}
		Gizmos.color = Color.white;
	}

	public Vector3 SetPosition(Vector3 point)
	{
		Vector3 setPoint = point;
		return setPoint;
	} 
}