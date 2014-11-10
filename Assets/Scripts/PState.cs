/*enum for player state to control when certain things should trigger,
 * i.e. pausing should not be allowed if player is dead because a different GUI should pop up
 */ 
public enum PState { 

	normal = 0, //player actually playing the game and can move/shoot/etc. 
	inmenus = 1, //player maybe at the main or pause menu
	talking = 2, //player talking to an NPC or reading a sign or something
	grabbing = 3, //player is grabbing a block
	stunned = 4, //player is stunned
	slowed = 5,
	dead = 9 //player just died, use to maybe pop up death menu option

};
