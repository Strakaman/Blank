/*enum for player state to control when certain things should trigger,
 * i.e. pausing should not be allowed if player is dead because a different GUI should pop up
 */ 
public enum PState { normal = 0, inmenus = 1, dead = 9 };
