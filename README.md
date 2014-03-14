Compy Compit
============
Game Controls:
--------------
- <arrow keys> for player movement
- <spacebar> for player jump
- <r> restart game (after win/lose)

About:
------
Compy Compit is a game where the player is pitted against a computer “compy” opponent on an aerial teeter-totter. The ultimate goal is to push the computer opponent off of the teeter-totter. Wins are collected along the way (until player loses… and then wins are reset.) Many ways to win or lose!

Game Details:
-------------
Compy Compit includes a fairly basic AI:

- The computer opponent has a limited range of vision (visibleDistance) setting where if player is within this visibleDistance length, the computer opponent can move/run towards player.
- When computer opponent is within a set, close range to player, computer opponent will try to jump (in this game, a jump is a type of “attack”,) if grounded (that is, if computer opponent is touching the ground, it can jump when within close proximity of the player.)
- When computer opponent can no longer see player (i.e. if player is outside the visibleDistance,) computer opponent will simply jump up and down (when grounded.) When the computer opponent does this sort of jumping, it will also return back to an upright position (because I’ve purposely turned off the “Fixed Angle” option after noticing that it adds another dimension to the compy battles. Previously, “Fixed Angle” was checked so that each character remained upright throughout an entire battle… but it just didn’t look as realistic, in the case of this sort of game.)
- Computer opponent “randomness” happens throughout the game via the attached 2D game physics matched with the finite state processes (and the overall variable that acts as a catalyst for this “randomness” is the player itself and where the player is located on the game stage at any given time.)

Also, both computer opponent and player use Animator to provide an animation-related finite state machine. When a computer opponent and/or player are moving to the left or right, for example, the Animator matches up the current direction to the corresponding animation state. (Admittedly, the animation frames in this game are very simple… but the goal was to aim more towards a simple, two-dimensional feel for the in-game compy characters.)