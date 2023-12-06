using UnityEngine;

//Класс для визуальной отрисовки ограничения кубика
public class BoundaryMoveDice : MonoBehaviour
{
    [SerializeField] private float _radius;
    public float radius => _radius;


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, _radius);
    }
#endif
}
