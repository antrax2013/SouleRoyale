Feature: Player Features
	Features to manage the behavior of soule player

Scenario: Create a player
	When I create a new player
	Then The player created have 4 life points
	And The player is not KO
	And The player position is 0

Scenario: Wrong Move
	Given A player
	When After a move, the player receives a wrong position:
	| position |
	| -4       |
	| 4        |
	Then A InvalidOperationException fires with message A player cannot leave the game area.
	
Scenario: A KO player cannot move
	Given A KO player
	When After a move, the player receives a new position
	Then A InvalidOperationException fires with message A KO player cannot move.

Scenario: A player becomes KO
	Given A player with 1 point of life
	When On losing the last point of live
	Then The player becomes KO
