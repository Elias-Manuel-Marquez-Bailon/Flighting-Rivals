using UnityEngine;

namespace FightingRivals.Arena
{
    // Límites de la arena. Va en el objeto "Arena".
    // Para cambiar el ancho de la arena, mueve en la escena los objetos LeftBoundary y RightBoundary.
    public class ArenaBounds : MonoBehaviour
    {
        public Transform leftBoundary;
        public Transform rightBoundary;
        [Tooltip("Collider del suelo; sirve para saber si un luchador está en el piso.")]
        public Collider2D floor;

        public float LeftLimit => leftBoundary != null ? leftBoundary.position.x : -8f;
        public float RightLimit => rightBoundary != null ? rightBoundary.position.x : 8f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(new Vector3(LeftLimit, -6f, 0f), new Vector3(LeftLimit, 6f, 0f));
            Gizmos.DrawLine(new Vector3(RightLimit, -6f, 0f), new Vector3(RightLimit, 6f, 0f));
        }
    }
}
