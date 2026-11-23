namespace FightingRivals.Characters
{
    // Estados posibles de un luchador. Solo puede estar en uno a la vez,
    // así se evitan combinaciones imposibles (por ejemplo, atacar estando en K.O.).
    public enum FighterState
    {
        Idle,       // Quieto en el suelo.
        Moving,     // Caminando.
        Jumping,    // En el aire.
        Attacking,  // Ejecutando un ataque.
        Blocking,   // Bloqueando.
        Hitstun,    // Aturdido después de recibir un golpe.
        KO          // Derrotado.
    }
}
