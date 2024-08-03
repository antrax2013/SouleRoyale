Feature: MirrorMove Features
	Features to manage the behavior of mirror move class

Scenario: Move a player
	Given I create a new player in position 0 
	When I move the player, his position change
		| Move type   | Expected position |
		| MoveForward | -1                 |
		| MoveBack    | 1                |

Scenario: Not should move back a player out of the area
	Given I create a new player in his goal line position
	When I move back the player
	Then His position should not change
	And A invalid operation exception fires with message: 'A player cannot leave the game area.'

Scenario: Not should move foward a player out of the area
	Given I create a new player in the overside goal line position
	When I move forward the player
	Then His position should not change
	And A invalid operation exception fires with message: 'A player cannot leave the game area.'