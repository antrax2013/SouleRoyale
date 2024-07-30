Feature: Player Features
	Features to manage the behavior of soule player

Scenario: Create a player
	Given I ask to create a new player
	When I create a new player
	Then The player created have 4 life points
	And The player is not ko
	And The player position is 0