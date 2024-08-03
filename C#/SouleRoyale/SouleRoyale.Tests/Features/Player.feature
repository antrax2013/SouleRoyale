Feature: Player Features
	Features to manage the behavior of soule player

Scenario: Create a player
	When I create a new player with 1 life point
	Then The player created have 1 life point
	And The player is not KO
	And The player position is 0

Scenario: A KO player cannot move back
	Given A KO player
	When The KO player move back
	Then A InvalidOperationException fires with message A KO player cannot move.

Scenario: A KO player cannot move foward
	Given A KO player
	When The KO player move foward
	Then A InvalidOperationException fires with message A KO player cannot move.

Scenario: A player becomes KO
	Given A player with 1 life point
	When On losing the last point of live
	Then The player becomes KO
