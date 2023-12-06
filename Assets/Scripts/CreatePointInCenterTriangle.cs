using System.Collections.Generic;
using UnityEngine;

//Касс для Editor. Находит центр треугольников меша, создает там пустой объект. Поворачивает  к центру.
public class CreatePointInCenterTriangle : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;

    [SerializeField] private Transform _parentObj;
    [SerializeField] private GameObject _objPointPref;
    [SerializeField] private Transform _centerDice;
    [SerializeField] private List<Transform> _points = new List<Transform>();

    private Mesh _mesh;
    private List<Vector3> _pointCenterTriangles = new List<Vector3>();


    //Строим треугольники меша, находим их центр, создаем пустой объект, добавляем объект в список.
    [ContextMenu("CreatePointInCenterTriangle")]
    private void CreatePoint()
    {
        _mesh = _meshFilter.GetComponent<MeshFilter>().mesh;

        Vector3[] meshVertices = _mesh.vertices;
        int[] meshTriangles = _mesh.triangles;

        _pointCenterTriangles.Clear();
        for (int i = 0; i < meshTriangles.Length; i += 3)
        {
            var center = (meshVertices[meshTriangles[i]] + meshVertices[meshTriangles[i + 1]] + meshVertices[meshTriangles[i + 2]]) / 3;

            _pointCenterTriangles.Add(center);
        }

        for (int i = 0; i < _pointCenterTriangles.Count; i++)
        {
            var gObj = Instantiate(_objPointPref, _pointCenterTriangles[i], Quaternion.identity);

            gObj.transform.SetParent(_parentObj, false);
            _points.Add(gObj.transform);
        }
        _pointCenterTriangles.Clear();
    }

    //Поварачиваем пустые объекты к центру кубика.
    [ContextMenu("Rotate")]
    private void RotateOfCenter()
    {
        for (int i = 0; i < _points.Count; i++)
        {
            _points[i].transform.LookAt(transform.TransformPoint(transform.localPosition) + transform.TransformPoint(transform.localPosition) - transform.TransformPoint(_centerDice.localPosition));
        }
        _points.Clear();
    }
}
